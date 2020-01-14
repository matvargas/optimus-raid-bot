using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HelloConnectMessage : NetworkMessage
{

	public const uint Id = 3;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Salt { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] Key { get; set; }

	public HelloConnectMessage() {}


	public HelloConnectMessage InitHelloConnectMessage(String Salt, byte[] Key)
	{
		this.Salt = Salt;
		this.Key = Key;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.Salt);
		writer.WriteVarInt(this.Key.Length);
		foreach (byte item in this.Key)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Salt = reader.ReadUTF();
		int KeyLen = reader.ReadVarInt();
		Key = new byte[KeyLen];
		for (int i = 0; i < KeyLen; i++)
		{
			this.Key[i] = reader.ReadByte();
		}
	}
}
}
