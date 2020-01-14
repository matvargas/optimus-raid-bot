using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ChatServerWithObjectMessage : ChatServerMessage
{

	public const uint Id = 883;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItem[] Objects { get; set; }

	public ChatServerWithObjectMessage() {}


	public ChatServerWithObjectMessage InitChatServerWithObjectMessage(ObjectItem[] Objects)
	{
		this.Objects = Objects;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Objects.Length);
		foreach (ObjectItem item in this.Objects)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int ObjectsLen = reader.ReadShort();
		Objects = new ObjectItem[ObjectsLen];
		for (int i = 0; i < ObjectsLen; i++)
		{
			this.Objects[i] = new ObjectItem();
			this.Objects[i].Deserialize(reader);
		}
	}
}
}
