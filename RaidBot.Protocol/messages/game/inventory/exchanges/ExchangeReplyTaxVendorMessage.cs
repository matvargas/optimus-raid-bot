using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeReplyTaxVendorMessage : NetworkMessage
{

	public const uint Id = 5787;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long ObjectValue { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long TotalTaxValue { get; set; }

	public ExchangeReplyTaxVendorMessage() {}


	public ExchangeReplyTaxVendorMessage InitExchangeReplyTaxVendorMessage(long ObjectValue, long TotalTaxValue)
	{
		this.ObjectValue = ObjectValue;
		this.TotalTaxValue = TotalTaxValue;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.ObjectValue);
		writer.WriteVarLong(this.TotalTaxValue);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ObjectValue = reader.ReadVarLong();
		this.TotalTaxValue = reader.ReadVarLong();
	}
}
}
