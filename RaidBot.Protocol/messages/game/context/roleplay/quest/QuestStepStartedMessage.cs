using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class QuestStepStartedMessage : NetworkMessage
{

	public const uint Id = 6096;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short QuestId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short StepId { get; set; }

	public QuestStepStartedMessage() {}


	public QuestStepStartedMessage InitQuestStepStartedMessage(short QuestId, short StepId)
	{
		this.QuestId = QuestId;
		this.StepId = StepId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.QuestId);
		writer.WriteVarShort(this.StepId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.QuestId = reader.ReadVarShort();
		this.StepId = reader.ReadVarShort();
	}
}
}
