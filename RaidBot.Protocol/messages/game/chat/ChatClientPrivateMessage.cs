using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ChatClientPrivateMessage : ChatAbstractClientMessage
{

	public const uint Id = 851;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Receiver { get; set; }

	public ChatClientPrivateMessage() {}


	public ChatClientPrivateMessage InitChatClientPrivateMessage(String Receiver)
	{
		this.Receiver = Receiver;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUTF(this.Receiver);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Receiver = reader.ReadUTF();
	}
}
}
