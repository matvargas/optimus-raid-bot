using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.World.Actor.Characters
{
    public class CharacterModel:ActorModel
    {
        private string mName;
        public string Name
        {
            get { return mName; }
            set
            {
                mName = value;
                Notify();
            }
        }
        public CharacterModel(GameRolePlayCharacterInformations message)
            :base(message)
        {
            Name = message.name;
        }
    }
}
