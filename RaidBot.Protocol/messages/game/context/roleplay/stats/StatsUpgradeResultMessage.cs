using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class StatsUpgradeResultMessage : NetworkMessage
{

	public const uint Id = 5609;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Result { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NbCharacBoost { get; set; }

	public StatsUpgradeResultMessage() {}


	public StatsUpgradeResultMessage InitStatsUpgradeResultMessage(byte Result, short NbCharacBoost)
	{
		this.Result = Result;
		this.NbCharacBoost = NbCharacBoost;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Result);
		writer.WriteVarShort(this.NbCharacBoost);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Result = reader.ReadByte();
		this.NbCharacBoost = reader.ReadVarShort();
	}
}
}
