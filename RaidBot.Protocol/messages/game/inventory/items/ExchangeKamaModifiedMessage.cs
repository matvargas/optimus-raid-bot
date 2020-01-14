using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeKamaModifiedMessage : ExchangeObjectMessage
{

	public const uint Id = 5521;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Quantity { get; set; }

	public ExchangeKamaModifiedMessage() {}


	public ExchangeKamaModifiedMessage InitExchangeKamaModifiedMessage(long Quantity)
	{
		this.Quantity = Quantity;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.Quantity);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Quantity = reader.ReadVarLong();
	}
}
}