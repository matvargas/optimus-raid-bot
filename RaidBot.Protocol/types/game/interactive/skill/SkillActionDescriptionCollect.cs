using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class SkillActionDescriptionCollect : SkillActionDescriptionTimed
{

	public const uint Id = 99;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Min { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Max { get; set; }

	public SkillActionDescriptionCollect() {}


	public SkillActionDescriptionCollect InitSkillActionDescriptionCollect(short Min, short Max)
	{
		this.Min = Min;
		this.Max = Max;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.Min);
		writer.WriteVarShort(this.Max);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Min = reader.ReadVarShort();
		this.Max = reader.ReadVarShort();
	}
}
}
