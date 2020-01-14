using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DareRewardWonMessage : NetworkMessage
{

	public const uint Id = 6678;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DareReward Reward { get; set; }

	public DareRewardWonMessage() {}


	public DareRewardWonMessage InitDareRewardWonMessage(DareReward Reward)
	{
		this.Reward = Reward;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Reward.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Reward = new DareReward();
		this.Reward.Deserialize(reader);
	}
}
}
