using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeOfflineSoldItemsMessage : NetworkMessage
{

	public const uint Id = 6613;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItemGenericQuantityPrice[] BidHouseItems { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItemGenericQuantityPrice[] MerchantItems { get; set; }

	public ExchangeOfflineSoldItemsMessage() {}


	public ExchangeOfflineSoldItemsMessage InitExchangeOfflineSoldItemsMessage(ObjectItemGenericQuantityPrice[] BidHouseItems, ObjectItemGenericQuantityPrice[] MerchantItems)
	{
		this.BidHouseItems = BidHouseItems;
		this.MerchantItems = MerchantItems;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.BidHouseItems.Length);
		foreach (ObjectItemGenericQuantityPrice item in this.BidHouseItems)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.MerchantItems.Length);
		foreach (ObjectItemGenericQuantityPrice item in this.MerchantItems)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int BidHouseItemsLen = reader.ReadShort();
		BidHouseItems = new ObjectItemGenericQuantityPrice[BidHouseItemsLen];
		for (int i = 0; i < BidHouseItemsLen; i++)
		{
			this.BidHouseItems[i] = new ObjectItemGenericQuantityPrice();
			this.BidHouseItems[i].Deserialize(reader);
		}
		int MerchantItemsLen = reader.ReadShort();
		MerchantItems = new ObjectItemGenericQuantityPrice[MerchantItemsLen];
		for (int i = 0; i < MerchantItemsLen; i++)
		{
			this.MerchantItems[i] = new ObjectItemGenericQuantityPrice();
			this.MerchantItems[i].Deserialize(reader);
		}
	}
}
}
