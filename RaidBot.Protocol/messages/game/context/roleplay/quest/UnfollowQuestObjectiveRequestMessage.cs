using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class UnfollowQuestObjectiveRequestMessage : NetworkMessage
{

	public const uint Id = 6723;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short QuestId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ObjectiveId { get; set; }

	public UnfollowQuestObjectiveRequestMessage() {}


	public UnfollowQuestObjectiveRequestMessage InitUnfollowQuestObjectiveRequestMessage(short QuestId, short ObjectiveId)
	{
		this.QuestId = QuestId;
		this.ObjectiveId = ObjectiveId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.QuestId);
		writer.WriteShort(this.ObjectiveId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.QuestId = reader.ReadVarShort();
		this.ObjectiveId = reader.ReadShort();
	}
}
}
