using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeBidPriceForSellerMessage : ExchangeBidPriceMessage
{

	public const uint Id = 6464;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool AllIdentical { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long[] MinimalPrices { get; set; }

	public ExchangeBidPriceForSellerMessage() {}


	public ExchangeBidPriceForSellerMessage InitExchangeBidPriceForSellerMessage(bool AllIdentical, long[] MinimalPrices)
	{
		this.AllIdentical = AllIdentical;
		this.MinimalPrices = MinimalPrices;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(this.AllIdentical);
		writer.WriteShort(this.MinimalPrices.Length);
		foreach (long item in this.MinimalPrices)
		{
			writer.WriteVarLong(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.AllIdentical = reader.ReadBoolean();
		int MinimalPricesLen = reader.ReadShort();
		MinimalPrices = new long[MinimalPricesLen];
		for (int i = 0; i < MinimalPricesLen; i++)
		{
			this.MinimalPrices[i] = reader.ReadVarLong();
		}
	}
}
}
