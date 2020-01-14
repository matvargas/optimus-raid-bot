using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangePlayerRequestMessage : ExchangeRequestMessage
{

	public const uint Id = 5773;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Target { get; set; }

	public ExchangePlayerRequestMessage() {}


	public ExchangePlayerRequestMessage InitExchangePlayerRequestMessage(long Target)
	{
		this.Target = Target;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.Target);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Target = reader.ReadVarLong();
	}
}
}
