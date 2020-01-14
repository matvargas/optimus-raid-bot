using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightTurnResumeMessage : GameFightTurnStartMessage
{

	public const uint Id = 6307;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int RemainingTime { get; set; }

	public GameFightTurnResumeMessage() {}


	public GameFightTurnResumeMessage InitGameFightTurnResumeMessage(int RemainingTime)
	{
		this.RemainingTime = RemainingTime;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.RemainingTime);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.RemainingTime = reader.ReadVarInt();
	}
}
}
