using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class JobCrafterDirectoryAddMessage : NetworkMessage
{

	public const uint Id = 5651;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public JobCrafterDirectoryListEntry ListEntry { get; set; }

	public JobCrafterDirectoryAddMessage() {}


	public JobCrafterDirectoryAddMessage InitJobCrafterDirectoryAddMessage(JobCrafterDirectoryListEntry ListEntry)
	{
		this.ListEntry = ListEntry;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.ListEntry.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ListEntry = new JobCrafterDirectoryListEntry();
		this.ListEntry.Deserialize(reader);
	}
}
}
