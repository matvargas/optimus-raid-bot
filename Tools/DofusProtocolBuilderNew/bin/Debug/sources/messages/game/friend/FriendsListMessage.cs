using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class FriendsListMessage : NetworkMessage
{

	public const uint Id = 4002;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FriendInformations[] FriendsList { get; set; }

	public FriendsListMessage() {}


	public FriendsListMessage InitFriendsListMessage(FriendInformations[] FriendsList)
	{
		this.FriendsList = FriendsList;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.FriendsList.Length);
		foreach (FriendInformations item in this.FriendsList)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int FriendsListLen = reader.ReadShort();
		FriendsList = new FriendInformations[FriendsListLen];
		for (int i = 0; i < FriendsListLen; i++)
		{
			this.FriendsList[i] = ProtocolTypeManager.GetInstance<FriendInformations>(reader.ReadShort());
			this.FriendsList[i].Deserialize(reader);
		}
	}
}
}
