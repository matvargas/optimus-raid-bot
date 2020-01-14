using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeCraftResultMessage : NetworkMessage
{

	public const uint Id = 5790;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte CraftResult { get; set; }

	public ExchangeCraftResultMessage() {}


	public ExchangeCraftResultMessage InitExchangeCraftResultMessage(byte CraftResult)
	{
		this.CraftResult = CraftResult;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.CraftResult);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CraftResult = reader.ReadByte();
	}
}
}
