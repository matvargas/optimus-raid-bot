using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DebugHighlightCellsMessage : NetworkMessage
{

	public const uint Id = 2001;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double Color { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Cells { get; set; }

	public DebugHighlightCellsMessage() {}


	public DebugHighlightCellsMessage InitDebugHighlightCellsMessage(double Color, short[] Cells)
	{
		this.Color = Color;
		this.Cells = Cells;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.Color);
		writer.WriteShort(this.Cells.Length);
		foreach (short item in this.Cells)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Color = reader.ReadDouble();
		int CellsLen = reader.ReadShort();
		Cells = new short[CellsLen];
		for (int i = 0; i < CellsLen; i++)
		{
			this.Cells[i] = reader.ReadVarShort();
		}
	}
}
}
