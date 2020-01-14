using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class SkillActionDescription : NetworkType
{

	public const uint Id = 102;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SkillId { get; set; }

	public SkillActionDescription() {}


	public SkillActionDescription InitSkillActionDescription(short SkillId)
	{
		this.SkillId = SkillId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SkillId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SkillId = reader.ReadVarShort();
	}
}
}
