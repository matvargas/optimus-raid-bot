using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class RefreshCharacterStatsMessage : NetworkMessage
{

	public const uint Id = 6699;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double FighterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameFightMinimalStats Stats { get; set; }

	public RefreshCharacterStatsMessage() {}


	public RefreshCharacterStatsMessage InitRefreshCharacterStatsMessage(double FighterId, GameFightMinimalStats Stats)
	{
		this.FighterId = FighterId;
		this.Stats = Stats;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.FighterId);
		this.Stats.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FighterId = reader.ReadDouble();
		this.Stats = new GameFightMinimalStats();
		this.Stats.Deserialize(reader);
	}
}
}
