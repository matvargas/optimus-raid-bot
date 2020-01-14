using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class TaxCollectorStaticExtendedInformations : TaxCollectorStaticInformations
{

	public const uint Id = 440;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AllianceInformations AllianceIdentity { get; set; }

	public TaxCollectorStaticExtendedInformations() {}


	public TaxCollectorStaticExtendedInformations InitTaxCollectorStaticExtendedInformations(AllianceInformations AllianceIdentity)
	{
		this.AllianceIdentity = AllianceIdentity;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.AllianceIdentity.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.AllianceIdentity = new AllianceInformations();
		this.AllianceIdentity.Deserialize(reader);
	}
}
}
