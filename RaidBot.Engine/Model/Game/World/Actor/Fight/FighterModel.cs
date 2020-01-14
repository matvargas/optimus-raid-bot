using RaidBot.Engine.Functionality.Fight;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Model.Game.World.Fight;
using RaidBot.Engine.Utility.Pathfinding;
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
    public class FighterModel : ActorModel
    {
      
      public  RaidFight Fight;
      public TeamEnum Ennemies
      {
          get
          {
              if (this.Team == TeamEnum.TEAM_CHALLENGER )
                  return TeamEnum.TEAM_DEFENDER;
              else
                  return Team;
          }
      }

        public  TeamEnum Team;
		public bool Alive = true;
      
        public FighterModel(int contextualId , short CellId , TeamEnum side , RaidFight fight)
           :base(CellId)
        {
            Team = side;
            Fight = fight;
            base.ContextualId = contextualId;
        }
     
    }
}
