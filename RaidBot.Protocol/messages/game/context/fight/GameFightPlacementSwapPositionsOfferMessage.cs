using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightPlacementSwapPositionsOfferMessage : NetworkMessage
{

	public const uint Id = 6542;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int RequestId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double RequesterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short RequesterCellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double RequestedId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short RequestedCellId { get; set; }

	public GameFightPlacementSwapPositionsOfferMessage() {}


	public GameFightPlacementSwapPositionsOfferMessage InitGameFightPlacementSwapPositionsOfferMessage(int RequestId, double RequesterId, short RequesterCellId, double RequestedId, short RequestedCellId)
	{
		this.RequestId = RequestId;
		this.RequesterId = RequesterId;
		this.RequesterCellId = RequesterCellId;
		this.RequestedId = RequestedId;
		this.RequestedCellId = RequestedCellId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.RequestId);
		writer.WriteDouble(this.RequesterId);
		writer.WriteVarShort(this.RequesterCellId);
		writer.WriteDouble(this.RequestedId);
		writer.WriteVarShort(this.RequestedCellId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.RequestId = reader.ReadInt();
		this.RequesterId = reader.ReadDouble();
		this.RequesterCellId = reader.ReadVarShort();
		this.RequestedId = reader.ReadDouble();
		this.RequestedCellId = reader.ReadVarShort();
	}
}
}
