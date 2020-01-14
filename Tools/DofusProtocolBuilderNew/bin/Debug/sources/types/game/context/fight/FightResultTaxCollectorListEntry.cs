using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightResultTaxCollectorListEntry : FightResultFighterListEntry
{

	public const uint Id = 84;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Level { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public BasicGuildInformations GuildInfo { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ExperienceForGuild { get; set; }

	public FightResultTaxCollectorListEntry() {}


	public FightResultTaxCollectorListEntry InitFightResultTaxCollectorListEntry(byte Level, BasicGuildInformations GuildInfo, int ExperienceForGuild)
	{
		this.Level = Level;
		this.GuildInfo = GuildInfo;
		this.ExperienceForGuild = ExperienceForGuild;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.Level);
		this.GuildInfo.Serialize(writer);
		writer.WriteInt(this.ExperienceForGuild);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Level = reader.ReadByte();
		this.GuildInfo = new BasicGuildInformations();
		this.GuildInfo.Deserialize(reader);
		this.ExperienceForGuild = reader.ReadInt();
	}
}
}
