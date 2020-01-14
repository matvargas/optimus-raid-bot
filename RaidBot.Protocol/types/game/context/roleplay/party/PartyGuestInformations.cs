using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PartyGuestInformations : NetworkType
{

	public const uint Id = 374;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long GuestId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long HostId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Name { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityLook GuestLook { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Breed { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Sex { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PlayerStatus Status { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PartyEntityBaseInformation[] Entities { get; set; }

	public PartyGuestInformations() {}


	public PartyGuestInformations InitPartyGuestInformations(long GuestId, long HostId, String Name, EntityLook GuestLook, byte Breed, bool Sex, PlayerStatus Status, PartyEntityBaseInformation[] Entities)
	{
		this.GuestId = GuestId;
		this.HostId = HostId;
		this.Name = Name;
		this.GuestLook = GuestLook;
		this.Breed = Breed;
		this.Sex = Sex;
		this.Status = Status;
		this.Entities = Entities;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.GuestId);
		writer.WriteVarLong(this.HostId);
		writer.WriteUTF(this.Name);
		this.GuestLook.Serialize(writer);
		writer.WriteByte(this.Breed);
		writer.WriteBoolean(this.Sex);
writer.WriteShort(Status.MessageId);
		Status.Serialize(writer);
		writer.WriteShort(this.Entities.Length);
		foreach (PartyEntityBaseInformation item in this.Entities)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.GuestId = reader.ReadVarLong();
		this.HostId = reader.ReadVarLong();
		this.Name = reader.ReadUTF();
		this.GuestLook = new EntityLook();
		this.GuestLook.Deserialize(reader);
		this.Breed = reader.ReadByte();
		this.Sex = reader.ReadBoolean();
this.Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
		this.Status.Deserialize(reader);
		int EntitiesLen = reader.ReadShort();
		Entities = new PartyEntityBaseInformation[EntitiesLen];
		for (int i = 0; i < EntitiesLen; i++)
		{
			this.Entities[i] = new PartyEntityBaseInformation();
			this.Entities[i].Deserialize(reader);
		}
	}
}
}
