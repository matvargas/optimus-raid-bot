using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GuildMember : CharacterMinimalInformations
{

	public const uint Id = 88;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Sex { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HavenBagShared { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Breed { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Rank { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long GivenExperience { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte ExperienceGivenPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Rights { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Connected { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AlignmentSide { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short HoursSinceLastConnection { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MoodSmileyId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AccountId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AchievementPoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PlayerStatus Status { get; set; }

	public GuildMember() {}


	public GuildMember InitGuildMember(bool Sex, bool HavenBagShared, byte Breed, short Rank, long GivenExperience, byte ExperienceGivenPercent, int Rights, byte Connected, byte AlignmentSide, short HoursSinceLastConnection, short MoodSmileyId, int AccountId, int AchievementPoints, PlayerStatus Status)
	{
		this.Sex = Sex;
		this.HavenBagShared = HavenBagShared;
		this.Breed = Breed;
		this.Rank = Rank;
		this.GivenExperience = GivenExperience;
		this.ExperienceGivenPercent = ExperienceGivenPercent;
		this.Rights = Rights;
		this.Connected = Connected;
		this.AlignmentSide = AlignmentSide;
		this.HoursSinceLastConnection = HoursSinceLastConnection;
		this.MoodSmileyId = MoodSmileyId;
		this.AccountId = AccountId;
		this.AchievementPoints = AchievementPoints;
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
		writer.WriteByte(this.Breed);
		writer.WriteVarShort(this.Rank);
		writer.WriteVarLong(this.GivenExperience);
		writer.WriteByte(this.ExperienceGivenPercent);
		writer.WriteVarInt(this.Rights);
		writer.WriteByte(this.Connected);
		writer.WriteByte(this.AlignmentSide);
		writer.WriteShort(this.HoursSinceLastConnection);
		writer.WriteVarShort(this.MoodSmileyId);
		writer.WriteInt(this.AccountId);
		writer.WriteInt(this.AchievementPoints);
writer.WriteShort(Status.MessageId);
		Status.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		byte box = reader.ReadByte();
		this.Sex = BooleanByteWrapper.GetFlag(box, 0);
		this.HavenBagShared = BooleanByteWrapper.GetFlag(box, 1);
		this.Breed = reader.ReadByte();
		this.Rank = reader.ReadVarShort();
		this.GivenExperience = reader.ReadVarLong();
		this.ExperienceGivenPercent = reader.ReadByte();
		this.Rights = reader.ReadVarInt();
		this.Connected = reader.ReadByte();
		this.AlignmentSide = reader.ReadByte();
		this.HoursSinceLastConnection = reader.ReadShort();
		this.MoodSmileyId = reader.ReadVarShort();
		this.AccountId = reader.ReadInt();
		this.AchievementPoints = reader.ReadInt();
this.Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
		this.Status.Deserialize(reader);
	}
}
}
