using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class JobExperienceMultiUpdateMessage : NetworkMessage
{

	public const uint Id = 5809;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public JobExperience[] ExperiencesUpdate { get; set; }

	public JobExperienceMultiUpdateMessage() {}


	public JobExperienceMultiUpdateMessage InitJobExperienceMultiUpdateMessage(JobExperience[] ExperiencesUpdate)
	{
		this.ExperiencesUpdate = ExperiencesUpdate;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.ExperiencesUpdate.Length);
		foreach (JobExperience item in this.ExperiencesUpdate)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ExperiencesUpdateLen = reader.ReadShort();
		ExperiencesUpdate = new JobExperience[ExperiencesUpdateLen];
		for (int i = 0; i < ExperiencesUpdateLen; i++)
		{
			this.ExperiencesUpdate[i] = new JobExperience();
			this.ExperiencesUpdate[i].Deserialize(reader);
		}
	}
}
}
