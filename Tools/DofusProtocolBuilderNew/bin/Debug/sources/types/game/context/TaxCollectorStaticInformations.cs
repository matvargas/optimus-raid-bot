using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class TaxCollectorStaticInformations : NetworkType
{

	public const uint Id = 147;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FirstNameId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short LastNameId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildInformations GuildIdentity { get; set; }

	public TaxCollectorStaticInformations() {}


	public TaxCollectorStaticInformations InitTaxCollectorStaticInformations(short FirstNameId, short LastNameId, GuildInformations GuildIdentity)
	{
		this.FirstNameId = FirstNameId;
		this.LastNameId = LastNameId;
		this.GuildIdentity = GuildIdentity;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.FirstNameId);
		writer.WriteVarShort(this.LastNameId);
		this.GuildIdentity.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FirstNameId = reader.ReadVarShort();
		this.LastNameId = reader.ReadVarShort();
		this.GuildIdentity = new GuildInformations();
		this.GuildIdentity.Deserialize(reader);
	}
}
}
