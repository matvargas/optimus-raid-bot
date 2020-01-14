using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightPlacementSwapPositionsCancelledMessage : NetworkMessage
{

	public const uint Id = 6546;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int RequestId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double CancellerId { get; set; }

	public GameFightPlacementSwapPositionsCancelledMessage() {}


	public GameFightPlacementSwapPositionsCancelledMessage InitGameFightPlacementSwapPositionsCancelledMessage(int RequestId, double CancellerId)
	{
		this.RequestId = RequestId;
		this.CancellerId = CancellerId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.RequestId);
		writer.WriteDouble(this.CancellerId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.RequestId = reader.ReadInt();
		this.CancellerId = reader.ReadDouble();
	}
}
}
