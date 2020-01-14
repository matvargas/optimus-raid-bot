using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightSpectatorJoinMessage : GameFightJoinMessage
{

	public const uint Id = 6504;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public NamedPartyTeam[] NamedPartyTeams { get; set; }

	public GameFightSpectatorJoinMessage() {}


	public GameFightSpectatorJoinMessage InitGameFightSpectatorJoinMessage(NamedPartyTeam[] NamedPartyTeams)
	{
		this.NamedPartyTeams = NamedPartyTeams;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.NamedPartyTeams.Length);
		foreach (NamedPartyTeam item in this.NamedPartyTeams)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int NamedPartyTeamsLen = reader.ReadShort();
		NamedPartyTeams = new NamedPartyTeam[NamedPartyTeamsLen];
		for (int i = 0; i < NamedPartyTeamsLen; i++)
		{
			this.NamedPartyTeams[i] = new NamedPartyTeam();
			this.NamedPartyTeams[i].Deserialize(reader);
		}
	}
}
}
