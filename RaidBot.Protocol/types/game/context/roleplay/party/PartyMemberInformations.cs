using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PartyMemberInformations : CharacterBaseInformations
{

	public const uint Id = 90;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LifePoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MaxLifePoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Prospecting { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte RegenRate { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Initiative { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AlignmentSide { get; set; }
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
	public PlayerStatus Status { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PartyEntityBaseInformation[] Entities { get; set; }

	public PartyMemberInformations() {}


	public PartyMemberInformations InitPartyMemberInformations(int LifePoints, int MaxLifePoints, short Prospecting, byte RegenRate, short Initiative, byte AlignmentSide, short WorldX, short WorldY, double MapId, short SubAreaId, PlayerStatus Status, PartyEntityBaseInformation[] Entities)
	{
		this.LifePoints = LifePoints;
		this.MaxLifePoints = MaxLifePoints;
		this.Prospecting = Prospecting;
		this.RegenRate = RegenRate;
		this.Initiative = Initiative;
		this.AlignmentSide = AlignmentSide;
		this.WorldX = WorldX;
		this.WorldY = WorldY;
		this.MapId = MapId;
		this.SubAreaId = SubAreaId;
		this.Status = Status;
		this.Entities = Entities;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.LifePoints);
		writer.WriteVarInt(this.MaxLifePoints);
		writer.WriteVarShort(this.Prospecting);
		writer.WriteByte(this.RegenRate);
		writer.WriteVarShort(this.Initiative);
		writer.WriteByte(this.AlignmentSide);
		writer.WriteShort(this.WorldX);
		writer.WriteShort(this.WorldY);
		writer.WriteDouble(this.MapId);
		writer.WriteVarShort(this.SubAreaId);
writer.WriteShort(Status.MessageId);
		Status.Serialize(writer);
		writer.WriteShort(this.Entities.Length);
		foreach (PartyEntityBaseInformation item in this.Entities)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.LifePoints = reader.ReadVarInt();
		this.MaxLifePoints = reader.ReadVarInt();
		this.Prospecting = reader.ReadVarShort();
		this.RegenRate = reader.ReadByte();
		this.Initiative = reader.ReadVarShort();
		this.AlignmentSide = reader.ReadByte();
		this.WorldX = reader.ReadShort();
		this.WorldY = reader.ReadShort();
		this.MapId = reader.ReadDouble();
		this.SubAreaId = reader.ReadVarShort();
this.Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
		this.Status.Deserialize(reader);
		int EntitiesLen = reader.ReadShort();
		Entities = new PartyEntityBaseInformation[EntitiesLen];
		for (int i = 0; i < EntitiesLen; i++)
		{
			this.Entities[i] = ProtocolTypeManager.GetInstance<PartyEntityBaseInformation>(reader.ReadShort());
			this.Entities[i].Deserialize(reader);
		}
	}
}
}
