using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class SpellListMessage : NetworkMessage
{

	public const uint Id = 1200;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool SpellPrevisualization { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public SpellItem[] Spells { get; set; }

	public SpellListMessage() {}


	public SpellListMessage InitSpellListMessage(bool SpellPrevisualization, SpellItem[] Spells)
	{
		this.SpellPrevisualization = SpellPrevisualization;
		this.Spells = Spells;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.SpellPrevisualization);
		writer.WriteShort(this.Spells.Length);
		foreach (SpellItem item in this.Spells)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SpellPrevisualization = reader.ReadBoolean();
		int SpellsLen = reader.ReadShort();
		Spells = new SpellItem[SpellsLen];
		for (int i = 0; i < SpellsLen; i++)
		{
			this.Spells[i] = new SpellItem();
			this.Spells[i].Deserialize(reader);
		}
	}
}
}
