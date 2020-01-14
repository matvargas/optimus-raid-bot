using RaidBot.Engine.Enums;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Manager
{
    public class MessageReceivedEventArgs
    {
        public NetworkMessage Message { get; private set; }
        public DestinationEnum Source { get; private set; }

        public MessageReceivedEventArgs(NetworkMessage message,DestinationEnum source)
        {
            Message = message;
            Source = source;
        }
    }
}
