using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CurrentServerStatusUpdateMessage : NetworkMessage
{

	public const uint Id = 6525;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Status { get; set; }

	public CurrentServerStatusUpdateMessage() {}


	public CurrentServerStatusUpdateMessage InitCurrentServerStatusUpdateMessage(byte Status)
	{
		this.Status = Status;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Status);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Status = reader.ReadByte();
	}
}
}
