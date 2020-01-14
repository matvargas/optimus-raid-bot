using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AchievementDetailedListRequestMessage : NetworkMessage
{

	public const uint Id = 6357;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CategoryId { get; set; }

	public AchievementDetailedListRequestMessage() {}


	public AchievementDetailedListRequestMessage InitAchievementDetailedListRequestMessage(short CategoryId)
	{
		this.CategoryId = CategoryId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.CategoryId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CategoryId = reader.ReadVarShort();
	}
}
}
