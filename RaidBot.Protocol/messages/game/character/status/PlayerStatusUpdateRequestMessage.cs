using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PlayerStatusUpdateRequestMessage : NetworkMessage
{

	public const uint Id = 6387;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PlayerStatus Status { get; set; }

	public PlayerStatusUpdateRequestMessage() {}


	public PlayerStatusUpdateRequestMessage InitPlayerStatusUpdateRequestMessage(PlayerStatus Status)
	{
		this.Status = Status;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
writer.WriteShort(Status.MessageId);
		Status.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
this.Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
		this.Status.Deserialize(reader);
	}
}
}
