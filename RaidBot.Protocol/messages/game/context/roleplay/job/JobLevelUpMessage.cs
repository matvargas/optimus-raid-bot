using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class JobLevelUpMessage : NetworkMessage
{

	public const uint Id = 5656;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NewLevel { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public JobDescription JobsDescription { get; set; }

	public JobLevelUpMessage() {}


	public JobLevelUpMessage InitJobLevelUpMessage(byte NewLevel, JobDescription JobsDescription)
	{
		this.NewLevel = NewLevel;
		this.JobsDescription = JobsDescription;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.NewLevel);
		this.JobsDescription.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.NewLevel = reader.ReadByte();
		this.JobsDescription = new JobDescription();
		this.JobsDescription.Deserialize(reader);
	}
}
}
