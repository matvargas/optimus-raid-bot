using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class QuestActiveDetailedInformations : QuestActiveInformations
{

	public const uint Id = 382;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short StepId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public QuestObjectiveInformations[] Objectives { get; set; }

	public QuestActiveDetailedInformations() {}


	public QuestActiveDetailedInformations InitQuestActiveDetailedInformations(short StepId, QuestObjectiveInformations[] Objectives)
	{
		this.StepId = StepId;
		this.Objectives = Objectives;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.StepId);
		writer.WriteShort(this.Objectives.Length);
		foreach (QuestObjectiveInformations item in this.Objectives)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.StepId = reader.ReadVarShort();
		int ObjectivesLen = reader.ReadShort();
		Objectives = new QuestObjectiveInformations[ObjectivesLen];
		for (int i = 0; i < ObjectivesLen; i++)
		{
			this.Objectives[i] = ProtocolTypeManager.GetInstance<QuestObjectiveInformations>(reader.ReadShort());
			this.Objectives[i].Deserialize(reader);
		}
	}
}
}
