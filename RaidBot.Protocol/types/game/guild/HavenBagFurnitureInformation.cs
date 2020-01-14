using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HavenBagFurnitureInformation : NetworkType
{

	public const uint Id = 498;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int FunitureId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Orientation { get; set; }

	public HavenBagFurnitureInformation() {}


	public HavenBagFurnitureInformation InitHavenBagFurnitureInformation(short CellId, int FunitureId, byte Orientation)
	{
		this.CellId = CellId;
		this.FunitureId = FunitureId;
		this.Orientation = Orientation;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.CellId);
		writer.WriteInt(this.FunitureId);
		writer.WriteByte(this.Orientation);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CellId = reader.ReadVarShort();
		this.FunitureId = reader.ReadInt();
		this.Orientation = reader.ReadByte();
	}
}
}
