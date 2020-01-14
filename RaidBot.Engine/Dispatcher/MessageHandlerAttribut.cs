using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Dispatcher
{
    public class MessageHandlerAttribut:Attribute
    {
        public Type MessageType { get; private set; }
        public MessageHandlerAttribut(Type messageType)
        {
            MessageType = messageType;
        }
    }
}
