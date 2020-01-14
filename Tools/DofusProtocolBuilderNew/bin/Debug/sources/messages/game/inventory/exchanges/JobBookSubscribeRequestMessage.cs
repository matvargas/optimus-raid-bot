using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class JobBookSubscribeRequestMessage : NetworkMessage
{

	public const uint Id = 6592;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] JobIds { get; set; }

	public JobBookSubscribeRequestMessage() {}


	public JobBookSubscribeRequestMessage InitJobBookSubscribeRequestMessage(byte[] JobIds)
	{
		this.JobIds = JobIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.JobIds.Length);
		foreach (byte item in this.JobIds)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int JobIdsLen = reader.ReadShort();
		JobIds = new byte[JobIdsLen];
		for (int i = 0; i < JobIdsLen; i++)
		{
			this.JobIds[i] = reader.ReadByte();
		}
	}
}
}
