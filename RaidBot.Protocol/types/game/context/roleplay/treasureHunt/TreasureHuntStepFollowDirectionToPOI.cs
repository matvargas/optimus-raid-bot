using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class TreasureHuntStepFollowDirectionToPOI : TreasureHuntStep
{

	public const uint Id = 461;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Direction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PoiLabelId { get; set; }

	public TreasureHuntStepFollowDirectionToPOI() {}


	public TreasureHuntStepFollowDirectionToPOI InitTreasureHuntStepFollowDirectionToPOI(byte Direction, short PoiLabelId)
	{
		this.Direction = Direction;
		this.PoiLabelId = PoiLabelId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.Direction);
		writer.WriteVarShort(this.PoiLabelId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Direction = reader.ReadByte();
		this.PoiLabelId = reader.ReadVarShort();
	}
}
}
