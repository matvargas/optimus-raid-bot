using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HavenBagPackListMessage : NetworkMessage
{

	public const uint Id = 6620;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] PackIds { get; set; }

	public HavenBagPackListMessage() {}


	public HavenBagPackListMessage InitHavenBagPackListMessage(byte[] PackIds)
	{
		this.PackIds = PackIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.PackIds.Length);
		foreach (byte item in this.PackIds)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int PackIdsLen = reader.ReadShort();
		PackIds = new byte[PackIdsLen];
		for (int i = 0; i < PackIdsLen; i++)
		{
			this.PackIds[i] = reader.ReadByte();
		}
	}
}
}
