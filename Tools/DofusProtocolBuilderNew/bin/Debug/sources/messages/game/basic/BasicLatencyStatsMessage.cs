using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class BasicLatencyStatsMessage : NetworkMessage
{

	public const uint Id = 5663;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Latency { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SampleCount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Max { get; set; }

	public BasicLatencyStatsMessage() {}


	public BasicLatencyStatsMessage InitBasicLatencyStatsMessage(short Latency, short SampleCount, short Max)
	{
		this.Latency = Latency;
		this.SampleCount = SampleCount;
		this.Max = Max;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Latency);
		writer.WriteVarShort(this.SampleCount);
		writer.WriteVarShort(this.Max);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Latency = reader.ReadShort();
		this.SampleCount = reader.ReadVarShort();
		this.Max = reader.ReadVarShort();
	}
}
}
