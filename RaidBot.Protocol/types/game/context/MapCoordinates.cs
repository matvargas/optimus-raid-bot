using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class MapCoordinates : NetworkType
{

	public const uint Id = 174;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldX { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldY { get; set; }

	public MapCoordinates() {}


	public MapCoordinates InitMapCoordinates(short WorldX, short WorldY)
	{
		this.WorldX = WorldX;
		this.WorldY = WorldY;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.WorldX);
		writer.WriteShort(this.WorldY);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.WorldX = reader.ReadShort();
		this.WorldY = reader.ReadShort();
	}
}
}
