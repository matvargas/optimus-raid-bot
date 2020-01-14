using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeCraftPaymentModificationRequestMessage : NetworkMessage
{

	public const uint Id = 6579;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Quantity { get; set; }

	public ExchangeCraftPaymentModificationRequestMessage() {}


	public ExchangeCraftPaymentModificationRequestMessage InitExchangeCraftPaymentModificationRequestMessage(long Quantity)
	{
		this.Quantity = Quantity;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.Quantity);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Quantity = reader.ReadVarLong();
	}
}
}
