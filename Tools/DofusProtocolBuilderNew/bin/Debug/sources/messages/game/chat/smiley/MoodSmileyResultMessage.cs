using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MoodSmileyResultMessage : NetworkMessage
{

	public const uint Id = 6196;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte ResultCode { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SmileyId { get; set; }

	public MoodSmileyResultMessage() {}


	public MoodSmileyResultMessage InitMoodSmileyResultMessage(byte ResultCode, short SmileyId)
	{
		this.ResultCode = ResultCode;
		this.SmileyId = SmileyId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.ResultCode);
		writer.WriteVarShort(this.SmileyId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ResultCode = reader.ReadByte();
		this.SmileyId = reader.ReadVarShort();
	}
}
}
