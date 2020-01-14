using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DareRewardsListMessage : NetworkMessage
{

	public const uint Id = 6677;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DareReward[] Rewards { get; set; }

	public DareRewardsListMessage() {}


	public DareRewardsListMessage InitDareRewardsListMessage(DareReward[] Rewards)
	{
		this.Rewards = Rewards;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Rewards.Length);
		foreach (DareReward item in this.Rewards)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int RewardsLen = reader.ReadShort();
		Rewards = new DareReward[RewardsLen];
		for (int i = 0; i < RewardsLen; i++)
		{
			this.Rewards[i] = new DareReward();
			this.Rewards[i].Deserialize(reader);
		}
	}
}
}
