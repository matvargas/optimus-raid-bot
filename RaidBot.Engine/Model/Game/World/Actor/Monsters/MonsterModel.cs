using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.World.Actor.Monsters
{
    public class MonsterModel
    {
        public MonsterModel(MonsterInGroupInformations message)
        {
            CreatureGenericId = message.creatureGenericId;
            Grade = message.grade;
            Look = message.look;
            
        }
        public MonsterModel(MonsterInGroupLightInformations message)
        {
            CreatureGenericId = message.creatureGenericId;
            Grade = message.grade;
            Look = null;

        }
        private int m_creatureGenericId;

        public virtual int CreatureGenericId
        {
            get
            {
                return m_creatureGenericId;
            }
            set
            {
                m_creatureGenericId = value;
            }
        }

        private sbyte m_grade;

        public virtual sbyte Grade
        {
            get
            {
                return m_grade;
            }
            set
            {
                m_grade = value;
            }
        }
        private EntityLook m_look;

        public virtual EntityLook Look
        {
            get
            {
                return m_look;
            }
            set
            {
                m_look = value;
            }
        }
    }
}
