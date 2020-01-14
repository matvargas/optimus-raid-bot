using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class QuestListMessage : NetworkMessage
{

	public const uint Id = 5626;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] FinishedQuestsIds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] FinishedQuestsCounts { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public QuestActiveInformations[] ActiveQuests { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] ReinitDoneQuestsIds { get; set; }

	public QuestListMessage() {}


	public QuestListMessage InitQuestListMessage(short[] FinishedQuestsIds, short[] FinishedQuestsCounts, QuestActiveInformations[] ActiveQuests, short[] ReinitDoneQuestsIds)
	{
		this.FinishedQuestsIds = FinishedQuestsIds;
		this.FinishedQuestsCounts = FinishedQuestsCounts;
		this.ActiveQuests = ActiveQuests;
		this.ReinitDoneQuestsIds = ReinitDoneQuestsIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.FinishedQuestsIds.Length);
		foreach (short item in this.FinishedQuestsIds)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.FinishedQuestsCounts.Length);
		foreach (short item in this.FinishedQuestsCounts)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.ActiveQuests.Length);
		foreach (QuestActiveInformations item in this.ActiveQuests)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteShort(this.ReinitDoneQuestsIds.Length);
		foreach (short item in this.ReinitDoneQuestsIds)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int FinishedQuestsIdsLen = reader.ReadShort();
		FinishedQuestsIds = new short[FinishedQuestsIdsLen];
		for (int i = 0; i < FinishedQuestsIdsLen; i++)
		{
			this.FinishedQuestsIds[i] = reader.ReadVarShort();
		}
		int FinishedQuestsCountsLen = reader.ReadShort();
		FinishedQuestsCounts = new short[FinishedQuestsCountsLen];
		for (int i = 0; i < FinishedQuestsCountsLen; i++)
		{
			this.FinishedQuestsCounts[i] = reader.ReadVarShort();
		}
		int ActiveQuestsLen = reader.ReadShort();
		ActiveQuests = new QuestActiveInformations[ActiveQuestsLen];
		for (int i = 0; i < ActiveQuestsLen; i++)
		{
			this.ActiveQuests[i] = ProtocolTypeManager.GetInstance<QuestActiveInformations>(reader.ReadShort());
			this.ActiveQuests[i].Deserialize(reader);
		}
		int ReinitDoneQuestsIdsLen = reader.ReadShort();
		ReinitDoneQuestsIds = new short[ReinitDoneQuestsIdsLen];
		for (int i = 0; i < ReinitDoneQuestsIdsLen; i++)
		{
			this.ReinitDoneQuestsIds[i] = reader.ReadVarShort();
		}
	}
}
}
