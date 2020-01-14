using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Protocol.DataCenter;
using RaidBot.Data.GameData;
using RaidBot.Data.IO.D2I;
using RaidBot.Protocol.Types;

namespace RaidBot.Engine.Model.Game.World.Actor.Npcs
{
    public class NpcModel:ActorModel
    {
        public NpcModel(GameRolePlayNpcInformations message)
            :base(message)
        {
            NpcId = message.npcId;
            Sex = message.sex;
            SpecialArtworkId = message.specialArtworkId;
            NpcData = GameDataAdapter.GetClass<Npc>(NpcId);
            if (NpcData == null)
                return;
            NpcName = TextDataAdapter.GetText((int)NpcData.NameId); 
            NpcReplies = new List<Replie>();
            foreach (List<int> list in this.NpcData.DialogReplies)
            this.NpcReplies.Add(new Replie(list[0], TextDataAdapter.GetText(list[1])));
        }
        public Replie GetReply(int id)
        {
            return Enumerable.FirstOrDefault<Replie>((IEnumerable<Replie>)this.NpcReplies, (Func<Replie, bool>)(f => f.ReplieId == id));
        }
        private Npc m_npcData;
        public virtual Npc NpcData
        {
            get
            {
                return m_npcData;
            }
            set
            {
                m_npcData = value;
               
            }
        }
        private string m_npcName;
        public string  NpcName
        {
            get
            {
                if (this.NpcData == null)
                    return null;
                return m_npcName;
            }
            set
            {
                m_npcName = value;
              
            }
        }
        private List<Replie> m_npcReplies;
        public virtual List<Replie> NpcReplies
        {
            get
            {
                return m_npcReplies;
            }
            set
            {
                m_npcReplies = value;
               
            }
        }
        private ushort m_npcId;
        public virtual ushort NpcId
        {
            get
            {
                return m_npcId;
            }
            set
            {
                m_npcId = value;
            }
        }

        private bool m_sex;
        public virtual bool Sex
        {
            get
            {
                return m_sex;
            }
            set
            {
                m_sex = value;
            }
        }

        private ushort m_specialArtworkId;
        public virtual ushort SpecialArtworkId
        {
            get
            {
                return m_specialArtworkId;
            }
            set
            {
                m_specialArtworkId = value;
            }
        }
    }
}
