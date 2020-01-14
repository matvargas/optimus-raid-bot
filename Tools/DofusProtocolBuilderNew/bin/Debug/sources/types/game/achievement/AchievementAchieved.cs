using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class AchievementAchieved : NetworkType
{

	public const uint Id = 514;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long AchievedBy { get; set; }

	public AchievementAchieved() {}


	public AchievementAchieved InitAchievementAchieved(short Id_, long AchievedBy)
	{
		this.Id_ = Id_;
		this.AchievedBy = AchievedBy;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.Id_);
		writer.WriteVarLong(this.AchievedBy);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Id_ = reader.ReadVarShort();
		this.AchievedBy = reader.ReadVarLong();
	}
}
}
