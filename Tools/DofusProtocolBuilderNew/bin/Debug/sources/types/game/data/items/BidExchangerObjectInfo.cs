using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class BidExchangerObjectInfo : NetworkType
{

	public const uint Id = 122;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ObjectUID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectEffect[] Effects { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long[] Prices { get; set; }

	public BidExchangerObjectInfo() {}


	public BidExchangerObjectInfo InitBidExchangerObjectInfo(int ObjectUID, ObjectEffect[] Effects, long[] Prices)
	{
		this.ObjectUID = ObjectUID;
		this.Effects = Effects;
		this.Prices = Prices;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.ObjectUID);
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
		this.ObjectUID = reader.ReadVarInt();
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
