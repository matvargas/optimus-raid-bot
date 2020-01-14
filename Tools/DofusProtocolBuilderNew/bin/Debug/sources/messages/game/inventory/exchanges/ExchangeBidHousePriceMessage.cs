using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeBidHousePriceMessage : NetworkMessage
{

	public const uint Id = 5805;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short GenId { get; set; }

	public ExchangeBidHousePriceMessage() {}


	public ExchangeBidHousePriceMessage InitExchangeBidHousePriceMessage(short GenId)
	{
		this.GenId = GenId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.GenId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.GenId = reader.ReadVarShort();
	}
}
}
