using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class FriendAddedMessage : NetworkMessage
{

	public const uint Id = 5599;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FriendInformations FriendAdded { get; set; }

	public FriendAddedMessage() {}


	public FriendAddedMessage InitFriendAddedMessage(FriendInformations FriendAdded)
	{
		this.FriendAdded = FriendAdded;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
writer.WriteShort(FriendAdded.MessageId);
		FriendAdded.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
this.FriendAdded = ProtocolTypeManager.GetInstance<FriendInformations>(reader.ReadShort());
		this.FriendAdded.Deserialize(reader);
	}
}
}
