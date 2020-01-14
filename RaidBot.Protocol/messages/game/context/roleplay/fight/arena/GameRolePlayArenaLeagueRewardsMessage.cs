using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayArenaLeagueRewardsMessage : NetworkMessage
{

	public const uint Id = 6785;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SeasonId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short LeagueId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LadderPosition { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool EndSeasonReward { get; set; }

	public GameRolePlayArenaLeagueRewardsMessage() {}


	public GameRolePlayArenaLeagueRewardsMessage InitGameRolePlayArenaLeagueRewardsMessage(short SeasonId, short LeagueId, int LadderPosition, bool EndSeasonReward)
	{
		this.SeasonId = SeasonId;
		this.LeagueId = LeagueId;
		this.LadderPosition = LadderPosition;
		this.EndSeasonReward = EndSeasonReward;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SeasonId);
		writer.WriteVarShort(this.LeagueId);
		writer.WriteInt(this.LadderPosition);
		writer.WriteBoolean(this.EndSeasonReward);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SeasonId = reader.ReadVarShort();
		this.LeagueId = reader.ReadVarShort();
		this.LadderPosition = reader.ReadInt();
		this.EndSeasonReward = reader.ReadBoolean();
	}
}
}
