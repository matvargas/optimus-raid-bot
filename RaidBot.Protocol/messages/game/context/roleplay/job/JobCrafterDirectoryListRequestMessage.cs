using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class JobCrafterDirectoryListRequestMessage : NetworkMessage
{

	public const uint Id = 6047;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte JobId { get; set; }

	public JobCrafterDirectoryListRequestMessage() {}


	public JobCrafterDirectoryListRequestMessage InitJobCrafterDirectoryListRequestMessage(byte JobId)
	{
		this.JobId = JobId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.JobId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.JobId = reader.ReadByte();
	}
}
}
