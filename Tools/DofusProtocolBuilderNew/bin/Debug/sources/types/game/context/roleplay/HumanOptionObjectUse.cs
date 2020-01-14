using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HumanOptionObjectUse : HumanOption
{

	public const uint Id = 449;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte DelayTypeId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double DelayEndTime { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ObjectGID { get; set; }

	public HumanOptionObjectUse() {}


	public HumanOptionObjectUse InitHumanOptionObjectUse(byte DelayTypeId, double DelayEndTime, short ObjectGID)
	{
		this.DelayTypeId = DelayTypeId;
		this.DelayEndTime = DelayEndTime;
		this.ObjectGID = ObjectGID;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.DelayTypeId);
		writer.WriteDouble(this.DelayEndTime);
		writer.WriteVarShort(this.ObjectGID);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.DelayTypeId = reader.ReadByte();
		this.DelayEndTime = reader.ReadDouble();
		this.ObjectGID = reader.ReadVarShort();
	}
}
}
