using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AchievementDetailsRequestMessage : NetworkMessage
{

	public const uint Id = 6380;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short AchievementId { get; set; }

	public AchievementDetailsRequestMessage() {}


	public AchievementDetailsRequestMessage InitAchievementDetailsRequestMessage(short AchievementId)
	{
		this.AchievementId = AchievementId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.AchievementId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AchievementId = reader.ReadVarShort();
	}
}
}
