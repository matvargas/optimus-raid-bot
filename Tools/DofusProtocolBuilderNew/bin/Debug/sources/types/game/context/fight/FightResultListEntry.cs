using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightResultListEntry : NetworkType
{

	public const uint Id = 16;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Outcome { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Wave { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightLoot Rewards { get; set; }

	public FightResultListEntry() {}


	public FightResultListEntry InitFightResultListEntry(short Outcome, byte Wave, FightLoot Rewards)
	{
		this.Outcome = Outcome;
		this.Wave = Wave;
		this.Rewards = Rewards;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.Outcome);
		writer.WriteByte(this.Wave);
		this.Rewards.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Outcome = reader.ReadVarShort();
		this.Wave = reader.ReadByte();
		this.Rewards = new FightLoot();
		this.Rewards.Deserialize(reader);
	}
}
}
