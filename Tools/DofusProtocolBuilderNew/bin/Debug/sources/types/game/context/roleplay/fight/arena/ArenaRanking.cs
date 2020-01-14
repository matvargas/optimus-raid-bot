using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ArenaRanking : NetworkType
{

	public const uint Id = 554;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Rank { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short BestRank { get; set; }

	public ArenaRanking() {}


	public ArenaRanking InitArenaRanking(short Rank, short BestRank)
	{
		this.Rank = Rank;
		this.BestRank = BestRank;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.Rank);
		writer.WriteVarShort(this.BestRank);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Rank = reader.ReadVarShort();
		this.BestRank = reader.ReadVarShort();
	}
}
}
