using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeRequestedTradeMessage : ExchangeRequestedMessage
{

	public const uint Id = 5523;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Source { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Target { get; set; }

	public ExchangeRequestedTradeMessage() {}


	public ExchangeRequestedTradeMessage InitExchangeRequestedTradeMessage(long Source, long Target)
	{
		this.Source = Source;
		this.Target = Target;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.Source);
		writer.WriteVarLong(this.Target);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Source = reader.ReadVarLong();
		this.Target = reader.ReadVarLong();
	}
}
}
