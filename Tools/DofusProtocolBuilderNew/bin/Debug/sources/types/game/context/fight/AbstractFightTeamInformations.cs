using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class AbstractFightTeamInformations : NetworkType
{

	public const uint Id = 116;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TeamId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double LeaderId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TeamSide { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TeamTypeId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NbWaves { get; set; }

	public AbstractFightTeamInformations() {}


	public AbstractFightTeamInformations InitAbstractFightTeamInformations(byte TeamId, double LeaderId, byte TeamSide, byte TeamTypeId, byte NbWaves)
	{
		this.TeamId = TeamId;
		this.LeaderId = LeaderId;
		this.TeamSide = TeamSide;
		this.TeamTypeId = TeamTypeId;
		this.NbWaves = NbWaves;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.TeamId);
		writer.WriteDouble(this.LeaderId);
		writer.WriteByte(this.TeamSide);
		writer.WriteByte(this.TeamTypeId);
		writer.WriteByte(this.NbWaves);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.TeamId = reader.ReadByte();
		this.LeaderId = reader.ReadDouble();
		this.TeamSide = reader.ReadByte();
		this.TeamTypeId = reader.ReadByte();
		this.NbWaves = reader.ReadByte();
	}
}
}
