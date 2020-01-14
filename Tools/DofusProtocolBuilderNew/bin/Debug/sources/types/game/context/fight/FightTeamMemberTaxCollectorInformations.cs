using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightTeamMemberTaxCollectorInformations : FightTeamMemberInformations
{

	public const uint Id = 177;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FirstNameId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short LastNameId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Level { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int GuildId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double Uid { get; set; }

	public FightTeamMemberTaxCollectorInformations() {}


	public FightTeamMemberTaxCollectorInformations InitFightTeamMemberTaxCollectorInformations(short FirstNameId, short LastNameId, byte Level, int GuildId, double Uid)
	{
		this.FirstNameId = FirstNameId;
		this.LastNameId = LastNameId;
		this.Level = Level;
		this.GuildId = GuildId;
		this.Uid = Uid;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.FirstNameId);
		writer.WriteVarShort(this.LastNameId);
		writer.WriteByte(this.Level);
		writer.WriteVarInt(this.GuildId);
		writer.WriteDouble(this.Uid);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.FirstNameId = reader.ReadVarShort();
		this.LastNameId = reader.ReadVarShort();
		this.Level = reader.ReadByte();
		this.GuildId = reader.ReadVarInt();
		this.Uid = reader.ReadDouble();
	}
}
}
