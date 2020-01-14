using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class AtlasPointsInformations : NetworkType
{

	public const uint Id = 175;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Type { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public MapCoordinatesExtended[] Coords { get; set; }

	public AtlasPointsInformations() {}


	public AtlasPointsInformations InitAtlasPointsInformations(byte Type, MapCoordinatesExtended[] Coords)
	{
		this.Type = Type;
		this.Coords = Coords;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Type);
		writer.WriteShort(this.Coords.Length);
		foreach (MapCoordinatesExtended item in this.Coords)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Type = reader.ReadByte();
		int CoordsLen = reader.ReadShort();
		Coords = new MapCoordinatesExtended[CoordsLen];
		for (int i = 0; i < CoordsLen; i++)
		{
			this.Coords[i] = new MapCoordinatesExtended();
			this.Coords[i].Deserialize(reader);
		}
	}
}
}
