using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Engine.Bot;
using RaidBot.Protocol.Messages;
namespace RaidBot.Engine.Dispatcher
{
    public class MessageInitializedEventArgs:EventArgs
    {
        public NetworkMessage Message { get; private set; }
        public Brain mHost;
        public MessageInitializedEventArgs(NetworkMessage message , Brain Host)
        {
            Message = message;
            mHost = Host;
        }
    }
}
