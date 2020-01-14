using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeStartOkNpcShopMessage : NetworkMessage
{

	public const uint Id = 5761;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double NpcSellerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short TokenId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItemToSellInNpcShop[] ObjectsInfos { get; set; }

	public ExchangeStartOkNpcShopMessage() {}


	public ExchangeStartOkNpcShopMessage InitExchangeStartOkNpcShopMessage(double NpcSellerId, short TokenId, ObjectItemToSellInNpcShop[] ObjectsInfos)
	{
		this.NpcSellerId = NpcSellerId;
		this.TokenId = TokenId;
		this.ObjectsInfos = ObjectsInfos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.NpcSellerId);
		writer.WriteVarShort(this.TokenId);
		writer.WriteShort(this.ObjectsInfos.Length);
		foreach (ObjectItemToSellInNpcShop item in this.ObjectsInfos)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.NpcSellerId = reader.ReadDouble();
		this.TokenId = reader.ReadVarShort();
		int ObjectsInfosLen = reader.ReadShort();
		ObjectsInfos = new ObjectItemToSellInNpcShop[ObjectsInfosLen];
		for (int i = 0; i < ObjectsInfosLen; i++)
		{
			this.ObjectsInfos[i] = new ObjectItemToSellInNpcShop();
			this.ObjectsInfos[i].Deserialize(reader);
		}
	}
}
}
