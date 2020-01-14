using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ArenaRankInfos : NetworkType
{

	public const uint Id = 499;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ArenaRanking Ranking { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ArenaLeagueRanking LeagueRanking { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short VictoryCount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Fightcount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NumFightNeededForLadder { get; set; }

	public ArenaRankInfos() {}


	public ArenaRankInfos InitArenaRankInfos(ArenaRanking Ranking, ArenaLeagueRanking LeagueRanking, short VictoryCount, short Fightcount, short NumFightNeededForLadder)
	{
		this.Ranking = Ranking;
		this.LeagueRanking = LeagueRanking;
		this.VictoryCount = VictoryCount;
		this.Fightcount = Fightcount;
		this.NumFightNeededForLadder = NumFightNeededForLadder;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Ranking.Serialize(writer);
		this.LeagueRanking.Serialize(writer);
		writer.WriteVarShort(this.VictoryCount);
		writer.WriteVarShort(this.Fightcount);
		writer.WriteShort(this.NumFightNeededForLadder);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Ranking = new ArenaRanking();
		this.Ranking.Deserialize(reader);
		this.LeagueRanking = new ArenaLeagueRanking();
		this.LeagueRanking.Deserialize(reader);
		this.VictoryCount = reader.ReadVarShort();
		this.Fightcount = reader.ReadVarShort();
		this.NumFightNeededForLadder = reader.ReadShort();
	}
}
}
