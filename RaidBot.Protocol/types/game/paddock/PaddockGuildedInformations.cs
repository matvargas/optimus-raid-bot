using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PaddockGuildedInformations : PaddockBuyableInformations
{

	public const uint Id = 508;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Deserted { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildInformations GuildInfo { get; set; }

	public PaddockGuildedInformations() {}


	public PaddockGuildedInformations InitPaddockGuildedInformations(bool Deserted, GuildInformations GuildInfo)
	{
		this.Deserted = Deserted;
		this.GuildInfo = GuildInfo;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(this.Deserted);
		this.GuildInfo.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Deserted = reader.ReadBoolean();
		this.GuildInfo = new GuildInformations();
		this.GuildInfo.Deserialize(reader);
	}
}
}
