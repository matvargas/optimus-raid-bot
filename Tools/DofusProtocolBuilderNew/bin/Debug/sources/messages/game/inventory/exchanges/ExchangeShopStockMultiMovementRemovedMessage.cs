using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeShopStockMultiMovementRemovedMessage : NetworkMessage
{

	public const uint Id = 6037;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] ObjectIdList { get; set; }

	public ExchangeShopStockMultiMovementRemovedMessage() {}


	public ExchangeShopStockMultiMovementRemovedMessage InitExchangeShopStockMultiMovementRemovedMessage(int[] ObjectIdList)
	{
		this.ObjectIdList = ObjectIdList;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.ObjectIdList.Length);
		foreach (int item in this.ObjectIdList)
		{
			writer.WriteVarInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ObjectIdListLen = reader.ReadShort();
		ObjectIdList = new int[ObjectIdListLen];
		for (int i = 0; i < ObjectIdListLen; i++)
		{
			this.ObjectIdList[i] = reader.ReadVarInt();
		}
	}
}
}
