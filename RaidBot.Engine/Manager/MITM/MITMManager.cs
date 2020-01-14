using RaidBot.Common.IO;
using RaidBot.Common.Network.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using RaidBot.Common.Default.Loging;
using RaidBot.Engine.Enums;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;

namespace RaidBot.Engine.Manager.MITM
{
    [Serializable()]
    public class MITMManager : ConnectionManager
    {
        public Client SourceClient;//socket connecter au client dofus
        public Client DestinationClient;//socket connecter au serveur dofus
        public bool IsMigrating = false;
        public MITMManager(Socket acceptedSocket, IPAddress adress, int port)
            : base()
        {
            Logg = new Logger();
            Host = new ConnectedHost(this);
            Host.CloseRequest += Host_CloseRequest;
            Host.MigrateRequest += Host_MigrateRequest;
            Host.PreMigrateRequest += Host_PreMigrateRequest;
            SourceClient = new Client(acceptedSocket, Logg);
            DestinationClient = new Client(adress, port, Logg);
            SourceClient.DataReceived += SourceClient_DataReceived;
            DestinationClient.DataReceived += DestinationClient_DataReceived;
            SourceClient.Disconnected +=SourceClient_Disconnected;
            DestinationClient.Disconnected +=DestinationClient_Disconnected;
        }
        void SourceClient_Disconnected(object sender, EventArgs e)
        {
            if(!IsMigrating)
            DestinationClient.Stop();
        }
        void DestinationClient_Disconnected(object sender, EventArgs e)
        {
            if (!IsMigrating)
            SourceClient.Stop();
        }
       
        void Host_PreMigrateRequest(object sender, EventArgs e)
        {
            IsMigrating = true;
            Host.Bot.BotState = BotStatsEnum.MIGRATING;
            SourceClient.Stop();
            DestinationClient.Stop();
            Logg.Log("Préparation de la migration ...");
            
        }

        void Host_CloseRequest(object sender, EventArgs e)
        {
            if (SourceClient.Runing)
                SourceClient.Stop();
            if (DestinationClient.Runing)
                DestinationClient.Stop();
            Host.Bot.BotState = BotStatsEnum.DISCONECTED;
        }

        void Host_MigrateRequest(object sender, MigrateRequestEventArgs e)
        {
            SourceClient = new Client(e.AcceptedSocket,Logg);
            
            DestinationClient = new Client(e.Adress, e.Port,Logg);
            SourceClient.DataReceived += SourceClient_DataReceived;
            DestinationClient.DataReceived += DestinationClient_DataReceived;
            Logg.Log(string.Format("Migration réussit avec succes, server de jeux : {0}: {1} .", e.Adress, e.Port), LogLevelEnum.Succes);
            Host.Bot.BotState = BotStatsEnum.INACTIF;
            IsMigrating = false;
        }

        void DestinationClient_DataReceived(object sender, Client.DataReceivedEventArgs e)
        {
       //   try
         //   {
           
                NetworkMessage message = ProtocolManager.GetPacket(e.Data.Data, (uint)e.Data.MessageId);
             //   Logg.Log("From server :" + message.ToString().Split('.').Reverse().First() + " -Id : " + message.MessageId + " -Lenght :" + message.Data.Length);
                if (Host.Dispatcher.DispatchMessage(message, Host))
                {
                    SourceClient.Send(e.Data);
                 //   Logg.Log ("Sent to client ----> " + message.ToString().Split('.').Reverse().First() + " -Id : "  + message.MessageId + " -Lenght : " + message.Data.Length); 
                }
                        
             //}
        //   catch
        //    {
          //      Logg.Log("Impossible de parser le packet : " + e.Data.MessageId.ToString() + " , " + e.Data.Length.ToString() + " bytes non lue", LogLevelEnum.Error);
         //       SourceClient.Send(e.Data);
         //   }
        }
		public void ping()
		{
			SendMessage(new BasicPingMessage(true) , DestinationEnum.SERVER);
		}
        void SourceClient_DataReceived(object sender, Client.DataReceivedEventArgs e)
        {
          //  try
          // {

                NetworkMessage message = ProtocolManager.GetPacket(e.Data.Data, (uint)e.Data.MessageId);
            //    Logg.Log("From client :" + message.ToString().Split('.').Reverse().First() + " -Id : " + message.MessageId + " -Lenght :" + message.Data.Length);
                if (Host.Dispatcher.DispatchMessage(message, Host))
                {
                DestinationClient.Send(e.Data);
            //    Logg.Log("Sent to server ----> " + message.ToString().Split('.').Reverse().First() + " -Id : " + message.MessageId + " -Lenght :"  + message.Data.Length); 
                }
               
            //}
           // catch
          //  {
           //     Logg.Log("Impossible de parser le packet : " + e.Data.MessageId.ToString() + " , " + e.Data.Length.ToString() + " bytes non lue", LogLevelEnum.Error);
          //      DestinationClient.Send(e.Data);
          //  }
        }
        public override void SendMessage(NetworkMessage message, DestinationEnum destination)
        {
            CustomDataWriter writer = new CustomDataWriter();
            message.Pack(writer); 
            switch (destination)
            {
                case DestinationEnum.CLIENT:
                    SourceClient.Send(writer.Data);
                    break;
                case DestinationEnum.SERVER:
                    DestinationClient.Send(writer.Data);
                    break;
            }
            Host.logger.Log("Sent " + message.ToString().Split('.').Reverse().First() + " to " + destination.ToString() , LogLevelEnum.Succes);
        }
        public override void Start() { }
        public void Dispose()
        {
            DestinationClient.Stop();
            SourceClient.Stop();
        }
    }
}