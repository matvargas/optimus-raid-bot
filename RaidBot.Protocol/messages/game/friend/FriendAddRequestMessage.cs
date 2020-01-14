using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class FriendAddRequestMessage : NetworkMessage
{

	public const uint Id = 4004;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Name { get; set; }

	public FriendAddRequestMessage() {}


	public FriendAddRequestMessage InitFriendAddRequestMessage(String Name)
	{
		this.Name = Name;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.Name);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Name = reader.ReadUTF();
	}
}
}
