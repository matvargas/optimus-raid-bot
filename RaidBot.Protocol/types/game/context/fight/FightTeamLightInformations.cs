using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightTeamLightInformations : AbstractFightTeamInformations
{

	public const uint Id = 115;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HasFriend { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HasGuildMember { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HasAllianceMember { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HasGroupMember { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool HasMyTaxCollector { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TeamMembersCount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MeanLevel { get; set; }

	public FightTeamLightInformations() {}


	public FightTeamLightInformations InitFightTeamLightInformations(bool HasFriend, bool HasGuildMember, bool HasAllianceMember, bool HasGroupMember, bool HasMyTaxCollector, byte TeamMembersCount, int MeanLevel)
	{
		this.HasFriend = HasFriend;
		this.HasGuildMember = HasGuildMember;
		this.HasAllianceMember = HasAllianceMember;
		this.HasGroupMember = HasGroupMember;
		this.HasMyTaxCollector = HasMyTaxCollector;
		this.TeamMembersCount = TeamMembersCount;
		this.MeanLevel = MeanLevel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, HasFriend);
		box = BooleanByteWrapper.SetFlag(box, 1, HasGuildMember);
		box = BooleanByteWrapper.SetFlag(box, 2, HasAllianceMember);
		box = BooleanByteWrapper.SetFlag(box, 3, HasGroupMember);
		box = BooleanByteWrapper.SetFlag(box, 4, HasMyTaxCollector);
		writer.WriteByte(box);
		writer.WriteByte(this.TeamMembersCount);
		writer.WriteVarInt(this.MeanLevel);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		byte box = reader.ReadByte();
		this.HasFriend = BooleanByteWrapper.GetFlag(box, 0);
		this.HasGuildMember = BooleanByteWrapper.GetFlag(box, 1);
		this.HasAllianceMember = BooleanByteWrapper.GetFlag(box, 2);
		this.HasGroupMember = BooleanByteWrapper.GetFlag(box, 3);
		this.HasMyTaxCollector = BooleanByteWrapper.GetFlag(box, 4);
		this.TeamMembersCount = reader.ReadByte();
		this.MeanLevel = reader.ReadVarInt();
	}
}
}
