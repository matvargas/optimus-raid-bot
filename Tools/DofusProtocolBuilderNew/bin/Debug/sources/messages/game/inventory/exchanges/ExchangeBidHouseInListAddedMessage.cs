using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeBidHouseInListAddedMessage : NetworkMessage
{

	public const uint Id = 5949;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ItemUID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ObjGenericId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectEffect[] Effects { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long[] Prices { get; set; }

	public ExchangeBidHouseInListAddedMessage() {}


	public ExchangeBidHouseInListAddedMessage InitExchangeBidHouseInListAddedMessage(int ItemUID, int ObjGenericId, ObjectEffect[] Effects, long[] Prices)
	{
		this.ItemUID = ItemUID;
		this.ObjGenericId = ObjGenericId;
		this.Effects = Effects;
		this.Prices = Prices;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.ItemUID);
		writer.WriteInt(this.ObjGenericId);
		writer.WriteShort(this.Effects.Length);
		foreach (ObjectEffect item in this.Effects)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteShort(this.Prices.Length);
		foreach (long item in this.Prices)
		{
			writer.WriteVarLong(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ItemUID = reader.ReadInt();
		this.ObjGenericId = reader.ReadInt();
		int EffectsLen = reader.ReadShort();
		Effects = new ObjectEffect[EffectsLen];
		for (int i = 0; i < EffectsLen; i++)
		{
			this.Effects[i] = ProtocolTypeManager.GetInstance<ObjectEffect>(reader.ReadShort());
			this.Effects[i].Deserialize(reader);
		}
		int PricesLen = reader.ReadShort();
		Prices = new long[PricesLen];
		for (int i = 0; i < PricesLen; i++)
		{
			this.Prices[i] = reader.ReadVarLong();
		}
	}
}
}
