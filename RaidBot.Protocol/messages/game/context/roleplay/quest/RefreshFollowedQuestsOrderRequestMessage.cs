using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class RefreshFollowedQuestsOrderRequestMessage : NetworkMessage
{

	public const uint Id = 6722;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Quests { get; set; }

	public RefreshFollowedQuestsOrderRequestMessage() {}


	public RefreshFollowedQuestsOrderRequestMessage InitRefreshFollowedQuestsOrderRequestMessage(short[] Quests)
	{
		this.Quests = Quests;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Quests.Length);
		foreach (short item in this.Quests)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int QuestsLen = reader.ReadShort();
		Quests = new short[QuestsLen];
		for (int i = 0; i < QuestsLen; i++)
		{
			this.Quests[i] = reader.ReadVarShort();
		}
	}
}
}
