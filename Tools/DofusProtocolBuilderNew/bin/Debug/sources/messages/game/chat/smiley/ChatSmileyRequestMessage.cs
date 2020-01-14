using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ChatSmileyRequestMessage : NetworkMessage
{

	public const uint Id = 800;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SmileyId { get; set; }

	public ChatSmileyRequestMessage() {}


	public ChatSmileyRequestMessage InitChatSmileyRequestMessage(short SmileyId)
	{
		this.SmileyId = SmileyId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SmileyId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SmileyId = reader.ReadVarShort();
	}
}
}
