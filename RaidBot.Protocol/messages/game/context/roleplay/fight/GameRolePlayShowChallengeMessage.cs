using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayShowChallengeMessage : NetworkMessage
{

	public const uint Id = 301;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightCommonInformations CommonsInfos { get; set; }

	public GameRolePlayShowChallengeMessage() {}


	public GameRolePlayShowChallengeMessage InitGameRolePlayShowChallengeMessage(FightCommonInformations CommonsInfos)
	{
		this.CommonsInfos = CommonsInfos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.CommonsInfos.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CommonsInfos = new FightCommonInformations();
		this.CommonsInfos.Deserialize(reader);
	}
}
}
