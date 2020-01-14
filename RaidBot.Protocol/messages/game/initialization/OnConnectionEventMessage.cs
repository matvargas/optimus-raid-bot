using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class OnConnectionEventMessage : NetworkMessage
{

	public const uint Id = 5726;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte EventType { get; set; }

	public OnConnectionEventMessage() {}


	public OnConnectionEventMessage InitOnConnectionEventMessage(byte EventType)
	{
		this.EventType = EventType;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.EventType);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.EventType = reader.ReadByte();
	}
}
}
