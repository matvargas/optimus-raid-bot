using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class InventoryWeightMessage : NetworkMessage
{

	public const uint Id = 3009;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Weight { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int WeightMax { get; set; }

	public InventoryWeightMessage() {}


	public InventoryWeightMessage InitInventoryWeightMessage(int Weight, int WeightMax)
	{
		this.Weight = Weight;
		this.WeightMax = WeightMax;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.Weight);
		writer.WriteVarInt(this.WeightMax);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Weight = reader.ReadVarInt();
		this.WeightMax = reader.ReadVarInt();
	}
}
}
