using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightNewRoundMessage : NetworkMessage
{

	public const uint Id = 6239;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int RoundNumber { get; set; }

	public GameFightNewRoundMessage() {}


	public GameFightNewRoundMessage InitGameFightNewRoundMessage(int RoundNumber)
	{
		this.RoundNumber = RoundNumber;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.RoundNumber);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.RoundNumber = reader.ReadVarInt();
	}
}
}
