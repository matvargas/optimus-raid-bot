using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ZaapListMessage : TeleportDestinationsListMessage
{

	public const uint Id = 1604;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double SpawnMapId { get; set; }

	public ZaapListMessage() {}


	public ZaapListMessage InitZaapListMessage(double SpawnMapId)
	{
		this.SpawnMapId = SpawnMapId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.SpawnMapId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.SpawnMapId = reader.ReadDouble();
	}
}
}
