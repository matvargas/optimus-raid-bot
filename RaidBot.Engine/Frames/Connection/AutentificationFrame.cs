using RaidBot.Common.Network.Server;
using RaidBot.Engine.Dispatcher;
using RaidBot.Engine.Enums;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Setting;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Frames.Connection
{
    public class AutentificationFrame : Frame
    {
        #region Properity

        private ConnectedHost mHost;
        private Server mServer;
        private string mGameAdress;
        public string mTicket;

        #endregion

        #region Events

        public event EventHandler ConnectedToGame;

        private void OnConnectedToGame()
        {
            if (ConnectedToGame != null)
                ConnectedToGame(this, new EventArgs());
        }

        #endregion

        #region Constructor, Init/UnInit

        public AutentificationFrame()
        {

        }

        public void Init(ConnectedHost host)
        {
            mHost = host;
            mHost.Dispatcher.Register(this);
        }

        public void UnInit()
        {
            mHost.Dispatcher.UnRegister(this);
        }

        #endregion

        #region Methods

        #endregion

        #region MessagesHandler

        [MessageHandlerAttribut(typeof(ServerSelectionMessage))]
        private void HandleServerSelectionMessage(ServerSelectionMessage message, ConnectedHost source)
        {
            mServer = new Server();
            mServer.Start(RemoteSetting.Default.GameListenPort);
            mServer.ConnectionAccepted += Default_ConnectionAccepted;
        }

        void Default_ConnectionAccepted(object sender, Socket acceptedSocket)
        {
            mHost.Migrate(acceptedSocket, IPAddress.Parse(mGameAdress), RemoteSetting.Default.GameListenPort);
            mServer.ConnectionAccepted -= Default_ConnectionAccepted;
            mServer.Stop();
            OnConnectedToGame();
        }

        [MessageHandlerAttribut(typeof(SelectedServerDataMessage))]
        private bool HandleSelectedServerDataMessage(SelectedServerDataMessage message, ConnectedHost source)
        {
            mGameAdress = message.address;
            message.address = RemoteSetting.Default.GameListenAdress;
            message.port = (ushort)RemoteSetting.Default.GameListenPort;
            mHost.SendMessage(message, DestinationEnum.CLIENT);
            mHost.PreMigrate();
            return false;
        }
        [MessageHandlerAttribut(typeof(CharacterSelectedSuccessMessage))]
        private void HandleSelectedServerDataMessage(CharacterSelectedSuccessMessage message, ConnectedHost source)
        {
            source.isConnected = true;
        }

        [MessageHandlerAttribut(typeof(SelectedServerDataExtendedMessage))]
        private bool HandleSelectedServerDataExtendedMessage(SelectedServerDataExtendedMessage message, ConnectedHost source)
        {
            mGameAdress = message.address;
            message.address = RemoteSetting.Default.GameListenAdress;
            message.port = (ushort)RemoteSetting.Default.GameListenPort;
            mHost.SendMessage(message, DestinationEnum.CLIENT);
            mHost.PreMigrate();
            return false;
        }
       

        #endregion


    }
}