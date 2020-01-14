using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightStartingPositions : NetworkType
{

	public const uint Id = 513;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] PositionsForChallengers { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] PositionsForDefenders { get; set; }

	public FightStartingPositions() {}


	public FightStartingPositions InitFightStartingPositions(short[] PositionsForChallengers, short[] PositionsForDefenders)
	{
		this.PositionsForChallengers = PositionsForChallengers;
		this.PositionsForDefenders = PositionsForDefenders;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.PositionsForChallengers.Length);
		foreach (short item in this.PositionsForChallengers)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.PositionsForDefenders.Length);
		foreach (short item in this.PositionsForDefenders)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int PositionsForChallengersLen = reader.ReadShort();
		PositionsForChallengers = new short[PositionsForChallengersLen];
		for (int i = 0; i < PositionsForChallengersLen; i++)
		{
			this.PositionsForChallengers[i] = reader.ReadVarShort();
		}
		int PositionsForDefendersLen = reader.ReadShort();
		PositionsForDefenders = new short[PositionsForDefendersLen];
		for (int i = 0; i < PositionsForDefendersLen; i++)
		{
			this.PositionsForDefenders[i] = reader.ReadVarShort();
		}
	}
}
}
