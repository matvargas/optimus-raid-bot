using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HouseInformationsForSell : NetworkType
{

	public const uint Id = 221;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int InstanceId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool SecondHand { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ModelId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String OwnerName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool OwnerConnected { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldX { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldY { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SubAreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NbRoom { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NbChest { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] SkillListIds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsLocked { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Price { get; set; }

	public HouseInformationsForSell() {}


	public HouseInformationsForSell InitHouseInformationsForSell(int InstanceId, bool SecondHand, int ModelId, String OwnerName, bool OwnerConnected, short WorldX, short WorldY, short SubAreaId, byte NbRoom, byte NbChest, int[] SkillListIds, bool IsLocked, long Price)
	{
		this.InstanceId = InstanceId;
		this.SecondHand = SecondHand;
		this.ModelId = ModelId;
		this.OwnerName = OwnerName;
		this.OwnerConnected = OwnerConnected;
		this.WorldX = WorldX;
		this.WorldY = WorldY;
		this.SubAreaId = SubAreaId;
		this.NbRoom = NbRoom;
		this.NbChest = NbChest;
		this.SkillListIds = SkillListIds;
		this.IsLocked = IsLocked;
		this.Price = Price;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.InstanceId);
		writer.WriteBoolean(this.SecondHand);
		writer.WriteVarInt(this.ModelId);
		writer.WriteUTF(this.OwnerName);
		writer.WriteBoolean(this.OwnerConnected);
		writer.WriteShort(this.WorldX);
		writer.WriteShort(this.WorldY);
		writer.WriteVarShort(this.SubAreaId);
		writer.WriteByte(this.NbRoom);
		writer.WriteByte(this.NbChest);
		writer.WriteShort(this.SkillListIds.Length);
		foreach (int item in this.SkillListIds)
		{
			writer.WriteInt(item);
		}
		writer.WriteBoolean(this.IsLocked);
		writer.WriteVarLong(this.Price);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.InstanceId = reader.ReadInt();
		this.SecondHand = reader.ReadBoolean();
		this.ModelId = reader.ReadVarInt();
		this.OwnerName = reader.ReadUTF();
		this.OwnerConnected = reader.ReadBoolean();
		this.WorldX = reader.ReadShort();
		this.WorldY = reader.ReadShort();
		this.SubAreaId = reader.ReadVarShort();
		this.NbRoom = reader.ReadByte();
		this.NbChest = reader.ReadByte();
		int SkillListIdsLen = reader.ReadShort();
		SkillListIds = new int[SkillListIdsLen];
		for (int i = 0; i < SkillListIdsLen; i++)
		{
			this.SkillListIds[i] = reader.ReadInt();
		}
		this.IsLocked = reader.ReadBoolean();
		this.Price = reader.ReadVarLong();
	}
}
}
