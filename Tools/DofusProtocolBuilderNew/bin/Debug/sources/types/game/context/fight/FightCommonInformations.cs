using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightCommonInformations : NetworkType
{

	public const uint Id = 43;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte FightType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightTeamInformations[] FightTeams { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] FightTeamsPositions { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightOptionsInformations[] FightTeamsOptions { get; set; }

	public FightCommonInformations() {}


	public FightCommonInformations InitFightCommonInformations(short FightId, byte FightType, FightTeamInformations[] FightTeams, short[] FightTeamsPositions, FightOptionsInformations[] FightTeamsOptions)
	{
		this.FightId = FightId;
		this.FightType = FightType;
		this.FightTeams = FightTeams;
		this.FightTeamsPositions = FightTeamsPositions;
		this.FightTeamsOptions = FightTeamsOptions;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.FightId);
		writer.WriteByte(this.FightType);
		writer.WriteShort(this.FightTeams.Length);
		foreach (FightTeamInformations item in this.FightTeams)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteShort(this.FightTeamsPositions.Length);
		foreach (short item in this.FightTeamsPositions)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.FightTeamsOptions.Length);
		foreach (FightOptionsInformations item in this.FightTeamsOptions)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FightId = reader.ReadVarShort();
		this.FightType = reader.ReadByte();
		int FightTeamsLen = reader.ReadShort();
		FightTeams = new FightTeamInformations[FightTeamsLen];
		for (int i = 0; i < FightTeamsLen; i++)
		{
			this.FightTeams[i] = ProtocolTypeManager.GetInstance<FightTeamInformations>(reader.ReadShort());
			this.FightTeams[i].Deserialize(reader);
		}
		int FightTeamsPositionsLen = reader.ReadShort();
		FightTeamsPositions = new short[FightTeamsPositionsLen];
		for (int i = 0; i < FightTeamsPositionsLen; i++)
		{
			this.FightTeamsPositions[i] = reader.ReadVarShort();
		}
		int FightTeamsOptionsLen = reader.ReadShort();
		FightTeamsOptions = new FightOptionsInformations[FightTeamsOptionsLen];
		for (int i = 0; i < FightTeamsOptionsLen; i++)
		{
			this.FightTeamsOptions[i] = new FightOptionsInformations();
			this.FightTeamsOptions[i].Deserialize(reader);
		}
	}
}
}
