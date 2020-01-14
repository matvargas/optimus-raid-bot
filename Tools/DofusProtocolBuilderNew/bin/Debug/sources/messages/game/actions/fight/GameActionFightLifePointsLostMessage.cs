using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightLifePointsLostMessage : AbstractGameActionMessage
{

	public const uint Id = 6312;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double TargetId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Loss { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int PermanentDamages { get; set; }

	public GameActionFightLifePointsLostMessage() {}


	public GameActionFightLifePointsLostMessage InitGameActionFightLifePointsLostMessage(double TargetId, int Loss, int PermanentDamages)
	{
		this.TargetId = TargetId;
		this.Loss = Loss;
		this.PermanentDamages = PermanentDamages;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.TargetId);
		writer.WriteVarInt(this.Loss);
		writer.WriteVarInt(this.PermanentDamages);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.TargetId = reader.ReadDouble();
		this.Loss = reader.ReadVarInt();
		this.PermanentDamages = reader.ReadVarInt();
	}
}
}
