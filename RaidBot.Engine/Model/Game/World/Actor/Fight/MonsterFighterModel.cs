using RaidBot.Data.GameData;
using RaidBot.Engine.Functionality.Fight;
using RaidBot.Engine.Model.Game.World.Fight;
using RaidBot.Protocol.DataCenter;
using RaidBot.Protocol.Enums;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.World.Actor.Fight
{
    public class MonsterFighterModel : FighterModel
    {

        public MonsterFighterModel(int ContextualId , short CellId, TeamEnum side  ,RaidFight fght)
            : base(ContextualId , CellId , side , fght)
        { 
  
        }      
    }
}
