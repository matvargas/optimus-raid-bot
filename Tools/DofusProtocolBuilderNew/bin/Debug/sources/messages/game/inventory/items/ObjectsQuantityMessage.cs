using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ObjectsQuantityMessage : NetworkMessage
{

	public const uint Id = 6206;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItemQuantity[] ObjectsUIDAndQty { get; set; }

	public ObjectsQuantityMessage() {}


	public ObjectsQuantityMessage InitObjectsQuantityMessage(ObjectItemQuantity[] ObjectsUIDAndQty)
	{
		this.ObjectsUIDAndQty = ObjectsUIDAndQty;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.ObjectsUIDAndQty.Length);
		foreach (ObjectItemQuantity item in this.ObjectsUIDAndQty)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ObjectsUIDAndQtyLen = reader.ReadShort();
		ObjectsUIDAndQty = new ObjectItemQuantity[ObjectsUIDAndQtyLen];
		for (int i = 0; i < ObjectsUIDAndQtyLen; i++)
		{
			this.ObjectsUIDAndQty[i] = new ObjectItemQuantity();
			this.ObjectsUIDAndQty[i].Deserialize(reader);
		}
	}
}
}
