using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeObjectsModifiedMessage : ExchangeObjectMessage
{

	public const uint Id = 6533;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItem[] Object { get; set; }

	public ExchangeObjectsModifiedMessage() {}


	public ExchangeObjectsModifiedMessage InitExchangeObjectsModifiedMessage(ObjectItem[] Object)
	{
		this.Object = Object;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Object.Length);
		foreach (ObjectItem item in this.Object)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int ObjectLen = reader.ReadShort();
		Object = new ObjectItem[ObjectLen];
		for (int i = 0; i < ObjectLen; i++)
		{
			this.Object[i] = new ObjectItem();
			this.Object[i].Deserialize(reader);
		}
	}
}
}
