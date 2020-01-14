using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameRolePlayGroupMonsterInformations : GameRolePlayActorInformations
{

	public const uint Id = 160;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool KeyRingBonus { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HasHardcoreDrop { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HasAVARewardToken { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GroupMonsterStaticInformations StaticInfos { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double CreationTime { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AgeBonusRate { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte LootShare { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AlignmentSide { get; set; }

	public GameRolePlayGroupMonsterInformations() {}


	public GameRolePlayGroupMonsterInformations InitGameRolePlayGroupMonsterInformations(bool KeyRingBonus, bool HasHardcoreDrop, bool HasAVARewardToken, GroupMonsterStaticInformations StaticInfos, double CreationTime, int AgeBonusRate, byte LootShare, byte AlignmentSide)
	{
		this.KeyRingBonus = KeyRingBonus;
		this.HasHardcoreDrop = HasHardcoreDrop;
		this.HasAVARewardToken = HasAVARewardToken;
		this.StaticInfos = StaticInfos;
		this.CreationTime = CreationTime;
		this.AgeBonusRate = AgeBonusRate;
		this.LootShare = LootShare;
		this.AlignmentSide = AlignmentSide;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, KeyRingBonus);
		box = BooleanByteWrapper.SetFlag(box, 1, HasHardcoreDrop);
		box = BooleanByteWrapper.SetFlag(box, 2, HasAVARewardToken);
		writer.WriteByte(box);
writer.WriteShort(StaticInfos.MessageId);
		StaticInfos.Serialize(writer);
		writer.WriteDouble(this.CreationTime);
		writer.WriteInt(this.AgeBonusRate);
		writer.WriteByte(this.LootShare);
		writer.WriteByte(this.AlignmentSide);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		byte box = reader.ReadByte();
		this.KeyRingBonus = BooleanByteWrapper.GetFlag(box, 0);
		this.HasHardcoreDrop = BooleanByteWrapper.GetFlag(box, 1);
		this.HasAVARewardToken = BooleanByteWrapper.GetFlag(box, 2);
this.StaticInfos = ProtocolTypeManager.GetInstance<GroupMonsterStaticInformations>(reader.ReadShort());
		this.StaticInfos.Deserialize(reader);
		this.CreationTime = reader.ReadDouble();
		this.AgeBonusRate = reader.ReadInt();
		this.LootShare = reader.ReadByte();
		this.AlignmentSide = reader.ReadByte();
	}
}
}
