using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class JobCrafterDirectoryListMessage : NetworkMessage
{

	public const uint Id = 6046;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public JobCrafterDirectoryListEntry[] ListEntries { get; set; }

	public JobCrafterDirectoryListMessage() {}


	public JobCrafterDirectoryListMessage InitJobCrafterDirectoryListMessage(JobCrafterDirectoryListEntry[] ListEntries)
	{
		this.ListEntries = ListEntries;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.ListEntries.Length);
		foreach (JobCrafterDirectoryListEntry item in this.ListEntries)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ListEntriesLen = reader.ReadShort();
		ListEntries = new JobCrafterDirectoryListEntry[ListEntriesLen];
		for (int i = 0; i < ListEntriesLen; i++)
		{
			this.ListEntries[i] = new JobCrafterDirectoryListEntry();
			this.ListEntries[i].Deserialize(reader);
		}
	}
}
}
