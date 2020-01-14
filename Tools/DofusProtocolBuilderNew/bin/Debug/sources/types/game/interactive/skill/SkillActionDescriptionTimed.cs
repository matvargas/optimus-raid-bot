using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class SkillActionDescriptionTimed : SkillActionDescription
{

	public const uint Id = 103;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Time { get; set; }

	public SkillActionDescriptionTimed() {}


	public SkillActionDescriptionTimed InitSkillActionDescriptionTimed(byte Time)
	{
		this.Time = Time;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.Time);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Time = reader.ReadByte();
	}
}
}
