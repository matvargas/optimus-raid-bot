using RaidBot.Common.IO;
using RaidBot.Common.Network.Client;
using RaidBot.Protocol.Types;
using RaidBot.Engine.Bot.Frames;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Engine.Bot.Data;
using RaidBot.Engine.Bot.Managers;
using Raidbot.Protocol.Messages;
using System.Net;
using RaidBot.Engine.Daemon;
using RaidBot.Common.Default.Loging;
using RaidBot.Engine.Bot.Managers.GameContext;
using RaidBot.Engine.Bot.Frames.GameContext;
using RaidBot.Engine.Bot.Routines;
using RaidBot.Engine.Bot.Managers.FightContext;
using RaidBot.Engine.Bot.Frames.FightContext;
using RaidBot.Engine.Bot.Extension.Context;
using RaidBot.Engine.Bot.Managers.Extension;

namespace RaidBot.Engine.Bot
{
    public class Brain : IDisposable
    {

        public static ObservableProperty<Dictionary<String, Brain>> ActiveInstances = new ObservableProperty<Dictionary<string, Brain>>(new Dictionary<string, Brain>());
        public enum BrainState
        {
            Disconnected,
            Idle,
            Dialog,
            Fight,
            Moving,
            WatingFollowers,
        }

        public bool WillReconnect { get; set; }

        public ObservableProperty<BrainState> CurrentState { get; set; }

        public Logger NetworkLogger { get; }
        public Logger Logger { get; }

        public Group Group { get; set; }

        /// <summary>
        /// All informations collected during exchange with server
        /// </summary>
        public Store State { get; }


        /// <summary>
        /// Permanent and user defined configuration
        /// </summary>
        public BotConfig Config { get; }

        /// <summary>
        /// DoDo server connection (Login and game)
        /// </summary>
        public Client Connection { get; private set; }

        /// <summary>
        /// Dispatch messages, principaly to frames using reflection
        /// </summary>
        public Dispatcher.Dispatcher Dispatcher { get; }

        /// <summary>
        /// Anka security that count packets and append this counter to all packet header
        /// </summary>
        public uint GlobalInstanceId { get; private set; }

        public ExtensionManager ExtManager { get; private set; }

        /// <summary>
        /// Managers
        /// </summary>
        public AuthManager AuthManager { get; }
        public ServerSelectionManager ServerManager { get; }
        public PlayerManager PlayerManager { get; }
        public ElementsManager ElementsManager { get; }
        public FightManager FightManager { get; }
        public InventoryManager InventoryManager { get; }
        public DialogManager DialogManager { get; }
        public PartyManager PartyManager { get; }
        public ActivityManager ActivityManager { get; }

        /// <summary>
        /// Frames 
        /// </summary>
        public AuthFrame @AuthFrame { get; }                               // Manage identification and game server ticket
        public CharacterSelectionFrame @CharacterSelectionFrame { get; }   // For now just select first character avaible
        public GameLoadingFrame @GameLoadingFrame { get; }                 // Make basic when you logon and send clientkeymessage
        public ContextFrame ContextFrame { get; }                           // TODO
        public MovementFrame MovementFrame { get; }
        public FightFrame FightFrame { get; }
        public LatencyFrame LatencyFrame { get; private set; }
        public InventoryFrame InventoryFrame { get; private set; }
        public DialogFrame DialogFrame { get; private set; }
        public PartyFrame PartyFrame { get; private set; }
        ///
        /// Routines
        ///
        public Fighter FighterRoutine { get; private set; }

        public SharedExtensionsContext ExtensionsContext { get; private set;}

