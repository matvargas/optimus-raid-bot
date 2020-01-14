using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MoodSmileyUpdateMessage : NetworkMessage
{

	public const uint Id = 6388;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AccountId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long PlayerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SmileyId { get; set; }

	public MoodSmileyUpdateMessage() {}


	public MoodSmileyUpdateMessage InitMoodSmileyUpdateMessage(int AccountId, long PlayerId, short SmileyId)
	{
		this.AccountId = AccountId;
		this.PlayerId = PlayerId;
		this.SmileyId = SmileyId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.AccountId);
		writer.WriteVarLong(this.PlayerId);
		writer.WriteVarShort(this.SmileyId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AccountId = reader.ReadInt();
		this.PlayerId = reader.ReadVarLong();
		this.SmileyId = reader.ReadVarShort();
	}
}
}
