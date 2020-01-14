using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightEndMessage : NetworkMessage
{

	public const uint Id = 720;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Duration { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short AgeBonus { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short LootShareLimitMalus { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightResultListEntry[] Results { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public NamedPartyTeamWithOutcome[] NamedPartyTeamsOutcomes { get; set; }

	public GameFightEndMessage() {}


	public GameFightEndMessage InitGameFightEndMessage(int Duration, short AgeBonus, short LootShareLimitMalus, FightResultListEntry[] Results, NamedPartyTeamWithOutcome[] NamedPartyTeamsOutcomes)
	{
		this.Duration = Duration;
		this.AgeBonus = AgeBonus;
		this.LootShareLimitMalus = LootShareLimitMalus;
		this.Results = Results;
		this.NamedPartyTeamsOutcomes = NamedPartyTeamsOutcomes;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.Duration);
		writer.WriteShort(this.AgeBonus);
		writer.WriteShort(this.LootShareLimitMalus);
		writer.WriteShort(this.Results.Length);
		foreach (FightResultListEntry item in this.Results)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteShort(this.NamedPartyTeamsOutcomes.Length);
		foreach (NamedPartyTeamWithOutcome item in this.NamedPartyTeamsOutcomes)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Duration = reader.ReadInt();
		this.AgeBonus = reader.ReadShort();
		this.LootShareLimitMalus = reader.ReadShort();
		int ResultsLen = reader.ReadShort();
		Results = new FightResultListEntry[ResultsLen];
		for (int i = 0; i < ResultsLen; i++)
		{
			this.Results[i] = ProtocolTypeManager.GetInstance<FightResultListEntry>(reader.ReadShort());
			this.Results[i].Deserialize(reader);
		}
		int NamedPartyTeamsOutcomesLen = reader.ReadShort();
		NamedPartyTeamsOutcomes = new NamedPartyTeamWithOutcome[NamedPartyTeamsOutcomesLen];
		for (int i = 0; i < NamedPartyTeamsOutcomesLen; i++)
		{
			this.NamedPartyTeamsOutcomes[i] = new NamedPartyTeamWithOutcome();
			this.NamedPartyTeamsOutcomes[i].Deserialize(reader);
		}
	}
}
}
