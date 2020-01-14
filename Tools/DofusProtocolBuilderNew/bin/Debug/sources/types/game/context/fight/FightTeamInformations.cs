using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightTeamInformations : AbstractFightTeamInformations
{

	public const uint Id = 33;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightTeamMemberInformations[] TeamMembers { get; set; }

	public FightTeamInformations() {}


	public FightTeamInformations InitFightTeamInformations(FightTeamMemberInformations[] TeamMembers)
	{
		this.TeamMembers = TeamMembers;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.TeamMembers.Length);
		foreach (FightTeamMemberInformations item in this.TeamMembers)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int TeamMembersLen = reader.ReadShort();
		TeamMembers = new FightTeamMemberInformations[TeamMembersLen];
		for (int i = 0; i < TeamMembersLen; i++)
		{
			this.TeamMembers[i] = ProtocolTypeManager.GetInstance<FightTeamMemberInformations>(reader.ReadShort());
			this.TeamMembers[i].Deserialize(reader);
		}
	}
}
}
