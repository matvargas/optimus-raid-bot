using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class SpellForPreset : NetworkType
{

	public const uint Id = 557;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SpellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Shortcuts { get; set; }

	public SpellForPreset() {}


	public SpellForPreset InitSpellForPreset(short SpellId, short[] Shortcuts)
	{
		this.SpellId = SpellId;
		this.Shortcuts = Shortcuts;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SpellId);
		writer.WriteShort(this.Shortcuts.Length);
		foreach (short item in this.Shortcuts)
		{
			writer.WriteShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SpellId = reader.ReadVarShort();
		int ShortcutsLen = reader.ReadShort();
		Shortcuts = new short[ShortcutsLen];
		for (int i = 0; i < ShortcutsLen; i++)
		{
			this.Shortcuts[i] = reader.ReadShort();
		}
	}
}
}
