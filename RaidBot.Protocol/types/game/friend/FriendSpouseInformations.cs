using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FriendSpouseInformations : NetworkType
{

	public const uint Id = 77;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SpouseAccountId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long SpouseId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String SpouseName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SpouseLevel { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Breed { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Sex { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityLook SpouseEntityLook { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildInformations GuildInfo { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AlignmentSide { get; set; }

	public FriendSpouseInformations() {}


	public FriendSpouseInformations InitFriendSpouseInformations(int SpouseAccountId, long SpouseId, String SpouseName, short SpouseLevel, byte Breed, byte Sex, EntityLook SpouseEntityLook, GuildInformations GuildInfo, byte AlignmentSide)
	{
		this.SpouseAccountId = SpouseAccountId;
		this.SpouseId = SpouseId;
		this.SpouseName = SpouseName;
		this.SpouseLevel = SpouseLevel;
		this.Breed = Breed;
		this.Sex = Sex;
		this.SpouseEntityLook = SpouseEntityLook;
		this.GuildInfo = GuildInfo;
		this.AlignmentSide = AlignmentSide;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.SpouseAccountId);
		writer.WriteVarLong(this.SpouseId);
		writer.WriteUTF(this.SpouseName);
		writer.WriteVarShort(this.SpouseLevel);
		writer.WriteByte(this.Breed);
		writer.WriteByte(this.Sex);
		this.SpouseEntityLook.Serialize(writer);
		this.GuildInfo.Serialize(writer);
		writer.WriteByte(this.AlignmentSide);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SpouseAccountId = reader.ReadInt();
		this.SpouseId = reader.ReadVarLong();
		this.SpouseName = reader.ReadUTF();
		this.SpouseLevel = reader.ReadVarShort();
		this.Breed = reader.ReadByte();
		this.Sex = reader.ReadByte();
		this.SpouseEntityLook = new EntityLook();
		this.SpouseEntityLook.Deserialize(reader);
		this.GuildInfo = new GuildInformations();
		this.GuildInfo.Deserialize(reader);
		this.AlignmentSide = reader.ReadByte();
	}
}
}
