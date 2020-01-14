using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Managers.Extension.Extensions
{
    public class Extension : Manager
    {
        public Extension(Brain brain) : base(brain)
        {
        }

        public async virtual Task RpLoaded()
        {

        }

        public async virtual Task FightLoaded()
        {

        }

        public async virtual Task FightEnded()
        {

        }
    }

    public class ExtensionHandlerAttribute : Attribute
    {
        public enum PriorityEnum
        {
            VeryLow,
            Low,
            Normal,
            High,
            VeryHigh,
        }

        public PriorityEnum Priority { get; set; }

        public ExtensionHandlerAttribute(PriorityEnum priority)
        {
            Priority = priority;
        }

        public ExtensionHandlerAttribute()
        {
            Priority = PriorityEnum.Normal;
        }
    }
}
