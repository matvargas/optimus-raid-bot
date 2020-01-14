using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class Achievement : NetworkType
{

	public const uint Id = 363;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AchievementObjective[] FinishedObjective { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AchievementStartedObjective[] StartedObjectives { get; set; }

	public Achievement() {}


	public Achievement InitAchievement(short Id_, AchievementObjective[] FinishedObjective, AchievementStartedObjective[] StartedObjectives)
	{
		this.Id_ = Id_;
		this.FinishedObjective = FinishedObjective;
		this.StartedObjectives = StartedObjectives;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.Id_);
		writer.WriteShort(this.FinishedObjective.Length);
		foreach (AchievementObjective item in this.FinishedObjective)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.StartedObjectives.Length);
		foreach (AchievementStartedObjective item in this.StartedObjectives)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Id_ = reader.ReadVarShort();
		int FinishedObjectiveLen = reader.ReadShort();
		FinishedObjective = new AchievementObjective[FinishedObjectiveLen];
		for (int i = 0; i < FinishedObjectiveLen; i++)
		{
			this.FinishedObjective[i] = new AchievementObjective();
			this.FinishedObjective[i].Deserialize(reader);
		}
		int StartedObjectivesLen = reader.ReadShort();
		StartedObjectives = new AchievementStartedObjective[StartedObjectivesLen];
		for (int i = 0; i < StartedObjectivesLen; i++)
		{
			this.StartedObjectives[i] = new AchievementStartedObjective();
			this.StartedObjectives[i].Deserialize(reader);
		}
	}
}
}
