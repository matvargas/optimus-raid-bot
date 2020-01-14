using RaidBot.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Model.Game.World.Actor
{
    public class ActorModel:ModelBase
    {
        private int m_contextualId;

        public virtual int ContextualId
        {
            get
            {
                return m_contextualId;
            }
            set
            {
                m_contextualId = value;
            }
        }
        private short mCellId;
        public short CellId
        {
            get { return mCellId; }
            set
            {
                mCellId = value;
                Notify();
            }
        }

        public ActorModel(short cellId)
        {

            CellId = cellId;
        }
        
        public ActorModel(GameContextActorInformations informations)
        {
            ContextualId = informations.contextualId;
            CellId = informations.disposition.cellId;
            
        }

        public static implicit operator ActorModel(GameContextActorInformations message)
        {
            return new ActorModel(message);
        }
       
    }
}
