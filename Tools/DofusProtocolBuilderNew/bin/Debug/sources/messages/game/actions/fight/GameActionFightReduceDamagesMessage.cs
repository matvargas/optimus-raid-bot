using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightReduceDamagesMessage : AbstractGameActionMessage
{

	public const uint Id = 5526;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double TargetId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Amount { get; set; }

	public GameActionFightReduceDamagesMessage() {}


	public GameActionFightReduceDamagesMessage InitGameActionFightReduceDamagesMessage(double TargetId, int Amount)
	{
		this.TargetId = TargetId;
		this.Amount = Amount;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.TargetId);
		writer.WriteVarInt(this.Amount);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.TargetId = reader.ReadDouble();
		this.Amount = reader.ReadVarInt();
	}
}
}
