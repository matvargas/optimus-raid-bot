using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ChallengeTargetsListMessage : NetworkMessage
{

	public const uint Id = 5613;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double[] TargetIds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] TargetCells { get; set; }

	public ChallengeTargetsListMessage() {}


	public ChallengeTargetsListMessage InitChallengeTargetsListMessage(double[] TargetIds, short[] TargetCells)
	{
		this.TargetIds = TargetIds;
		this.TargetCells = TargetCells;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.TargetIds.Length);
		foreach (double item in this.TargetIds)
		{
			writer.WriteDouble(item);
		}
		writer.WriteShort(this.TargetCells.Length);
		foreach (short item in this.TargetCells)
		{
			writer.WriteShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int TargetIdsLen = reader.ReadShort();
		TargetIds = new double[TargetIdsLen];
		for (int i = 0; i < TargetIdsLen; i++)
		{
			this.TargetIds[i] = reader.ReadDouble();
		}
		int TargetCellsLen = reader.ReadShort();
		TargetCells = new short[TargetCellsLen];
		for (int i = 0; i < TargetCellsLen; i++)
		{
			this.TargetCells[i] = reader.ReadShort();
		}
	}
}
}
