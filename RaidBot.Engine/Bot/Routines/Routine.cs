using RaidBot.Engine.Bot.Managers;
using RaidBot.Engine.Dispatcher;
using RaidBot.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Routines
{
    public class Routine : Manager, IMessagesHandler
    {
        public Routine(Brain brain) : base(brain)
        {
            Context = GameContext.NONE;
            LoadMessagesHandler();
            brain.Dispatcher.Register(this);
        }


        ~Routine()
        {
            UnLoadMessagesHandler();
        }

        public Dictionary<Type, MethodInfo> Methods { get; set; }
        public GameContext Context { get; set; }

        public bool DispatchMessage(NetworkMessage message)
        {
            if (Methods.ContainsKey(message.GetType()))
            {
                if (Methods[message.GetType()].ReturnType == typeof(bool))
                    return (bool)Methods[message.GetType()].Invoke(this, new object[] { message });
                else
                    Methods[message.GetType()].Invoke(this, new object[] { message });
            }
            return true;

        }

        public bool ContainsType(Type t)
        {
            return Methods.ContainsKey(t);
        }

        public void LoadMessagesHandler()
        {
            Methods = new Dictionary<Type, MethodInfo>();
            MethodInfo[] methods = this.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            foreach (var method in methods)
            {
                MessageHandlerAttribut[] attributes = method.GetCustomAttributes<MessageHandlerAttribut>().ToArray();
                if (attributes.Length != 0)
                {
                    Methods.Add(attributes[0].MessageType, method);
                }
            }
        }

        public void UnLoadMessagesHandler()
        {

        }
    }
}
