using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Protocol.DataCenter;
using RaidBot.Data.GameData;
using RaidBot.Data.IO.D2I;
using RaidBot.Protocol.Types;
using RaidBot.Engine.Model.Game.World.Fight.Shapes;

namespace RaidBot.Engine.Model.Game.Player.Spells
{
    public class SpellData
    {
        private SpellLevel miniSpell;
        public SpellData(SpellItem item)
        {
            Position = item.position;
            SpellId = item.spellId;
            SpellLevelId = item.spellLevel;
            miniSpell = GameDataAdapter.GetClass<SpellLevel>((int)GameDataAdapter.GetClass<Spell>((int)this.SpellId).SpellLevels[(int)this.SpellLevelId - 1]);
        }
        private byte m_position;

        public virtual byte Position
        {
            get
            {
                return m_position;
            }
            set
            {
                m_position = value;
            }
        }

        private int m_spellId;

        public virtual int SpellId
        {
            get
            {
                return m_spellId;
            }
            set
            {
                m_spellId = value;
            }
        }

       

        public virtual SpellLevel SpellLevel
        {
            get
            {
				return GameDataAdapter.GetClass<SpellLevel> ((int)GameDataAdapter.GetClass<Spell> ((int)this.SpellId).SpellLevels [(int)this.SpellLevelId - 1]);
            }
           
        }
		private sbyte m_spellLevelId;

		public virtual sbyte SpellLevelId
		{
			get
			{
				return m_spellLevelId;
			}
			set
			{
				m_spellLevelId = value;
			}
		}


        public string Name
        {
            get
            {
                return Data.IO.D2I.TextDataAdapter.GetText((int)GameDataAdapter.GetClass<Spell>(this.SpellId).NameId);
            }

        }
        public string Description
        {
            get
            {
                return Data.IO.D2I.TextDataAdapter.GetText((int)GameDataAdapter.GetClass<Spell>(this.SpellId).DescriptionId);
            }

        }
		public IShape SpellZone
		{
			get
			{
				if (SpellLevel.CastInLine && !SpellLevel.CastInDiagonal) {
					return new Cross ((byte)SpellLevel.MinRange,(byte) SpellLevel.Range);
				}
				else if (!SpellLevel.CastInLine && SpellLevel.CastInDiagonal) {
					return new DiagonalCross ((byte)SpellLevel.MinRange, (byte)SpellLevel.Range);
				}
				else  {
					return new Lozenge ((byte)SpellLevel.MinRange,(byte) SpellLevel.Range);
				}

			}
		}


       

    }
}
