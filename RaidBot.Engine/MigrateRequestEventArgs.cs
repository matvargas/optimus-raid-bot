using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace RaidBot.Engine
{
    public class MigrateRequestEventArgs:EventArgs
    {
        public Socket AcceptedSocket { get; private set; }
        public IPAddress Adress { get; private set; }
        public int Port { get; private set; }
        public MigrateRequestEventArgs(Socket acceptedSocket,int port, IPAddress adress)//for mitm
        {
            Adress = adress;
            AcceptedSocket = acceptedSocket;
            Port = port;
        }
        
        public MigrateRequestEventArgs(IPAddress adress,int port)//for full socket
        {
            Adress = adress;
            Port = port;
        }
    }
}
