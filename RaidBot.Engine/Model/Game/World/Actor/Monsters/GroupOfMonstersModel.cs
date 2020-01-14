using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.World.Actor.Monsters
{
   public class GroupOfMonstersModel:ActorModel
    {
        public GroupOfMonstersModel(GameRolePlayGroupMonsterInformations message)
            :base(message)
        {
            KeyRingBonus = message.keyRingBonus;
            HasHardcoreDrop = message.hasHardcoreDrop;
            HasAVARewardToken = message.hasAVARewardToken;
            AgeBonus = message.ageBonus;
            AlignmentSide = message.alignmentSide;
            Monsters = new List<MonsterModel>();
            foreach (var msg in message.staticInfos.underlings)
                this.Monsters.Add(new MonsterModel(msg));
        }
        private MonsterModel m_MainMonster;

        public virtual MonsterModel MainMonster
        {
            get
            {
                return m_MainMonster;
            }
            set
            {
               m_MainMonster = value;
            }
        }
        private List<MonsterModel> m_Monsters;

        public virtual List<MonsterModel> Monsters
        {
            get
            {
                return m_Monsters;
            }
            set
            {
                m_Monsters = value;
            }
        }
        private bool m_keyRingBonus;

        public virtual bool KeyRingBonus
        {
            get
            {
                return m_keyRingBonus;
            }
            set
            {
                m_keyRingBonus = value;
            }
        }

        private bool m_hasHardcoreDrop;

        public virtual bool HasHardcoreDrop
        {
            get
            {
                return m_hasHardcoreDrop;
            }
            set
            {
                m_hasHardcoreDrop = value;
            }
        }

        private bool m_hasAVARewardToken;

        public virtual bool HasAVARewardToken
        {
            get
            {
                return m_hasAVARewardToken;
            }
            set
            {
                m_hasAVARewardToken = value;
            }
        }

        private GroupMonsterStaticInformations m_staticInfos;

        public virtual GroupMonsterStaticInformations StaticInfos
        {
            get
            {
                return m_staticInfos;
            }
            set
            {
                m_staticInfos = value;
            }
        }

        private short m_ageBonus;

        public virtual short AgeBonus
        {
            get
            {
                return m_ageBonus;
            }
            set
            {
                m_ageBonus = value;
            }
        }

        private byte m_lootShare;

        public virtual byte LootShare
        {
            get
            {
                return m_lootShare;
            }
            set
            {
                m_lootShare = value;
            }
        }

        private sbyte m_alignmentSide;

        public virtual sbyte AlignmentSide
        {
            get
            {
                return m_alignmentSide;
            }
            set
            {
                m_alignmentSide = value;
            }
        }
    }
}
