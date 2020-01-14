using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Extension.Context
{
    [MoonSharpUserData]
    public class FightExtensionContext : ExtensionContext
    {
        public FightExtensionContext(Brain brain) : base(brain)
        {
        }
    }
}
