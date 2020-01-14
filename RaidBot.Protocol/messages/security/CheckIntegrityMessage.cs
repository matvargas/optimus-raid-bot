using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CheckIntegrityMessage : NetworkMessage
{

	public const uint Id = 6372;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] Data { get; set; }

	public CheckIntegrityMessage() {}


	public CheckIntegrityMessage InitCheckIntegrityMessage(byte[] Data)
	{
		this.Data = Data;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.Data.Length);
		foreach (byte item in this.Data)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int DataLen = reader.ReadVarInt();
		Data = new byte[DataLen];
		for (int i = 0; i < DataLen; i++)
		{
			this.Data[i] = reader.ReadByte();
		}
	}
}
}
