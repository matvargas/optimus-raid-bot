using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightExchangePositionsMessage : AbstractGameActionMessage
{

	public const uint Id = 5527;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double TargetId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CasterCellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short TargetCellId { get; set; }

	public GameActionFightExchangePositionsMessage() {}


	public GameActionFightExchangePositionsMessage InitGameActionFightExchangePositionsMessage(double TargetId, short CasterCellId, short TargetCellId)
	{
		this.TargetId = TargetId;
		this.CasterCellId = CasterCellId;
		this.TargetCellId = TargetCellId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.TargetId);
		writer.WriteShort(this.CasterCellId);
		writer.WriteShort(this.TargetCellId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.TargetId = reader.ReadDouble();
		this.CasterCellId = reader.ReadShort();
		this.TargetCellId = reader.ReadShort();
	}
}
}
