using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class FriendUpdateMessage : NetworkMessage
{

	public const uint Id = 5924;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FriendInformations FriendUpdated { get; set; }

	public FriendUpdateMessage() {}


	public FriendUpdateMessage InitFriendUpdateMessage(FriendInformations FriendUpdated)
	{
		this.FriendUpdated = FriendUpdated;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
writer.WriteShort(FriendUpdated.MessageId);
		FriendUpdated.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
this.FriendUpdated = ProtocolTypeManager.GetInstance<FriendInformations>(reader.ReadShort());
		this.FriendUpdated.Deserialize(reader);
	}
}
}
