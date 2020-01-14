using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeTypesItemsExchangerDescriptionForUserMessage : NetworkMessage
{

	public const uint Id = 5752;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public BidExchangerObjectInfo[] ItemTypeDescriptions { get; set; }

	public ExchangeTypesItemsExchangerDescriptionForUserMessage() {}


	public ExchangeTypesItemsExchangerDescriptionForUserMessage InitExchangeTypesItemsExchangerDescriptionForUserMessage(BidExchangerObjectInfo[] ItemTypeDescriptions)
	{
		this.ItemTypeDescriptions = ItemTypeDescriptions;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.ItemTypeDescriptions.Length);
		foreach (BidExchangerObjectInfo item in this.ItemTypeDescriptions)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ItemTypeDescriptionsLen = reader.ReadShort();
		ItemTypeDescriptions = new BidExchangerObjectInfo[ItemTypeDescriptionsLen];
		for (int i = 0; i < ItemTypeDescriptionsLen; i++)
		{
			this.ItemTypeDescriptions[i] = new BidExchangerObjectInfo();
			this.ItemTypeDescriptions[i].Deserialize(reader);
		}
	}
}
}
