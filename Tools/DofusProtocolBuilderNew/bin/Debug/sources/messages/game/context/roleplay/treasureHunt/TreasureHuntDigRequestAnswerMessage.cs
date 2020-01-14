using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TreasureHuntDigRequestAnswerMessage : NetworkMessage
{

	public const uint Id = 6484;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte QuestType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Result { get; set; }

	public TreasureHuntDigRequestAnswerMessage() {}


	public TreasureHuntDigRequestAnswerMessage InitTreasureHuntDigRequestAnswerMessage(byte QuestType, byte Result)
	{
		this.QuestType = QuestType;
		this.Result = Result;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.QuestType);
		writer.WriteByte(this.Result);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.QuestType = reader.ReadByte();
		this.Result = reader.ReadByte();
	}
}
}
