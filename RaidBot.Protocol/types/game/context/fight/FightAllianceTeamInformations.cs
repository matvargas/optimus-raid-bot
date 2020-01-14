using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightAllianceTeamInformations : FightTeamInformations
{

	public const uint Id = 439;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Relation { get; set; }

	public FightAllianceTeamInformations() {}


	public FightAllianceTeamInformations InitFightAllianceTeamInformations(byte Relation)
	{
		this.Relation = Relation;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.Relation);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Relation = reader.ReadByte();
	}
}
}
