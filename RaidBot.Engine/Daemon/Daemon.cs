using Raidbot.Protocol.Messages;
using RaidBot.Common.Default.Loging;
using RaidBot.Common.IO;
using RaidBot.Common.Network.Client;
using RaidBot.Common.Network.Server;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Daemon
{
    public class Daemon
    {
        class ClientHandle
        {
            public Client Connection { get; set; }
            public RawDataMessage Rdm { get; set; }
            public BasicLatencyStatsRequestMessage LatencyReqest { get; set; }

            public ClientHandle(Client conn)
            {
                Connection = conn;
            }

        }
        private Dispatcher.Dispatcher mDispatcher;
        private Server mServer;
        private Server mBotServer;
        private Client slave;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<EventArgs> Ready;

        Dictionary<Client, ClientHandle> Clients;
        ClientHandle currentHandled = null;


        private void OnReady()
        {
            if (Ready != null)
                Ready(this, new EventArgs());
        }

        public Daemon()
        {
            Clients = new Dictionary<Client, ClientHandle>();
            mServer = new Server();
            mBotServer = new Server();
            mServer.ConnectionAccepted += MServer_ConnectionAccepted;
            mBotServer.ConnectionAccepted += MBotServer_ConnectionAccepted;
            MessageReceived += Daemon_MessageReceived;
            mServer.Start(5555);
        }

        private void MBotServer_ConnectionAccepted(object sender, System.Net.Sockets.Socket acceptedSocket)
        {
            Console.WriteLine("New client connected ...");
            Client cl = new Client();
            cl.DataReceived += (s, e) =>
            {
                Console.WriteLine("Client say something ?");
                if (e.Data.MessageId == RawDataMessage.Id)
                {
                    Console.WriteLine("Client request for rdm ...");
                    ClientHandle ha = Clients[(Client)s];
                    RawDataMessage msg = (RawDataMessage)ProtocolManager.GetPacket(e.Data.Data, (uint)e.Data.MessageId);
                    msg.Deserialize(new CustomDataReader(msg.Data));
                    ha.Rdm = msg;
                    if (currentHandled == null)
                    {
                        Console.WriteLine("No pending request, executing directly !");
                        currentHandled = ha;
                        SendMessage(ha.Rdm);
                        ha.Rdm = null;
                    } else
                    {
                        Console.WriteLine("Pushed to the clients queue");
                    }
                }
            };
            cl.IsNewProtocol = false;
            ClientHandle handle = new ClientHandle(cl);
            Clients.Add(handle.Connection, handle);
            cl.Start(acceptedSocket);
        }

        private void Daemon_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Slave send " + e.Msg.GetType().Name);
            if (e.Msg is CheckIntegrityMessage || e.Msg is BasicLatencyStatsMessage)
            {
                Console.WriteLine("Resending to current client ...");
                CustomDataWriter st = new CustomDataWriter();
                e.Msg.PackOld(st);
                currentHandled.Connection.Send(st.Data);
                currentHandled.Connection.Stop();
                Clients.Remove(currentHandled.Connection);
                currentHandled = null;
                if (Clients.Count > 0)
                {
                    Console.WriteLine("Executing pending request ...");
                    currentHandled = Clients.First().Value;
                    SendMessage(Clients.First().Value.Rdm);
                }
            }
        }

        private void MServer_ConnectionAccepted(object sender, System.Net.Sockets.Socket acceptedSocket)
        {
            Console.WriteLine("Slave client connected !");
            slave = new Client(acceptedSocket, new Logger(), true);
            mDispatcher = new Dispatcher.Dispatcher();
            slave.Disconnected += Slave_Disconnected;
            slave.DataReceived += Slave_DataReceived;
            SendMessage(new ProtocolRequired().InitProtocolRequired(1878, 1890));
            OnReady();
            mServer.Stop();
            mBotServer.Start(4242);
        }

        private void Slave_Disconnected(object sender, Client.DisconnectedEventArgs e)
        {
            mServer.Start(5555);
        }

        public void SendMessage(NetworkMessage message)
        {
            var st = new CustomDataWriter();
            message.PackOld(st);
            slave.Send(st.Data);
        }

        private void Slave_DataReceived(object sender, Client.DataReceivedEventArgs e)
        {
            NetworkMessage msg = ProtocolManager.GetPacket(e.Data.Data, (uint)e.Data.MessageId);
            msg.Deserialize(new CustomDataReader(msg.Data));
            MessageReceived(this, new MessageReceivedEventArgs(msg));
        }


        public class MessageReceivedEventArgs : EventArgs
        {
            public NetworkMessage Msg;
            public MessageReceivedEventArgs(NetworkMessage msg)
            {
                Msg = msg;
            }

        }
    }
}
