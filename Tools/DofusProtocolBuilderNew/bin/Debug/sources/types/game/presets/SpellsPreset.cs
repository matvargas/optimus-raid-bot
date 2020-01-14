using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class SpellsPreset : Preset
{

	public const uint Id = 519;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public SpellForPreset[] Spells { get; set; }

	public SpellsPreset() {}


	public SpellsPreset InitSpellsPreset(SpellForPreset[] Spells)
	{
		this.Spells = Spells;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Spells.Length);
		foreach (SpellForPreset item in this.Spells)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int SpellsLen = reader.ReadShort();
		Spells = new SpellForPreset[SpellsLen];
		for (int i = 0; i < SpellsLen; i++)
		{
			this.Spells[i] = new SpellForPreset();
			this.Spells[i].Deserialize(reader);
		}
	}
}
}
