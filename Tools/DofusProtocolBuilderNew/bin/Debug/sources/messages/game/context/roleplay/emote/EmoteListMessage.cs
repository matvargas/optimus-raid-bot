using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class EmoteListMessage : NetworkMessage
{

	public const uint Id = 5689;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] EmoteIds { get; set; }

	public EmoteListMessage() {}


	public EmoteListMessage InitEmoteListMessage(byte[] EmoteIds)
	{
		this.EmoteIds = EmoteIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.EmoteIds.Length);
		foreach (byte item in this.EmoteIds)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int EmoteIdsLen = reader.ReadShort();
		EmoteIds = new byte[EmoteIdsLen];
		for (int i = 0; i < EmoteIdsLen; i++)
		{
			this.EmoteIds[i] = reader.ReadByte();
		}
	}
}
}
