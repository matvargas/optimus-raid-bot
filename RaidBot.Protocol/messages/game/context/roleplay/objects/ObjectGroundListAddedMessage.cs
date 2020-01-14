using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ObjectGroundListAddedMessage : NetworkMessage
{

	public const uint Id = 5925;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Cells { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] ReferenceIds { get; set; }

	public ObjectGroundListAddedMessage() {}


	public ObjectGroundListAddedMessage InitObjectGroundListAddedMessage(short[] Cells, short[] ReferenceIds)
	{
		this.Cells = Cells;
		this.ReferenceIds = ReferenceIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Cells.Length);
		foreach (short item in this.Cells)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.ReferenceIds.Length);
		foreach (short item in this.ReferenceIds)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int CellsLen = reader.ReadShort();
		Cells = new short[CellsLen];
		for (int i = 0; i < CellsLen; i++)
		{
			this.Cells[i] = reader.ReadVarShort();
		}
		int ReferenceIdsLen = reader.ReadShort();
		ReferenceIds = new short[ReferenceIdsLen];
		for (int i = 0; i < ReferenceIdsLen; i++)
		{
			this.ReferenceIds[i] = reader.ReadVarShort();
		}
	}
}
}
