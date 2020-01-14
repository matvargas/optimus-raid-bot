using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class JobDescriptionMessage : NetworkMessage
{

	public const uint Id = 5655;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public JobDescription[] JobsDescription { get; set; }

	public JobDescriptionMessage() {}


	public JobDescriptionMessage InitJobDescriptionMessage(JobDescription[] JobsDescription)
	{
		this.JobsDescription = JobsDescription;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.JobsDescription.Length);
		foreach (JobDescription item in this.JobsDescription)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int JobsDescriptionLen = reader.ReadShort();
		JobsDescription = new JobDescription[JobsDescriptionLen];
		for (int i = 0; i < JobsDescriptionLen; i++)
		{
			this.JobsDescription[i] = new JobDescription();
			this.JobsDescription[i].Deserialize(reader);
		}
	}
}
}
