using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ChatCommunityChannelCommunityMessage : NetworkMessage
{

	public const uint Id = 6730;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CommunityId { get; set; }

	public ChatCommunityChannelCommunityMessage() {}


	public ChatCommunityChannelCommunityMessage InitChatCommunityChannelCommunityMessage(short CommunityId)
	{
		this.CommunityId = CommunityId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.CommunityId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CommunityId = reader.ReadShort();
	}
}
}
