using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeCraftCountModifiedMessage : NetworkMessage
{

	public const uint Id = 6595;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Count { get; set; }

	public ExchangeCraftCountModifiedMessage() {}


	public ExchangeCraftCountModifiedMessage InitExchangeCraftCountModifiedMessage(int Count)
	{
		this.Count = Count;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.Count);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Count = reader.ReadVarInt();
	}
}
}
