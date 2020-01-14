using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PopupWarningMessage : NetworkMessage
{

	public const uint Id = 6134;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte LockDuration { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Author { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Content { get; set; }

	public PopupWarningMessage() {}


	public PopupWarningMessage InitPopupWarningMessage(byte LockDuration, String Author, String Content)
	{
		this.LockDuration = LockDuration;
		this.Author = Author;
		this.Content = Content;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.LockDuration);
		writer.WriteUTF(this.Author);
		writer.WriteUTF(this.Content);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.LockDuration = reader.ReadByte();
		this.Author = reader.ReadUTF();
		this.Content = reader.ReadUTF();
	}
}
}
