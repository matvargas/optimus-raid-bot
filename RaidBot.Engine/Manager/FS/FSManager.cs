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

namespace RaidBot.Engine.Manager.FS
{
     [Serializable()]
   public class FSManager : ConnectionManager
    {
        public Client DestinationClient;//socket connecter au serveur dofus
         public FSManager()
            : base()
        {
            Logg = new Logger();
            Host = new ConnectedHost(this);
			Host.IsFS = true;
            Host.CloseRequest += Host_CloseRequest;
            Host.MigrateRequest += Host_MigrateRequest;
            Host.PreMigrateRequest += Host_PreMigrateRequest;
            DestinationClient = new Client();
            DestinationClient.DataReceived += DestinationClient_DataReceived;

        }

         void DestinationClient_DataReceived(object sender, Client.DataReceivedEventArgs e)
         {
             NetworkMessage message = ProtocolManager.GetPacket(e.Data.Data, (uint)e.Data.MessageId);
             Host.Dispatcher.DispatchMessage(message, Host);
			 Logg.Log("From server :" + message.MessageId.ToString());
         }
         public override void SendMessage(NetworkMessage message, DestinationEnum destination)
         {
             CustomDataWriter writer = new CustomDataWriter();
             message.Pack(writer);
             DestinationClient.Send(writer.Data);
			Logg.Log("Sent to server :" + message.MessageId.ToString());
         }
         void Host_PreMigrateRequest(object sender, EventArgs e)
         {
             Host.Bot.BotState = BotStatsEnum.MIGRATING;
             DestinationClient.Stop();
             Logg.Log("Préparation de la migration ...");
         }

         void Host_CloseRequest(object sender, EventArgs e)
         {
             if (DestinationClient.Runing)
                 DestinationClient.Stop();
             Host.Bot.BotState = BotStatsEnum.DISCONECTED;
         }

         void Host_MigrateRequest(object sender, MigrateRequestEventArgs e)
         {
             DestinationClient = new Client(e.Adress, e.Port, Logg);
             DestinationClient.DataReceived += DestinationClient_DataReceived;
             Logg.Log(string.Format("Migration réussit avec succes, server de jeux : {0}: {1} .", e.Adress, e.Port), LogLevelEnum.Succes);
             Host.Bot.BotState = BotStatsEnum.INACTIF;
         }
         public override void Start()
         {
             DestinationClient.Start(IPAddress.Parse(Setting.RemoteSetting.Default.LoginConnectionAdresse), Setting.RemoteSetting.Default.LoginListenPort);
         }
    }
}
