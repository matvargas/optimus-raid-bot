using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TreasureHuntDigRequestMessage : NetworkMessage
{

	public const uint Id = 6485;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte QuestType { get; set; }

	public TreasureHuntDigRequestMessage() {}


	public TreasureHuntDigRequestMessage InitTreasureHuntDigRequestMessage(byte QuestType)
	{
		this.QuestType = QuestType;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.QuestType);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.QuestType = reader.ReadByte();
	}
}
}
