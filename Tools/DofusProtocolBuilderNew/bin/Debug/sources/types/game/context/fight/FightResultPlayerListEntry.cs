using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightResultPlayerListEntry : FightResultFighterListEntry
{

	public const uint Id = 24;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Level { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightResultAdditionalData[] Additional { get; set; }

	public FightResultPlayerListEntry() {}


	public FightResultPlayerListEntry InitFightResultPlayerListEntry(short Level, FightResultAdditionalData[] Additional)
	{
		this.Level = Level;
		this.Additional = Additional;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.Level);
		writer.WriteShort(this.Additional.Length);
		foreach (FightResultAdditionalData item in this.Additional)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Level = reader.ReadVarShort();
		int AdditionalLen = reader.ReadShort();
		Additional = new FightResultAdditionalData[AdditionalLen];
		for (int i = 0; i < AdditionalLen; i++)
		{
			this.Additional[i] = ProtocolTypeManager.GetInstance<FightResultAdditionalData>(reader.ReadShort());
			this.Additional[i].Deserialize(reader);
		}
	}
}
}
