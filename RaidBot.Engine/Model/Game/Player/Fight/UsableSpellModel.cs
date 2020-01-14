using RaidBot.Engine.Frames.Game.World.Fight;
using RaidBot.Engine.Model.Game.Player.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.Player.Fight
{
   public class UsableSpellModel
    {
       public SpellData Spell;
       public CibleEnum Cible;
       public int Priority;
       public UsableSpellModel(SpellData spell , CibleEnum cible , int priority)
       {
           Spell = spell;
           Cible = cible;
           Priority = priority;
       }
    }
}
