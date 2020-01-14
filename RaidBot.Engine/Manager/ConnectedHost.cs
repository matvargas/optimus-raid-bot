using RaidBot.Common.Default.Loging;
using RaidBot.Engine.Controler;
using RaidBot.Engine.Enums;
using RaidBot.Engine.Frames.Connection;
using RaidBot.Engine.Functionality.Script.Machine;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Manager
{
    public class ConnectedHost
    {
        #region Properity
        public bool isConnected = false;
        public Logger logger;
        private ConnectionManager connectionManager;
        public BotControler Bot { get; private set; }
        public TrajetControler Trajet { get; private set;}
        public Dispatcher.Dispatcher Dispatcher { get; private set; }
        public Model.Authentification.AccountInformationsModel account { get; private set; }
		public bool IsFS = false;
		public string ndc;
		public string mdp;
        #endregion

        #region Events

        public event EventHandler<MigrateRequestEventArgs> MigrateRequest;
        public event EventHandler PreMigrateRequest;
        public event EventHandler CloseRequest;
        private void OnCloseRequest()
        {
            if (CloseRequest != null)
                CloseRequest(this, new EventArgs());
        }
        private void OnOnPreMigrateRequest()
        {
            if(PreMigrateRequest!=null)
                PreMigrateRequest(this, new EventArgs());
        }
        private void OnMigrateRequest(MigrateRequestEventArgs e)
        {
            if (MigrateRequest != null)
                MigrateRequest(this, e);
        }

        #endregion

        #region Constructor

        public ConnectedHost(ConnectionManager manager)
        {
            logger = manager.Logg;
            this.connectionManager = manager;
            this.Dispatcher = new Dispatcher.Dispatcher(this);
            connectionManager.MessageReceived += connectionManager_MessageReceived;
            Bot = new BotControler(this, manager.Logg);
            Trajet = new TrajetControler(this);
            account = new Model.Authentification.AccountInformationsModel(this);
        }

        #endregion

        void connectionManager_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Dispatcher.DispatchMessage(e.Message, this);
        }

        public void SendMessage(NetworkMessage message)
        {
            connectionManager.SendMessage(message, DestinationEnum.SERVER);
        }

        public void SendMessage(NetworkMessage message, DestinationEnum destination)
        {
            connectionManager.SendMessage(message, destination);
        }
         public void Start()
        {
            connectionManager.Start();
        }
        public void PreMigrate()
        {
            OnOnPreMigrateRequest();
        }
        public void Migrate(Socket acceptedSocket, IPAddress adress, int port)//Migrate method for mitm
        {
            OnMigrateRequest(new MigrateRequestEventArgs(acceptedSocket, port, adress));
        }

        public void Close()
        {
            OnCloseRequest();
        }
    }
}