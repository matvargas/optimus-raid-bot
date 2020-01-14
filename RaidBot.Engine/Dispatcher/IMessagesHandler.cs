using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Engine.Bot;
using RaidBot.Protocol.Messages;
namespace RaidBot.Engine.Dispatcher
{
    public interface IMessagesHandler
    {
        bool ContainsType(Type t);
        void LoadMessagesHandler();
        void UnLoadMessagesHandler();
        bool DispatchMessage(NetworkMessage message);
        GameContext Context { get; set; }
        Dictionary<Type, MethodInfo> Methods { get; set; }
    }

    public enum GameContext
    {
        NONE,
        LOGIN,
        GAME,
        ROLEPLAY,
        FIGHT
    }
}
