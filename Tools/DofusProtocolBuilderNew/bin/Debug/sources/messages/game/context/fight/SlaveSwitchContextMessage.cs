using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class SlaveSwitchContextMessage : NetworkMessage
{

	public const uint Id = 6214;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MasterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double SlaveId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public SpellItem[] SlaveSpells { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterCharacteristicsInformations SlaveStats { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public Shortcut[] Shortcuts { get; set; }

	public SlaveSwitchContextMessage() {}


	public SlaveSwitchContextMessage InitSlaveSwitchContextMessage(double MasterId, double SlaveId, SpellItem[] SlaveSpells, CharacterCharacteristicsInformations SlaveStats, Shortcut[] Shortcuts)
	{
		this.MasterId = MasterId;
		this.SlaveId = SlaveId;
		this.SlaveSpells = SlaveSpells;
		this.SlaveStats = SlaveStats;
		this.Shortcuts = Shortcuts;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.MasterId);
		writer.WriteDouble(this.SlaveId);
		writer.WriteShort(this.SlaveSpells.Length);
		foreach (SpellItem item in this.SlaveSpells)
		{
			item.Serialize(writer);
		}
		this.SlaveStats.Serialize(writer);
		writer.WriteShort(this.Shortcuts.Length);
		foreach (Shortcut item in this.Shortcuts)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MasterId = reader.ReadDouble();
		this.SlaveId = reader.ReadDouble();
		int SlaveSpellsLen = reader.ReadShort();
		SlaveSpells = new SpellItem[SlaveSpellsLen];
		for (int i = 0; i < SlaveSpellsLen; i++)
		{
			this.SlaveSpells[i] = new SpellItem();
			this.SlaveSpells[i].Deserialize(reader);
		}
		this.SlaveStats = new CharacterCharacteristicsInformations();
		this.SlaveStats.Deserialize(reader);
		int ShortcutsLen = reader.ReadShort();
		Shortcuts = new Shortcut[ShortcutsLen];
		for (int i = 0; i < ShortcutsLen; i++)
		{
			this.Shortcuts[i] = ProtocolTypeManager.GetInstance<Shortcut>(reader.ReadShort());
			this.Shortcuts[i].Deserialize(reader);
		}
	}
}
}
