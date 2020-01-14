using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class TreasureHuntStepFollowDirection : TreasureHuntStep
{

	public const uint Id = 468;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Direction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MapCount { get; set; }

	public TreasureHuntStepFollowDirection() {}


	public TreasureHuntStepFollowDirection InitTreasureHuntStepFollowDirection(byte Direction, short MapCount)
	{
		this.Direction = Direction;
		this.MapCount = MapCount;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.Direction);
		writer.WriteVarShort(this.MapCount);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Direction = reader.ReadByte();
		this.MapCount = reader.ReadVarShort();
	}
}
}
