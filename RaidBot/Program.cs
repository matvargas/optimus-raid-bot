

using RaidBot.Common.Default.Loging;
using RaidBot.Common.IO;
using RaidBot.Common.Network.Client;
using RaidBot.Common.Network.Server;
using RaidBot.Daemon;
using RaidBot.Data.IO.D2P;
using RaidBot.Data.IO.D2P.File;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using RaidBot.Protocol;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using ServiceStack.Text;
using System.Windows.Forms;

namespace RaidBot
{
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


    class MainClass
    {
        public class Sniffer
        {
            Server srv;
            Client clientLocal;
            Client clientRemote;
            public Logger log = new Logger();
            public Sniffer()
            {
                srv = new Server();
                srv.ConnectionAccepted += Srv_ConnectionAccepted;
                srv.Start(5555);
            }

            String ForcedAddr = null;
            int ForcedPort = 0;
            private void Srv_ConnectionAccepted(object sender, System.Net.Sockets.Socket acceptedSocket)
            {
                clientLocal = new Client(acceptedSocket, new Logger(), true);
                clientRemote = new Client();
                clientRemote.DataReceived += ClientRemote_DataReceived;
                clientLocal.DataReceived += ClientLocal_DataReceived;
                if (ForcedAddr != null)
                {
                    clientRemote.Start(IPAddress.Parse(ForcedAddr), ForcedPort);
                    ForcedAddr = null;
                }
                else
                    clientRemote.Start(IPAddress.Parse("52.17.231.202"), 5555);
            }

            private void ClientLocal_DataReceived(object sender, Client.DataReceivedEventArgs e)
            {
                NetworkMessage msg = ProtocolManager.GetPacket(e.Data.Data, (uint)e.Data.MessageId);
                try
                {
                    msg.Deserialize(new CustomDataReader(msg.Data));
                    log.LogObj(new PacketLogContainer(msg, false));
                }
                catch
                {
                    Console.WriteLine("> can't be deserialized {0}", msg.GetType().Name);
                }
                clientRemote.Send(e.Data, true);
            }

            private void ClientRemote_DataReceived(object sender, Client.DataReceivedEventArgs e)
            {

                NetworkMessage msg = ProtocolManager.GetPacket(e.Data.Data, (uint)e.Data.MessageId);
                try
                {
                    msg.Deserialize(new CustomDataReader(msg.Data));
                    log.LogObj(new PacketLogContainer(msg, true));
                }
                catch
                {
                    Console.WriteLine("< can't be deserialized {0}", msg.GetType().Name);
                    log.LogObj(new PacketLogContainer(msg, true));
                }

                if (msg is SelectedServerDataMessage || msg is SelectedServerDataExtendedMessage)
                {
                    msg.Deserialize(new CustomDataReader(msg.Data));
                    SelectedServerDataMessage ssdm = (SelectedServerDataMessage)msg;
                    ForcedAddr = ssdm.Address;
                    ForcedPort = ssdm.Ports[0];
                    ssdm.Address = "127.0.0.1";
                    ssdm.Ports = new int[] { 5555 };
                    CustomDataWriter wr = new CustomDataWriter();
                    ssdm.PackOld(wr);
                    clientLocal.Send(wr.Data);
                    clientLocal.Stop();
                    clientRemote.Stop();
                    Console.WriteLine("Migrating to game server");
                }
                else
                    clientLocal.Send(e.Data, false);
            }

            private void Dump(NetworkMessage msg)
            {
                msg.PrintDump();
            }
        }

        static Process process;
        [STAThread]
        public static void Main(string[] args)
        {
            Sniffer sniff = new Sniffer();
            process = new Process();
            process.StartInfo.FileName = Path.Combine(Properties.Settings.Default.DofusPath, "Dofus.exe");
            process.Start();
            process.WaitForInputIdle();
            System.Threading.Thread.Sleep(2000);
            WinsockHook h = new WinsockHook(process);
            h.SourceIPs = new List<System.Net.IPAddress>
            {
                System.Net.IPAddress.Parse("34.252.21.81"),
                System.Net.IPAddress.Parse("52.17.231.202"),
            };
            h.RemoteIP = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 5555);
            h.AllowReplace = true;
            h.Hook();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Main frm = new Main();
            frm.log = sniff.log;
            ApplicationContext applicationContext = new ApplicationContext(frm);
            Application.Run(applicationContext);


        }
    }
}
