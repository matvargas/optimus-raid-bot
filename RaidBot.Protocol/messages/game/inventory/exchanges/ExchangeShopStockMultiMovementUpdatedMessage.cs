using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeShopStockMultiMovementUpdatedMessage : NetworkMessage
{

	public const uint Id = 6038;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItemToSell[] ObjectInfoList { get; set; }

	public ExchangeShopStockMultiMovementUpdatedMessage() {}


	public ExchangeShopStockMultiMovementUpdatedMessage InitExchangeShopStockMultiMovementUpdatedMessage(ObjectItemToSell[] ObjectInfoList)
	{
		this.ObjectInfoList = ObjectInfoList;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.ObjectInfoList.Length);
		foreach (ObjectItemToSell item in this.ObjectInfoList)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ObjectInfoListLen = reader.ReadShort();
		ObjectInfoList = new ObjectItemToSell[ObjectInfoListLen];
		for (int i = 0; i < ObjectInfoListLen; i++)
		{
			this.ObjectInfoList[i] = new ObjectItemToSell();
			this.ObjectInfoList[i].Deserialize(reader);
		}
	}
}
}
