using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FriendOnlineInformations : FriendInformations
{

	public const uint Id = 92;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Sex { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HavenBagShared { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long PlayerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String PlayerName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Level { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AlignmentSide { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Breed { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildInformations GuildInfo { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MoodSmileyId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PlayerStatus Status { get; set; }

	public FriendOnlineInformations() {}


	public FriendOnlineInformations InitFriendOnlineInformations(bool Sex, bool HavenBagShared, long PlayerId, String PlayerName, short Level, byte AlignmentSide, byte Breed, GuildInformations GuildInfo, short MoodSmileyId, PlayerStatus Status)
	{
		this.Sex = Sex;
		this.HavenBagShared = HavenBagShared;
		this.PlayerId = PlayerId;
		this.PlayerName = PlayerName;
		this.Level = Level;
		this.AlignmentSide = AlignmentSide;
		this.Breed = Breed;
		this.GuildInfo = GuildInfo;
		this.MoodSmileyId = MoodSmileyId;
		this.Status = Status;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, Sex);
		box = BooleanByteWrapper.SetFlag(box, 1, HavenBagShared);
		writer.WriteByte(box);
		writer.WriteVarLong(this.PlayerId);
		writer.WriteUTF(this.PlayerName);
		writer.WriteVarShort(this.Level);
		writer.WriteByte(this.AlignmentSide);
		writer.WriteByte(this.Breed);
		this.GuildInfo.Serialize(writer);
		writer.WriteVarShort(this.MoodSmileyId);
writer.WriteShort(Status.MessageId);
		Status.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		byte box = reader.ReadByte();
		this.Sex = BooleanByteWrapper.GetFlag(box, 0);
		this.HavenBagShared = BooleanByteWrapper.GetFlag(box, 1);
		this.PlayerId = reader.ReadVarLong();
		this.PlayerName = reader.ReadUTF();
		this.Level = reader.ReadVarShort();
		this.AlignmentSide = reader.ReadByte();
		this.Breed = reader.ReadByte();
		this.GuildInfo = new GuildInformations();
		this.GuildInfo.Deserialize(reader);
		this.MoodSmileyId = reader.ReadVarShort();
this.Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
		this.Status.Deserialize(reader);
	}
}
}
