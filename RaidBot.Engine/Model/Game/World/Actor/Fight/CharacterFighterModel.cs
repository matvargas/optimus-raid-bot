using RaidBot.Data.GameData;
using RaidBot.Engine.Manager;
using RaidBot.Engine.Model.Game.World.Actor.Fight;
using RaidBot.Engine.Model.Game.World.Fight;
using RaidBot.Protocol.DataCenter;
using RaidBot.Protocol.Enums;
using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Fight
{
    public class CharacterFighterModel : FighterModel
    {
        public string Name;
        public int Level;
        public GameFightMinimalStats Stats;
        public Breed Breed;

        public CharacterFighterModel(int contextualId, short CellId, TeamEnum side, RaidFight fight)
            : base(contextualId, CellId, side, fight)
        {
            
        }
        
    }
}
