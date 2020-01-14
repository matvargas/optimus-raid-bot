using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class RawDataMessage : NetworkMessage
{

	public const uint Id = 6253;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] Content { get; set; }

	public RawDataMessage() {}


	public RawDataMessage InitRawDataMessage(byte[] Content)
	{
		this.Content = Content;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.Content.Length);
		foreach (byte item in this.Content)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ContentLen = reader.ReadVarInt();
		Content = new byte[ContentLen];
		for (int i = 0; i < ContentLen; i++)
		{
			this.Content[i] = reader.ReadByte();
		}
	}
}
}
