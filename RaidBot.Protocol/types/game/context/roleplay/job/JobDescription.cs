using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class JobDescription : NetworkType
{

	public const uint Id = 101;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte JobId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public SkillActionDescription[] Skills { get; set; }

	public JobDescription() {}


	public JobDescription InitJobDescription(byte JobId, SkillActionDescription[] Skills)
	{
		this.JobId = JobId;
		this.Skills = Skills;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.JobId);
		writer.WriteShort(this.Skills.Length);
		foreach (SkillActionDescription item in this.Skills)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.JobId = reader.ReadByte();
		int SkillsLen = reader.ReadShort();
		Skills = new SkillActionDescription[SkillsLen];
		for (int i = 0; i < SkillsLen; i++)
		{
			this.Skills[i] = ProtocolTypeManager.GetInstance<SkillActionDescription>(reader.ReadShort());
			this.Skills[i].Deserialize(reader);
		}
	}
}
}
