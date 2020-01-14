using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class NpcDialogReplyMessage : NetworkMessage
{

	public const uint Id = 5616;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ReplyId { get; set; }

	public NpcDialogReplyMessage() {}


	public NpcDialogReplyMessage InitNpcDialogReplyMessage(int ReplyId)
	{
		this.ReplyId = ReplyId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.ReplyId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ReplyId = reader.ReadVarInt();
	}
}
}
