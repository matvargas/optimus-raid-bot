using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightTeamMemberMonsterInformations : FightTeamMemberInformations
{

	public const uint Id = 6;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MonsterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Grade { get; set; }

	public FightTeamMemberMonsterInformations() {}


	public FightTeamMemberMonsterInformations InitFightTeamMemberMonsterInformations(int MonsterId, byte Grade)
	{
		this.MonsterId = MonsterId;
		this.Grade = Grade;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(this.MonsterId);
		writer.WriteByte(this.Grade);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.MonsterId = reader.ReadInt();
		this.Grade = reader.ReadByte();
	}
}
}
