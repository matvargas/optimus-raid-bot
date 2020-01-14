using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HumanOptionGuild : HumanOption
{

	public const uint Id = 409;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildInformations GuildInformations { get; set; }

	public HumanOptionGuild() {}


	public HumanOptionGuild InitHumanOptionGuild(GuildInformations GuildInformations)
	{
		this.GuildInformations = GuildInformations;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.GuildInformations.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.GuildInformations = new GuildInformations();
		this.GuildInformations.Deserialize(reader);
	}
}
}
