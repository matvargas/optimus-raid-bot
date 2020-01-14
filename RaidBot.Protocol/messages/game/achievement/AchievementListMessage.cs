using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AchievementListMessage : NetworkMessage
{

	public const uint Id = 6205;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AchievementAchieved[] FinishedAchievements { get; set; }

	public AchievementListMessage() {}


	public AchievementListMessage InitAchievementListMessage(AchievementAchieved[] FinishedAchievements)
	{
		this.FinishedAchievements = FinishedAchievements;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.FinishedAchievements.Length);
		foreach (AchievementAchieved item in this.FinishedAchievements)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int FinishedAchievementsLen = reader.ReadShort();
		FinishedAchievements = new AchievementAchieved[FinishedAchievementsLen];
		for (int i = 0; i < FinishedAchievementsLen; i++)
		{
			this.FinishedAchievements[i] = ProtocolTypeManager.GetInstance<AchievementAchieved>(reader.ReadShort());
			this.FinishedAchievements[i].Deserialize(reader);
		}
	}
}
}
