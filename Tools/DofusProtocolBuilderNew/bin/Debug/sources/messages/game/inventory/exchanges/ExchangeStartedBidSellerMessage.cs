using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeStartedBidSellerMessage : NetworkMessage
{

	public const uint Id = 5905;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public SellerBuyerDescriptor SellerDescriptor { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItemToSellInBid[] ObjectsInfos { get; set; }

	public ExchangeStartedBidSellerMessage() {}


	public ExchangeStartedBidSellerMessage InitExchangeStartedBidSellerMessage(SellerBuyerDescriptor SellerDescriptor, ObjectItemToSellInBid[] ObjectsInfos)
	{
		this.SellerDescriptor = SellerDescriptor;
		this.ObjectsInfos = ObjectsInfos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.SellerDescriptor.Serialize(writer);
		writer.WriteShort(this.ObjectsInfos.Length);
		foreach (ObjectItemToSellInBid item in this.ObjectsInfos)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SellerDescriptor = new SellerBuyerDescriptor();
		this.SellerDescriptor.Deserialize(reader);
		int ObjectsInfosLen = reader.ReadShort();
		ObjectsInfos = new ObjectItemToSellInBid[ObjectsInfosLen];
		for (int i = 0; i < ObjectsInfosLen; i++)
		{
			this.ObjectsInfos[i] = new ObjectItemToSellInBid();
			this.ObjectsInfos[i].Deserialize(reader);
		}
	}
}
}
