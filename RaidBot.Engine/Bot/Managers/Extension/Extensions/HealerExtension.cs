using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Managers.Extension.Extensions
{
    [ExtensionHandler(ExtensionHandlerAttribute.PriorityEnum.VeryHigh)]
    public class HealerExtension : Extension
    {
        public HealerExtension(Brain bot) : base(bot)
        {
        }

        [ExtensionHandler(ExtensionHandlerAttribute.PriorityEnum.VeryHigh)]
        public async override Task RpLoaded()
        {
            if (Brain.ExtManager.Trajet.Get() != null && Brain.ExtManager.Trajet.Get().Globals.Get("REGEN_END") != null)
            {
                int RegenPer = (int)Brain.ExtManager.Trajet.Get().Globals.Get("REGEN_END").Number;
                CharacterCharacteristicsInformations info = Brain.State.Player.Characteristics.Get();
                if (info != null && (((info.LifePoints > 0 ? info.LifePoints : 1) * 1000) / ((info.MaxLifePoints * 1000) / 100) < RegenPer))
                {
                    Log("Begin regen ...");
                    while (((info.LifePoints > 0 ? info.LifePoints : 1) * 1000) / ((info.MaxLifePoints * 1000) / 100) < RegenPer)
                    {
                        await Task.Delay(1000);
                    }
                    Log("Regen end !");
                }
            }

        }
    }
}
