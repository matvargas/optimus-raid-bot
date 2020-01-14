using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ObjectItemInRolePlay : NetworkType
{

	public const uint Id = 198;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ObjectGID { get; set; }

	public ObjectItemInRolePlay() {}


	public ObjectItemInRolePlay InitObjectItemInRolePlay(short CellId, short ObjectGID)
	{
		this.CellId = CellId;
		this.ObjectGID = ObjectGID;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.CellId);
		writer.WriteVarShort(this.ObjectGID);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CellId = reader.ReadVarShort();
		this.ObjectGID = reader.ReadVarShort();
	}
}
}
