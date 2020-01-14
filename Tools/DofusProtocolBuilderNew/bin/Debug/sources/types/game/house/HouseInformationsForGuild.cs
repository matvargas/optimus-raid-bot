using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HouseInformationsForGuild : HouseInformations
{

	public const uint Id = 170;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int InstanceId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool SecondHand { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String OwnerName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldX { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldY { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MapId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SubAreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] SkillListIds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int GuildshareParams { get; set; }

	public HouseInformationsForGuild() {}


	public HouseInformationsForGuild InitHouseInformationsForGuild(int InstanceId, bool SecondHand, String OwnerName, short WorldX, short WorldY, double MapId, short SubAreaId, int[] SkillListIds, int GuildshareParams)
	{
		this.InstanceId = InstanceId;
		this.SecondHand = SecondHand;
		this.OwnerName = OwnerName;
		this.WorldX = WorldX;
		this.WorldY = WorldY;
		this.MapId = MapId;
		this.SubAreaId = SubAreaId;
		this.SkillListIds = SkillListIds;
		this.GuildshareParams = GuildshareParams;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(this.InstanceId);
		writer.WriteBoolean(this.SecondHand);
		writer.WriteUTF(this.OwnerName);
		writer.WriteShort(this.WorldX);
		writer.WriteShort(this.WorldY);
		writer.WriteDouble(this.MapId);
		writer.WriteVarShort(this.SubAreaId);
		writer.WriteShort(this.SkillListIds.Length);
		foreach (int item in this.SkillListIds)
		{
			writer.WriteInt(item);
		}
		writer.WriteVarInt(this.GuildshareParams);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.InstanceId = reader.ReadInt();
		this.SecondHand = reader.ReadBoolean();
		this.OwnerName = reader.ReadUTF();
		this.WorldX = reader.ReadShort();
		this.WorldY = reader.ReadShort();
		this.MapId = reader.ReadDouble();
		this.SubAreaId = reader.ReadVarShort();
		int SkillListIdsLen = reader.ReadShort();
		SkillListIds = new int[SkillListIdsLen];
		for (int i = 0; i < SkillListIdsLen; i++)
		{
			this.SkillListIds[i] = reader.ReadInt();
		}
		this.GuildshareParams = reader.ReadVarInt();
	}
}
}
