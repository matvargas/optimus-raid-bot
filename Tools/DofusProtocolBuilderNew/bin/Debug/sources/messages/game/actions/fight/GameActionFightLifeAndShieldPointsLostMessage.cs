using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightLifeAndShieldPointsLostMessage : GameActionFightLifePointsLostMessage
{

	public const uint Id = 6310;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ShieldLoss { get; set; }

	public GameActionFightLifeAndShieldPointsLostMessage() {}


	public GameActionFightLifeAndShieldPointsLostMessage InitGameActionFightLifeAndShieldPointsLostMessage(short ShieldLoss)
	{
		this.ShieldLoss = ShieldLoss;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.ShieldLoss);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.ShieldLoss = reader.ReadVarShort();
	}
}
}