        /// <summary>
        /// A simple client that connect to my server and send it RDM,
        /// in server side we just resend the RDM to an official client
        /// it resend us the CheckIntegrityMessage we want and the server
        /// send it to the DaemonClient so we can resend it to the server LOl!
        /// To make this you just need to remove some protection that make impossible to send RDM from login server
        /// NOTE For now you can RDM from different account to the same client (with no deconnection) and it's work fine
        /// </summary>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">Bot configuration</param>
        public Brain(BotConfig config, Group group)
        {
            ActiveInstances.Get().Add(config.Username, this);
            ActiveInstances.OnChanged();

            CurrentState = new ObservableProperty<BrainState>(BrainState.Disconnected);
            this.NetworkLogger = new Logger();
            this.Logger = new Logger();
            this.Group = group;
            this.Dispatcher = new Dispatcher.Dispatcher();
            this.GlobalInstanceId = 0;
            this.Config = config;
            this.State = new Store();

            //Managers
            this.AuthManager = new AuthManager(this);
            this.ServerManager = new ServerSelectionManager(this);
            this.PlayerManager = new PlayerManager(this);
            this.ElementsManager = new ElementsManager(this);
            this.FightManager = new FightManager(this);
            this.InventoryManager = new InventoryManager(this);
            this.PartyManager = new PartyManager(this);
            this.ActivityManager = new ActivityManager(this);
            //Frames
            this.AuthFrame = new AuthFrame(this);
            this.CharacterSelectionFrame = new CharacterSelectionFrame(this);
            this.GameLoadingFrame = new GameLoadingFrame(this);
            this.ContextFrame = new ContextFrame(this);
            this.LatencyFrame = new LatencyFrame(this);
            this.MovementFrame = new MovementFrame(this);
            this.FightFrame = new FightFrame(this);
            this.InventoryFrame = new InventoryFrame(this);
            this.PartyFrame = new PartyFrame(this);
            //Routines
            this.FighterRoutine = new Fighter(this);
            //Managers post init
            this.PlayerManager.Movement.PostInit();

            ExtManager = new ExtensionManager(this);
            WillReconnect = false;
            Logger.Log(String.Format("[Core] Client loaded ({0})", config.Username));

            ExtensionsContext = new SharedExtensionsContext(this);
        }

        ~Brain()
        {
            ActiveInstances.Get().Remove(this.Config.Username);
            ActiveInstances.OnChanged();
            Group.Bots.Remove(this.Config.Username);
            Group.Configs.Remove(this.Config.Username);
            if (Group.Leader == this)
                Group.Leader = null;
        }

        /// <summary>
        /// Refresh current client (socket) and register all events listener
        /// </summary>
        /// <param name="newClient">Nouvelle connection au serveur</param>
        public void UpdateClient(Client newClient)
        {
            CurrentState.Set(BrainState.Disconnected);
            this.Connection = newClient;
            this.Connection.DataReceived += Connection_DataReceived;
            this.Connection.DataSended += Connection_DataSended;
            this.Connection.Connected += Connection_Connected;
            this.Connection.Disconnected += Connection_Disconnected;
        }

        /// <summary>
        /// Thath means client want to make migration from login server to game server
        /// TODO: Add other server selection message
        /// </summary>
        /// <param name="msg">Message received from server</param>
        bool Migrating = false;
        public void SelectedServer(SelectedServerDataMessage msg)
        {
            State.SelectedServer.Set(msg);
            Logger.Log(String.Format("[Core] Migrating to game server"));
            Connection.Stop();
            UpdateClient(new Client(IPAddress.Parse(msg.Address), msg.Ports[0], NetworkLogger));
            Migrating = true;
            WillReconnect = false;
        }

        private void Connection_Disconnected(object sender, Client.DisconnectedEventArgs e)
        {
            CurrentState.Set(BrainState.Disconnected);
            Logger.Log(String.Format("[Core] Disconnected !"), LogLevelEnum.Warning);
            this.State.Connected.Set(false);
        }

        private void Connection_Connected(object sender, Client.ConnectedEventArgs e)
        {
            Logger.Log(String.Format("[Core] Connected !"));
            this.State.Connected.Set(true);
            this.CurrentState.Set(BrainState.Idle);
            if (Migrating)
            {
                Migrating = false;
                WillReconnect = true;
            }
        }

        public void SendMessage(NetworkMessage message)
        {
            NetworkLogger.LogObj(new PacketLogContainer(message, false));
            CustomDataWriter writer = new CustomDataWriter();
            message.Pack(writer, ++GlobalInstanceId);
            Connection.Send(writer.Data);
        }

        private void Connection_DataReceived(object sender, Client.DataReceivedEventArgs e)
        {
            NetworkMessage msg = ProtocolManager.GetPacket(e.Data.Data, (uint)e.Data.MessageId);
            NetworkLogger.LogObj(new PacketLogContainer(msg, true));
            this.GlobalInstanceId++;
            this.Dispatcher.DispatchMessage(msg, this);
        }

        private void Connection_DataSended(object sender, Client.DataSendedEventArgs e)
        {
        }

        public void Dispose()
        {
            ActiveInstances.Get().Remove(this.Config.Username);
            ActiveInstances.OnChanged();
            Group.Bots.Remove(this.Config.Username);
            Group.Configs.Remove(this.Config.Username);
            if (Group.Leader == this)
                Group.Leader = null;
        }

        public class PacketLogContainer
        {
            public NetworkMessage Message { get; set; }
            public bool Received { get; set; }

            public PacketLogContainer(NetworkMessage msg, bool received)
            {
                Message = msg;
                Received = received;
            }

            public override string ToString()
            {
                return String.Format("{0} {1}", Received ? "<" : ">", Message.GetType().Name);
            }
        }
    }
}
