using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PrismFightDefenderLeaveMessage : NetworkMessage
{

	public const uint Id = 5892;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SubAreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long FighterToRemoveId { get; set; }

	public PrismFightDefenderLeaveMessage() {}


	public PrismFightDefenderLeaveMessage InitPrismFightDefenderLeaveMessage(short SubAreaId, short FightId, long FighterToRemoveId)
	{
		this.SubAreaId = SubAreaId;
		this.FightId = FightId;
		this.FighterToRemoveId = FighterToRemoveId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SubAreaId);
		writer.WriteVarShort(this.FightId);
		writer.WriteVarLong(this.FighterToRemoveId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SubAreaId = reader.ReadVarShort();
		this.FightId = reader.ReadVarShort();
		this.FighterToRemoveId = reader.ReadVarLong();
	}
}
}
