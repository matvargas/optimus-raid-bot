using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ChatServerMessage : ChatAbstractServerMessage
{

	public const uint Id = 881;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double SenderId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String SenderName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SenderAccountId { get; set; }

	public ChatServerMessage() {}


	public ChatServerMessage InitChatServerMessage(double SenderId, String SenderName, int SenderAccountId)
	{
		this.SenderId = SenderId;
		this.SenderName = SenderName;
		this.SenderAccountId = SenderAccountId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.SenderId);
		writer.WriteUTF(this.SenderName);
		writer.WriteInt(this.SenderAccountId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.SenderId = reader.ReadDouble();
		this.SenderName = reader.ReadUTF();
		this.SenderAccountId = reader.ReadInt();
	}
}
}
