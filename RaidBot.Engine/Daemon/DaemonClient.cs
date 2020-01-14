using RaidBot.Common.IO;
using RaidBot.Common.Network.Client;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static RaidBot.Engine.Daemon.Daemon;

namespace RaidBot.Engine.Daemon
{
    public class DaemonClient
    {
        static DaemonClient instance = new DaemonClient("127.0.0.1", 4242);
        public static DaemonClient GetInstance()
        {
            return instance;
        }

        short port;
        String ip;
        Client client;
        byte[] currentRequest;

        private Mutex mutex = new Mutex();

        public EventHandler<MessageReceivedEventArgs> MessageReceived;
        private void OnMessageReceived(NetworkMessage msg)
        {
            if (MessageReceived != null)
                MessageReceived(this, new MessageReceivedEventArgs(msg));
        }

        public DaemonClient(String ip, short port)
        {
            this.port = port;
            this.ip = ip;
        }

        public bool Request(RawDataMessage msg)
        {
            mutex.WaitOne();
            try
            {
                client = new Client(IPAddress.Parse(this.ip), port, new Common.Default.Loging.Logger());
                CustomDataWriter writer = new CustomDataWriter();
                client.DataReceived += Client_DataReceived;
                msg.PackOld(writer);
                client.Send(writer.Data);
                mutex.ReleaseMutex();
                return true;
            }
            catch
            {
                Console.WriteLine("Can't contact daemon !");
                mutex.ReleaseMutex();
                return false;
            }
        }

        private void Client_DataReceived(object sender, Client.DataReceivedEventArgs e)
        {
            Console.WriteLine("Daemon say");
            if (e.Data.MessageId == HelloConnectMessage.Id && currentRequest != null && currentRequest.Length > 0)
            {
                Console.WriteLine("Sening request to daemon ...");
            }
            mutex.WaitOne();
            OnMessageReceived(ProtocolManager.GetPacket(e.Data.Data, (uint)e.Data.MessageId));
            mutex.ReleaseMutex();
        }
    }
}
