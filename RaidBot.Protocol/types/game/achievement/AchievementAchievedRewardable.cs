using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class AchievementAchievedRewardable : AchievementAchieved
{

	public const uint Id = 515;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Finishedlevel { get; set; }

	public AchievementAchievedRewardable() {}


	public AchievementAchievedRewardable InitAchievementAchievedRewardable(short Finishedlevel)
	{
		this.Finishedlevel = Finishedlevel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.Finishedlevel);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Finishedlevel = reader.ReadVarShort();
	}
}
}
