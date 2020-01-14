using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CompassUpdateMessage : NetworkMessage
{

	public const uint Id = 5591;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Type { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public MapCoordinates Coords { get; set; }

	public CompassUpdateMessage() {}


	public CompassUpdateMessage InitCompassUpdateMessage(byte Type, MapCoordinates Coords)
	{
		this.Type = Type;
		this.Coords = Coords;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Type);
writer.WriteShort(Coords.MessageId);
		Coords.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Type = reader.ReadByte();
this.Coords = ProtocolTypeManager.GetInstance<MapCoordinates>(reader.ReadShort());
		this.Coords.Deserialize(reader);
	}
}
}
