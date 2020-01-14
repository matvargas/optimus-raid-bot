using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ArenaLeagueRanking : NetworkType
{

	public const uint Id = 553;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Rank { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short LeagueId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short LeaguePoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short TotalLeaguePoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LadderPosition { get; set; }

	public ArenaLeagueRanking() {}


	public ArenaLeagueRanking InitArenaLeagueRanking(short Rank, short LeagueId, short LeaguePoints, short TotalLeaguePoints, int LadderPosition)
	{
		this.Rank = Rank;
		this.LeagueId = LeagueId;
		this.LeaguePoints = LeaguePoints;
		this.TotalLeaguePoints = TotalLeaguePoints;
		this.LadderPosition = LadderPosition;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.Rank);
		writer.WriteVarShort(this.LeagueId);
		writer.WriteVarShort(this.LeaguePoints);
		writer.WriteVarShort(this.TotalLeaguePoints);
		writer.WriteInt(this.LadderPosition);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Rank = reader.ReadVarShort();
		this.LeagueId = reader.ReadVarShort();
		this.LeaguePoints = reader.ReadVarShort();
		this.TotalLeaguePoints = reader.ReadVarShort();
		this.LadderPosition = reader.ReadInt();
	}
}
}
