using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeBidHouseSearchMessage : NetworkMessage
{

	public const uint Id = 5806;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Type { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short GenId { get; set; }

	public ExchangeBidHouseSearchMessage() {}


	public ExchangeBidHouseSearchMessage InitExchangeBidHouseSearchMessage(int Type, short GenId)
	{
		this.Type = Type;
		this.GenId = GenId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.Type);
		writer.WriteVarShort(this.GenId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Type = reader.ReadVarInt();
		this.GenId = reader.ReadVarShort();
	}
}
}
