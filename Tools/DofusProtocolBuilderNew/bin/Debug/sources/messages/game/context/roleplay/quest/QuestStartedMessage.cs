using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class QuestStartedMessage : NetworkMessage
{

	public const uint Id = 6091;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short QuestId { get; set; }

	public QuestStartedMessage() {}


	public QuestStartedMessage InitQuestStartedMessage(short QuestId)
	{
		this.QuestId = QuestId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.QuestId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.QuestId = reader.ReadVarShort();
	}
}
}
