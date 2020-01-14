using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TaxCollectorStateUpdateMessage : NetworkMessage
{

	public const uint Id = 6455;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double UniqueId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte State { get; set; }

	public TaxCollectorStateUpdateMessage() {}


	public TaxCollectorStateUpdateMessage InitTaxCollectorStateUpdateMessage(double UniqueId, byte State)
	{
		this.UniqueId = UniqueId;
		this.State = State;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.UniqueId);
		writer.WriteByte(this.State);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.UniqueId = reader.ReadDouble();
		this.State = reader.ReadByte();
	}
}
}
