using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightPlacementSwapPositionsAcceptMessage : NetworkMessage
{

	public const uint Id = 6547;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int RequestId { get; set; }

	public GameFightPlacementSwapPositionsAcceptMessage() {}


	public GameFightPlacementSwapPositionsAcceptMessage InitGameFightPlacementSwapPositionsAcceptMessage(int RequestId)
	{
		this.RequestId = RequestId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.RequestId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.RequestId = reader.ReadInt();
	}
}
}
