using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeCraftPaymentModifiedMessage : NetworkMessage
{

	public const uint Id = 6578;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long GoldSum { get; set; }

	public ExchangeCraftPaymentModifiedMessage() {}


	public ExchangeCraftPaymentModifiedMessage InitExchangeCraftPaymentModifiedMessage(long GoldSum)
	{
		this.GoldSum = GoldSum;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.GoldSum);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.GoldSum = reader.ReadVarLong();
	}
}
}
