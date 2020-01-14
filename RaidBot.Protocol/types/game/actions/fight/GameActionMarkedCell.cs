using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameActionMarkedCell : NetworkType
{

	public const uint Id = 85;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte ZoneSize { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int CellColor { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte CellsType { get; set; }

	public GameActionMarkedCell() {}


	public GameActionMarkedCell InitGameActionMarkedCell(short CellId, byte ZoneSize, int CellColor, byte CellsType)
	{
		this.CellId = CellId;
		this.ZoneSize = ZoneSize;
		this.CellColor = CellColor;
		this.CellsType = CellsType;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.CellId);
		writer.WriteByte(this.ZoneSize);
		writer.WriteInt(this.CellColor);
		writer.WriteByte(this.CellsType);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CellId = reader.ReadVarShort();
		this.ZoneSize = reader.ReadByte();
		this.CellColor = reader.ReadInt();
		this.CellsType = reader.ReadByte();
	}
}
}
