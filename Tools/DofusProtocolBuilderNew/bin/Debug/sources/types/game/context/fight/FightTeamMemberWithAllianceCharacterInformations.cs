using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightTeamMemberWithAllianceCharacterInformations : FightTeamMemberCharacterInformations
{

	public const uint Id = 426;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public BasicAllianceInformations AllianceInfos { get; set; }

	public FightTeamMemberWithAllianceCharacterInformations() {}


	public FightTeamMemberWithAllianceCharacterInformations InitFightTeamMemberWithAllianceCharacterInformations(BasicAllianceInformations AllianceInfos)
	{
		this.AllianceInfos = AllianceInfos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.AllianceInfos.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.AllianceInfos = new BasicAllianceInformations();
		this.AllianceInfos.Deserialize(reader);
	}
}
}
