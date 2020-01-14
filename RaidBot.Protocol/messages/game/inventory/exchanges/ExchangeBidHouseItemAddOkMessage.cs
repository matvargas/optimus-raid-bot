using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeBidHouseItemAddOkMessage : NetworkMessage
{

	public const uint Id = 5945;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItemToSellInBid ItemInfo { get; set; }

	public ExchangeBidHouseItemAddOkMessage() {}


	public ExchangeBidHouseItemAddOkMessage InitExchangeBidHouseItemAddOkMessage(ObjectItemToSellInBid ItemInfo)
	{
		this.ItemInfo = ItemInfo;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.ItemInfo.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ItemInfo = new ObjectItemToSellInBid();
		this.ItemInfo.Deserialize(reader);
	}
}
}
