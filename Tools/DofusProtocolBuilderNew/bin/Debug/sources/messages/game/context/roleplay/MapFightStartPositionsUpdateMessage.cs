using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MapFightStartPositionsUpdateMessage : NetworkMessage
{

	public const uint Id = 6716;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MapId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightStartingPositions FightStartPositions { get; set; }

	public MapFightStartPositionsUpdateMessage() {}


	public MapFightStartPositionsUpdateMessage InitMapFightStartPositionsUpdateMessage(double MapId, FightStartingPositions FightStartPositions)
	{
		this.MapId = MapId;
		this.FightStartPositions = FightStartPositions;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.MapId);
		this.FightStartPositions.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MapId = reader.ReadDouble();
		this.FightStartPositions = new FightStartingPositions();
		this.FightStartPositions.Deserialize(reader);
	}
}
}
