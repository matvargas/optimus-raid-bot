using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ObjectUseOnCellMessage : ObjectUseMessage
{

	public const uint Id = 3013;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Cells { get; set; }

	public ObjectUseOnCellMessage() {}


	public ObjectUseOnCellMessage InitObjectUseOnCellMessage(short Cells)
	{
		this.Cells = Cells;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.Cells);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Cells = reader.ReadVarShort();
	}
}
}
