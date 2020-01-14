using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ChangeHavenBagRoomRequestMessage : NetworkMessage
{

	public const uint Id = 6638;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte RoomId { get; set; }

	public ChangeHavenBagRoomRequestMessage() {}


	public ChangeHavenBagRoomRequestMessage InitChangeHavenBagRoomRequestMessage(byte RoomId)
	{
		this.RoomId = RoomId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.RoomId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.RoomId = reader.ReadByte();
	}
}
}
