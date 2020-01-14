using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PaddockToSellFilterMessage : NetworkMessage
{

	public const uint Id = 6161;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AtLeastNbMount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AtLeastNbMachine { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long MaxPrice { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte OrderBy { get; set; }

	public PaddockToSellFilterMessage() {}


	public PaddockToSellFilterMessage InitPaddockToSellFilterMessage(int AreaId, byte AtLeastNbMount, byte AtLeastNbMachine, long MaxPrice, byte OrderBy)
	{
		this.AreaId = AreaId;
		this.AtLeastNbMount = AtLeastNbMount;
		this.AtLeastNbMachine = AtLeastNbMachine;
		this.MaxPrice = MaxPrice;
		this.OrderBy = OrderBy;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.AreaId);
		writer.WriteByte(this.AtLeastNbMount);
		writer.WriteByte(this.AtLeastNbMachine);
		writer.WriteVarLong(this.MaxPrice);
		writer.WriteByte(this.OrderBy);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AreaId = reader.ReadInt();
		this.AtLeastNbMount = reader.ReadByte();
		this.AtLeastNbMachine = reader.ReadByte();
		this.MaxPrice = reader.ReadVarLong();
		this.OrderBy = reader.ReadByte();
	}
}
}
