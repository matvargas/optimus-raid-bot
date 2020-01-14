using RaidBot.Common.Default.Loging;
using RaidBot.Engine.Enums;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Manager
{
    public  abstract class ConnectionManager
    {
        public Logger Logg;
        public ConnectedHost Host { get;  set; }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public BotStatsEnum ConnectionState { get; private set; }
        private void OnMessageReceived(MessageReceivedEventArgs e)
        {
            if (MessageReceived != null)
                MessageReceived(this, e);
        }
        public abstract void SendMessage(NetworkMessage message, DestinationEnum destination);
        public abstract void Start(); // For full sokcet
      
    }
}
