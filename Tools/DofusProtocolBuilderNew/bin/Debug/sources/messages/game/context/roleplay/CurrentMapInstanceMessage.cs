using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CurrentMapInstanceMessage : CurrentMapMessage
{

	public const uint Id = 6738;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double InstantiatedMapId { get; set; }

	public CurrentMapInstanceMessage() {}


	public CurrentMapInstanceMessage InitCurrentMapInstanceMessage(double InstantiatedMapId)
	{
		this.InstantiatedMapId = InstantiatedMapId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.InstantiatedMapId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.InstantiatedMapId = reader.ReadDouble();
	}
}
}
