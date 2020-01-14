using MoonSharp.Interpreter;
using RaidBot.Engine.Bot.Data;
using RaidBot.Engine.Bot.Managers.GameContext;
using RaidBot.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Extension.Context
{
    [MoonSharpUserData]
    public class RolePlayExtensionContext : ExtensionContext
    {

        public void UseCharac(int charac, uint point)
        {
            Brain.PlayerManager.UseCharacPoint((BoostableCharacteristicEnum)charac, point);
        }

        public PlayerManager Player
        {
            get
            {
                return Brain.PlayerManager;
            }
        }

        public RolePlayExtensionContext(Brain brain) : base(brain)
        {
        }
    }
}
