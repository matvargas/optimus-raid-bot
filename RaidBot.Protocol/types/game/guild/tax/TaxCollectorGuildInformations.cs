using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class TaxCollectorGuildInformations : TaxCollectorComplementaryInformations
{

	public const uint Id = 446;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public BasicGuildInformations Guild { get; set; }

	public TaxCollectorGuildInformations() {}


	public TaxCollectorGuildInformations InitTaxCollectorGuildInformations(BasicGuildInformations Guild)
	{
		this.Guild = Guild;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.Guild.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Guild = new BasicGuildInformations();
		this.Guild.Deserialize(reader);
	}
}
}
