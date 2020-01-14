using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using RaidBot.Common.Default.Loging;
using RaidBot.Common.IO;

namespace RaidBot.Common.Network.Client
{
    public class Client
    {
        #region Declarations

        public bool IsNewProtocol { get; set; } // Not well named, if it's true when we read packed we also read aditional packed counter (only for mitm or slave client)
        private Socket clientSocket;
        public bool Runing { get; private set; }
        private byte[] sendBuffer, receiveBuffer;
        private BigEndianReader buffer;
        const int bufferLength = 8192;
        private Buffer currentMessage;
        private Logger Logg;

        private short sequence = 1;
        public short NextSequence()
        {
            return sequence++;
        }

        public Latency Latency { get; private set; }

        public bool IsConnected
        {
            get
            {
                return clientSocket.Connected;
            }
        }

        #endregion

        #region Constructeur

        public Client(IPAddress ip, int port, Logger logg)
        {
            IsNewProtocol = false;
            this.Logg = logg;
            Init();
            Start(ip, port);
        }

        public Client(Socket socket, Logger logg, bool newProtocol)
        {
            IsNewProtocol = newProtocol;
            this.Logg = logg;
            Init();
            Start(socket);
        }

        public Client()
        {
            Init();
        }

        #endregion

        #region Methode publique


        public void Start(Socket socket)
        {
            Latency = new Latency();
            Runing = true;
            clientSocket = socket;
            clientSocket.BeginReceive(receiveBuffer, 0, bufferLength, SocketFlags.None, new AsyncCallback(ReceiveCallBack), clientSocket);
        }

        public void Start(IPAddress adress, int port)
        {
            Latency = new Latency();
            clientSocket.BeginConnect(adress, port, new AsyncCallback(ConnectionCallBack), clientSocket);
        }

        public void Stop()
        {
            clientSocket.BeginDisconnect(false, DisconectedCallBack, clientSocket);
        }

        public void Send(byte[] data)
        {
            Latency.Send();
            if (clientSocket.Connected == false)
                Runing = false;
            if (Runing)
            {
                if (data.Length == 0)
                    return;
                sendBuffer = data;
                clientSocket.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), clientSocket);
            }
            else
                Logg.Log("Send " + data.Length + " bytes but not runing", LogLevelEnum.CriticalError);
        }

        public void Send(Buffer message, bool withCounter)
        {
            BigEndianWriter writer = new BigEndianWriter();
            byte typeLen;//get the size of the size
            if (message.Data.Length > 65535)
                typeLen = 3;
            else if (message.Data.Length > 255)
                typeLen = 2;
            else if (message.Data.Length > 0)
                typeLen = 1;
            else
                typeLen = 0;

            writer.WriteShort((short)(message.MessageId << 2 | typeLen)); //write id and size of size
            if (withCounter)
                writer.WriteUInt(message.Count);

            switch (typeLen)//write the size
            {
                case 0:
                    break;
                case 1:
                    writer.WriteByte((byte)message.Data.Length);
                    break;
                case 2:
                    writer.WriteShort((short)message.Data.Length);
                    break;
                case 3:
                    writer.WriteByte((byte)(message.Data.Length >> 16 & 255));
                    writer.WriteShort((short)(message.Data.Length & 65535));
                    break;
            }
            writer.WriteBytes(message.Data);//write the packet after write the header
            Send(writer.Data);
        }

        #endregion

        #region Methode privée

        private void Init()
        {
            buffer = new BigEndianReader();
            receiveBuffer = new byte[bufferLength];
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void ThreatBuffer()
        {
            if (currentMessage == null)
                currentMessage = new Buffer();
            long pos = buffer.Position;
            if (currentMessage.Build(buffer, IsNewProtocol))
            {
                OnDataReceived(new DataReceivedEventArgs(currentMessage));
                currentMessage = null;
                Latency.UpdateLatency();
                ThreatBuffer();
            }
        }

        #endregion

        #region CallBack

        private void ConnectionCallBack(IAsyncResult asyncResult)
        {
            Runing = true;
            Socket client = (Socket)asyncResult.AsyncState;
            client.EndConnect(asyncResult);
            if (client.Connected)
            {
                client.BeginReceive(receiveBuffer, 0, bufferLength, SocketFlags.None, new AsyncCallback(ReceiveCallBack), client);
                OnConnected(new ConnectedEventArgs());
            }
            else
                OnDisconnected(new DisconnectedEventArgs());
        }

        private void DisconectedCallBack(IAsyncResult asyncResult)
        {
            Runing = false;
            Socket client = (Socket)asyncResult.AsyncState;
            client.EndDisconnect(asyncResult);
            OnDisconnected(new DisconnectedEventArgs());
        }

        private void ReceiveCallBack(IAsyncResult asyncResult)
        {
            Socket client = (Socket)asyncResult.AsyncState;
            if (client.Connected == false)
            {
                Runing = false;
                return;
            }
            if (Runing)
            {
                int bytesRead = 0;
                bytesRead = client.EndReceive(asyncResult);
                if (bytesRead == 0)
                {
                    Runing = false;
                    OnDisconnected(new DisconnectedEventArgs());
                    return;
                }
                byte[] data = new byte[bytesRead];
                Array.Copy(receiveBuffer, data, bytesRead);
                buffer.Add(data, 0, data.Length);
                ThreatBuffer();
                client.BeginReceive(receiveBuffer, 0, bufferLength, SocketFlags.None, new AsyncCallback(ReceiveCallBack), client);
            }
            else
                Logg.Log("Receive data but not running", LogLevelEnum.Error);
        }

        private void SendCallBack(IAsyncResult asyncResult)
        {
            if (Runing == true)
            {
                Socket client = (Socket)asyncResult.AsyncState;
                client.EndSend(asyncResult);
                OnDataSended(new DataSendedEventArgs());
            }
            else
                Logg.Log(string.Format("Send data but not runing"), LogLevelEnum.CriticalError);
        }

        #endregion

        #region Evenement

        public event EventHandler<ConnectedEventArgs> Connected;
        public event EventHandler<DisconnectedEventArgs> Disconnected;
        public event EventHandler<DataReceivedEventArgs> DataReceived;
        public event EventHandler<DataSendedEventArgs> DataSended;

        private void OnConnected(ConnectedEventArgs e)
        {
            if (Connected != null)
                Connected(this, e);
        }

        private void OnDisconnected(DisconnectedEventArgs e)
        {
            if (Disconnected != null)
                Disconnected(this, e);
        }

        private void OnDataReceived(DataReceivedEventArgs e)
        {
            if (DataReceived != null)
                DataReceived(this, e);
        }

        private void OnDataSended(DataSendedEventArgs e)
        {
            if (DataSended != null)
                DataSended(this, e);
        }

        #endregion

        #region EventArgs

        public class ConnectedEventArgs : EventArgs
        {
        }

        public class DisconnectedEventArgs : EventArgs
        {
        }

        public class DataSendedEventArgs : EventArgs
        {
        }

        public class DataReceivedEventArgs : EventArgs
        {
            public Buffer Data { get; private set; }

            public DataReceivedEventArgs(Buffer data)
            {
                Data = data;
            }
        }

        #endregion

    }
}