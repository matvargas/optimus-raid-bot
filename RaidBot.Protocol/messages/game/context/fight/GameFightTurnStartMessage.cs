using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightTurnStartMessage : NetworkMessage
{

	public const uint Id = 714;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int WaitTime { get; set; }

	public GameFightTurnStartMessage() {}


	public GameFightTurnStartMessage InitGameFightTurnStartMessage(double Id_, int WaitTime)
	{
		this.Id_ = Id_;
		this.WaitTime = WaitTime;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.Id_);
		writer.WriteVarInt(this.WaitTime);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Id_ = reader.ReadDouble();
		this.WaitTime = reader.ReadVarInt();
	}
}
}
