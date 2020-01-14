using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayArenaInvitationCandidatesAnswer : NetworkMessage
{

	public const uint Id = 6783;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public LeagueFriendInformations[] Candidates { get; set; }

	public GameRolePlayArenaInvitationCandidatesAnswer() {}


	public GameRolePlayArenaInvitationCandidatesAnswer InitGameRolePlayArenaInvitationCandidatesAnswer(LeagueFriendInformations[] Candidates)
	{
		this.Candidates = Candidates;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Candidates.Length);
		foreach (LeagueFriendInformations item in this.Candidates)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int CandidatesLen = reader.ReadShort();
		Candidates = new LeagueFriendInformations[CandidatesLen];
		for (int i = 0; i < CandidatesLen; i++)
		{
			this.Candidates[i] = new LeagueFriendInformations();
			this.Candidates[i].Deserialize(reader);
		}
	}
}
}
