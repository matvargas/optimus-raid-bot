using RaidBot.Common.Default.Loging;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Dispatcher
{
    [Serializable()]
    class MessageDelegateDefinition:MarshalByRefObject
    {
        public event EventHandler MessageReSended;
        public MethodInfo Method { get; private set; }
        public bool IsMITMMessage { get; private set; }
        private object Instance;

        public MessageDelegateDefinition(MethodInfo method,object instance)
        {
            IsMITMMessage = false;
            Method = method;
            if (Method.ReturnType == typeof(bool))
            {
                IsMITMMessage = true;
            }
            else
            {
                IsMITMMessage = false;
            }
            Logger.Default.Log(string.Format("Method : " + Method.ToString() + " added to dispatcher"));
        }

        public bool Handle(NetworkMessage message)
        {
            if(IsMITMMessage)
            {
                return (bool)Method.Invoke(Instance, new object[] { message });
            }
            else
            {
                Method.Invoke(Instance, new object[] { message });
                return true;
            }
        }

        public void ReSend()
        {
            OnMessageReSended();
        }

        private void OnMessageReSended()
        {
            if (MessageReSended != null)
                MessageReSended(this, new EventArgs());
        }
    }
}
