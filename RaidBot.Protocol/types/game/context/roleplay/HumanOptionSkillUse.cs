using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HumanOptionSkillUse : HumanOption
{

	public const uint Id = 495;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ElementId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SkillId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double SkillEndTime { get; set; }

	public HumanOptionSkillUse() {}


	public HumanOptionSkillUse InitHumanOptionSkillUse(int ElementId, short SkillId, double SkillEndTime)
	{
		this.ElementId = ElementId;
		this.SkillId = SkillId;
		this.SkillEndTime = SkillEndTime;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.ElementId);
		writer.WriteVarShort(this.SkillId);
		writer.WriteDouble(this.SkillEndTime);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.ElementId = reader.ReadVarInt();
		this.SkillId = reader.ReadVarShort();
		this.SkillEndTime = reader.ReadDouble();
	}
}
}
