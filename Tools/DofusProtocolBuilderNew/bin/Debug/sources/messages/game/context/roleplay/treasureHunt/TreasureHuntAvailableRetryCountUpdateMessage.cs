using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TreasureHuntAvailableRetryCountUpdateMessage : NetworkMessage
{

	public const uint Id = 6491;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte QuestType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AvailableRetryCount { get; set; }

	public TreasureHuntAvailableRetryCountUpdateMessage() {}


	public TreasureHuntAvailableRetryCountUpdateMessage InitTreasureHuntAvailableRetryCountUpdateMessage(byte QuestType, int AvailableRetryCount)
	{
		this.QuestType = QuestType;
		this.AvailableRetryCount = AvailableRetryCount;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.QuestType);
		writer.WriteInt(this.AvailableRetryCount);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.QuestType = reader.ReadByte();
		this.AvailableRetryCount = reader.ReadInt();
	}
}
}
