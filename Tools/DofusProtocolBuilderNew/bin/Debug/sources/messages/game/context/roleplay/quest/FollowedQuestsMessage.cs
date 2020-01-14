using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class FollowedQuestsMessage : NetworkMessage
{

	public const uint Id = 6717;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public QuestActiveDetailedInformations[] Quests { get; set; }

	public FollowedQuestsMessage() {}


	public FollowedQuestsMessage InitFollowedQuestsMessage(QuestActiveDetailedInformations[] Quests)
	{
		this.Quests = Quests;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Quests.Length);
		foreach (QuestActiveDetailedInformations item in this.Quests)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int QuestsLen = reader.ReadShort();
		Quests = new QuestActiveDetailedInformations[QuestsLen];
		for (int i = 0; i < QuestsLen; i++)
		{
			this.Quests[i] = new QuestActiveDetailedInformations();
			this.Quests[i].Deserialize(reader);
		}
	}
}
}
