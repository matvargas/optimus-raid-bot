using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace RaidBot.Common.Network.Server
{
    public class Server
    {
        public static Server Default = new Server();
        private Socket socketListener;
        public bool Runing { get; private set; }

        public delegate void ConnectionAcceptedDelegate(object sender, Socket acceptedSocket);
        public event ConnectionAcceptedDelegate ConnectionAccepted;
        private void OnConnectionAccepted(Socket client)
        {
            if (ConnectionAccepted != null)
                ConnectionAccepted(this, client);
        }

        public Server()
        {
            if (Runing)
            {
                Runing = false;
                socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                RaidBot.Common.Default.Loging.Logger.Default.Log("Serveur arrêté .");
            }
            else
                RaidBot.Common.Default.Loging.Logger.Default.Log("Serveur déjà arrêté...", RaidBot.Common.Default.Loging.LogLevelEnum.Error);
        }

        public void Start(short listenPort)
        {
            if (!Runing)
            {
                Runing = true;
                socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try {
                    socketListener.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), listenPort));
                    socketListener.Listen(5);
                    socketListener.BeginAccept(BeiginAcceptCallBack, socketListener);
                    RaidBot.Common.Default.Loging.Logger.Default.Log("Listening Server");
                } 
                catch (Exception e) {
                    Console.WriteLine("Winsock error: " + e.ToString());
                }
            }
            else
                RaidBot.Common.Default.Loging.Logger.Default.Log("Server already listening ..." , RaidBot.Common.Default.Loging.LogLevelEnum.Error);
        }

        public void Listen()
        {
            socketListener.BeginAccept(BeiginAcceptCallBack, socketListener);
        }

        public void Stop()
        {
            Runing = false;
            socketListener.Close(50);
        }

        private void BeiginAcceptCallBack(IAsyncResult result)
        {
            if (Runing)
            {
                Socket listener = (Socket)result.AsyncState;
                Socket acceptedSocket = listener.EndAccept(result);               
                listener.BeginAccept(BeiginAcceptCallBack, listener);
                OnConnectionAccepted(acceptedSocket);
            }
        }
    }
}