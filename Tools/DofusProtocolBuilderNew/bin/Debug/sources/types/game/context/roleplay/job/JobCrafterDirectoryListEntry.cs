using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class JobCrafterDirectoryListEntry : NetworkType
{

	public const uint Id = 196;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public JobCrafterDirectoryEntryPlayerInfo PlayerInfo { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public JobCrafterDirectoryEntryJobInfo JobInfo { get; set; }

	public JobCrafterDirectoryListEntry() {}


	public JobCrafterDirectoryListEntry InitJobCrafterDirectoryListEntry(JobCrafterDirectoryEntryPlayerInfo PlayerInfo, JobCrafterDirectoryEntryJobInfo JobInfo)
	{
		this.PlayerInfo = PlayerInfo;
		this.JobInfo = JobInfo;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.PlayerInfo.Serialize(writer);
		this.JobInfo.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PlayerInfo = new JobCrafterDirectoryEntryPlayerInfo();
		this.PlayerInfo.Deserialize(reader);
		this.JobInfo = new JobCrafterDirectoryEntryJobInfo();
		this.JobInfo.Deserialize(reader);
	}
}
}
