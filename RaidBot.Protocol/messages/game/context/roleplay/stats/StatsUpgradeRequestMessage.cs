using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class StatsUpgradeRequestMessage : NetworkMessage
{

	public const uint Id = 5610;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool UseAdditionnal { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte StatId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short BoostPoint { get; set; }

	public StatsUpgradeRequestMessage() {}


	public StatsUpgradeRequestMessage InitStatsUpgradeRequestMessage(bool UseAdditionnal, byte StatId, short BoostPoint)
	{
		this.UseAdditionnal = UseAdditionnal;
		this.StatId = StatId;
		this.BoostPoint = BoostPoint;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.UseAdditionnal);
		writer.WriteByte(this.StatId);
		writer.WriteVarShort(this.BoostPoint);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.UseAdditionnal = reader.ReadBoolean();
		this.StatId = reader.ReadByte();
		this.BoostPoint = reader.ReadVarShort();
	}
}
}
