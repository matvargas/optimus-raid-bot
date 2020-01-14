using RaidBot.Protocol.Enums;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Bot.Routines
{
    public class Fighter : Routine
    {
        Random rnd = new Random();

        public Fighter(Brain brain) : base(brain)
        {
        }


        public async Task<bool> AttackMonster(int depth)
        {
            if (depth > 0)
                await Task.Delay(depth * 300);
            if (depth > 3)
            {
                Error("Max retry attemp, waiting for something LOL");
                return true;
            }
            if (Brain.ElementsManager.GroupMonster.Get().Count > depth && await Brain.PlayerManager.AttackGroupMonster(Brain.ElementsManager.GroupMonster.Get().Values.ElementAt(depth)))
            {
                Log("Group monsters attacked ...");
                return true;
            }
            else
            {
                Log("No monsters avaiable, goto random map");
            }
            return false;
        }
    }
}
