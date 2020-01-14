using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HouseToSellFilterMessage : NetworkMessage
{

	public const uint Id = 6137;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AtLeastNbRoom { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AtLeastNbChest { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SkillRequested { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long MaxPrice { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte OrderBy { get; set; }

	public HouseToSellFilterMessage() {}


	public HouseToSellFilterMessage InitHouseToSellFilterMessage(int AreaId, byte AtLeastNbRoom, byte AtLeastNbChest, short SkillRequested, long MaxPrice, byte OrderBy)
	{
		this.AreaId = AreaId;
		this.AtLeastNbRoom = AtLeastNbRoom;
		this.AtLeastNbChest = AtLeastNbChest;
		this.SkillRequested = SkillRequested;
		this.MaxPrice = MaxPrice;
		this.OrderBy = OrderBy;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.AreaId);
		writer.WriteByte(this.AtLeastNbRoom);
		writer.WriteByte(this.AtLeastNbChest);
		writer.WriteVarShort(this.SkillRequested);
		writer.WriteVarLong(this.MaxPrice);
		writer.WriteByte(this.OrderBy);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AreaId = reader.ReadInt();
		this.AtLeastNbRoom = reader.ReadByte();
		this.AtLeastNbChest = reader.ReadByte();
		this.SkillRequested = reader.ReadVarShort();
		this.MaxPrice = reader.ReadVarLong();
		this.OrderBy = reader.ReadByte();
	}
}
}
