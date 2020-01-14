using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeShopStockStartedMessage : NetworkMessage
{

	public const uint Id = 5910;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItemToSell[] ObjectsInfos { get; set; }

	public ExchangeShopStockStartedMessage() {}


	public ExchangeShopStockStartedMessage InitExchangeShopStockStartedMessage(ObjectItemToSell[] ObjectsInfos)
	{
		this.ObjectsInfos = ObjectsInfos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.ObjectsInfos.Length);
		foreach (ObjectItemToSell item in this.ObjectsInfos)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ObjectsInfosLen = reader.ReadShort();
		ObjectsInfos = new ObjectItemToSell[ObjectsInfosLen];
		for (int i = 0; i < ObjectsInfosLen; i++)
		{
			this.ObjectsInfos[i] = new ObjectItemToSell();
			this.ObjectsInfos[i].Deserialize(reader);
		}
	}
}
}
