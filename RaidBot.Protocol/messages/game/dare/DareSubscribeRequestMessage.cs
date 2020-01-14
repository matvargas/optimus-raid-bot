using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DareSubscribeRequestMessage : NetworkMessage
{

	public const uint Id = 6666;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double DareId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Subscribe { get; set; }

	public DareSubscribeRequestMessage() {}


	public DareSubscribeRequestMessage InitDareSubscribeRequestMessage(double DareId, bool Subscribe)
	{
		this.DareId = DareId;
		this.Subscribe = Subscribe;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.DareId);
		writer.WriteBoolean(this.Subscribe);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DareId = reader.ReadDouble();
		this.Subscribe = reader.ReadBoolean();
	}
}
}
