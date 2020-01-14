using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DebugInClientMessage : NetworkMessage
{

	public const uint Id = 6028;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Level { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Message { get; set; }

	public DebugInClientMessage() {}


	public DebugInClientMessage InitDebugInClientMessage(byte Level, String Message)
	{
		this.Level = Level;
		this.Message = Message;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Level);
		writer.WriteUTF(this.Message);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Level = reader.ReadByte();
		this.Message = reader.ReadUTF();
	}
}
}
