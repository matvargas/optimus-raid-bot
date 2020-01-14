using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeBidPriceMessage : NetworkMessage
{

	public const uint Id = 5755;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short GenericId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long AveragePrice { get; set; }

	public ExchangeBidPriceMessage() {}


	public ExchangeBidPriceMessage InitExchangeBidPriceMessage(short GenericId, long AveragePrice)
	{
		this.GenericId = GenericId;
		this.AveragePrice = AveragePrice;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.GenericId);
		writer.WriteVarLong(this.AveragePrice);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.GenericId = reader.ReadVarShort();
		this.AveragePrice = reader.ReadVarLong();
	}
}
}
