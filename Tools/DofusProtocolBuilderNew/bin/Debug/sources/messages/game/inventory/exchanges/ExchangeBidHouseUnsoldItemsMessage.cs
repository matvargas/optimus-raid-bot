using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeBidHouseUnsoldItemsMessage : NetworkMessage
{

	public const uint Id = 6612;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItemGenericQuantity[] Items { get; set; }

	public ExchangeBidHouseUnsoldItemsMessage() {}


	public ExchangeBidHouseUnsoldItemsMessage InitExchangeBidHouseUnsoldItemsMessage(ObjectItemGenericQuantity[] Items)
	{
		this.Items = Items;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Items.Length);
		foreach (ObjectItemGenericQuantity item in this.Items)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ItemsLen = reader.ReadShort();
		Items = new ObjectItemGenericQuantity[ItemsLen];
		for (int i = 0; i < ItemsLen; i++)
		{
			this.Items[i] = new ObjectItemGenericQuantity();
			this.Items[i].Deserialize(reader);
		}
	}
}
}
