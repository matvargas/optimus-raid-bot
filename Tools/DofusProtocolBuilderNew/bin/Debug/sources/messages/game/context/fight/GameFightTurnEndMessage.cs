using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightTurnEndMessage : NetworkMessage
{

	public const uint Id = 719;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double Id_ { get; set; }

	public GameFightTurnEndMessage() {}


	public GameFightTurnEndMessage InitGameFightTurnEndMessage(double Id_)
	{
		this.Id_ = Id_;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.Id_);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Id_ = reader.ReadDouble();
	}
}
}
