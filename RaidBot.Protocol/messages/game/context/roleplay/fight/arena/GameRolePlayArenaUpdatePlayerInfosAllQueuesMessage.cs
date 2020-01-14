using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayArenaUpdatePlayerInfosAllQueuesMessage : GameRolePlayArenaUpdatePlayerInfosMessage
{

	public const uint Id = 6728;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ArenaRankInfos Team { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ArenaRankInfos Duel { get; set; }

	public GameRolePlayArenaUpdatePlayerInfosAllQueuesMessage() {}


	public GameRolePlayArenaUpdatePlayerInfosAllQueuesMessage InitGameRolePlayArenaUpdatePlayerInfosAllQueuesMessage(ArenaRankInfos Team, ArenaRankInfos Duel)
	{
		this.Team = Team;
		this.Duel = Duel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.Team.Serialize(writer);
		this.Duel.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Team = new ArenaRankInfos();
		this.Team.Deserialize(reader);
		this.Duel = new ArenaRankInfos();
		this.Duel.Deserialize(reader);
	}
}
}
