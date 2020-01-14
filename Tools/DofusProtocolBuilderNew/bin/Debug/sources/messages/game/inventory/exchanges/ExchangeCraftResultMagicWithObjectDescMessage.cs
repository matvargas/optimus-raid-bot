using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeCraftResultMagicWithObjectDescMessage : ExchangeCraftResultWithObjectDescMessage
{

	public const uint Id = 6188;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte MagicPoolStatus { get; set; }

	public ExchangeCraftResultMagicWithObjectDescMessage() {}


	public ExchangeCraftResultMagicWithObjectDescMessage InitExchangeCraftResultMagicWithObjectDescMessage(byte MagicPoolStatus)
	{
		this.MagicPoolStatus = MagicPoolStatus;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.MagicPoolStatus);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.MagicPoolStatus = reader.ReadByte();
	}
}
}
