using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CheckFileMessage : NetworkMessage
{

	public const uint Id = 6156;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String FilenameHash { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Type { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Value { get; set; }

	public CheckFileMessage() {}


	public CheckFileMessage InitCheckFileMessage(String FilenameHash, byte Type, String Value)
	{
		this.FilenameHash = FilenameHash;
		this.Type = Type;
		this.Value = Value;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.FilenameHash);
		writer.WriteByte(this.Type);
		writer.WriteUTF(this.Value);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FilenameHash = reader.ReadUTF();
		this.Type = reader.ReadByte();
		this.Value = reader.ReadUTF();
	}
}
}
