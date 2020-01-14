using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeIsReadyMessage : NetworkMessage
{

	public const uint Id = 5509;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Ready { get; set; }

	public ExchangeIsReadyMessage() {}


	public ExchangeIsReadyMessage InitExchangeIsReadyMessage(double Id_, bool Ready)
	{
		this.Id_ = Id_;
		this.Ready = Ready;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.Id_);
		writer.WriteBoolean(this.Ready);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Id_ = reader.ReadDouble();
		this.Ready = reader.ReadBoolean();
	}
}
}
