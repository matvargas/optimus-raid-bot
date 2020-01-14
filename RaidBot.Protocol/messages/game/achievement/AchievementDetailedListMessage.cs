using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AchievementDetailedListMessage : NetworkMessage
{

	public const uint Id = 6358;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public Achievement[] StartedAchievements { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public Achievement[] FinishedAchievements { get; set; }

	public AchievementDetailedListMessage() {}


	public AchievementDetailedListMessage InitAchievementDetailedListMessage(Achievement[] StartedAchievements, Achievement[] FinishedAchievements)
	{
		this.StartedAchievements = StartedAchievements;
		this.FinishedAchievements = FinishedAchievements;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.StartedAchievements.Length);
		foreach (Achievement item in this.StartedAchievements)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.FinishedAchievements.Length);
		foreach (Achievement item in this.FinishedAchievements)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int StartedAchievementsLen = reader.ReadShort();
		StartedAchievements = new Achievement[StartedAchievementsLen];
		for (int i = 0; i < StartedAchievementsLen; i++)
		{
			this.StartedAchievements[i] = new Achievement();
			this.StartedAchievements[i].Deserialize(reader);
		}
		int FinishedAchievementsLen = reader.ReadShort();
		FinishedAchievements = new Achievement[FinishedAchievementsLen];
		for (int i = 0; i < FinishedAchievementsLen; i++)
		{
			this.FinishedAchievements[i] = new Achievement();
			this.FinishedAchievements[i].Deserialize(reader);
		}
	}
}
}
