using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class SequenceEndMessage : NetworkMessage
{

	public const uint Id = 956;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ActionId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double AuthorId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte SequenceType { get; set; }

	public SequenceEndMessage() {}


	public SequenceEndMessage InitSequenceEndMessage(short ActionId, double AuthorId, byte SequenceType)
	{
		this.ActionId = ActionId;
		this.AuthorId = AuthorId;
		this.SequenceType = SequenceType;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.ActionId);
		writer.WriteDouble(this.AuthorId);
		writer.WriteByte(this.SequenceType);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ActionId = reader.ReadVarShort();
		this.AuthorId = reader.ReadDouble();
		this.SequenceType = reader.ReadByte();
	}
}
}
