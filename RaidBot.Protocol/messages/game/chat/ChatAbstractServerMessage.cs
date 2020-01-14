using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ChatAbstractServerMessage : NetworkMessage
{

	public const uint Id = 880;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Channel { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Content { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Timestamp { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Fingerprint { get; set; }

	public ChatAbstractServerMessage() {}


	public ChatAbstractServerMessage InitChatAbstractServerMessage(byte Channel, String Content, int Timestamp, String Fingerprint)
	{
		this.Channel = Channel;
		this.Content = Content;
		this.Timestamp = Timestamp;
		this.Fingerprint = Fingerprint;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Channel);
		writer.WriteUTF(this.Content);
		writer.WriteInt(this.Timestamp);
		writer.WriteUTF(this.Fingerprint);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Channel = reader.ReadByte();
		this.Content = reader.ReadUTF();
		this.Timestamp = reader.ReadInt();
		this.Fingerprint = reader.ReadUTF();
	}
}
}
