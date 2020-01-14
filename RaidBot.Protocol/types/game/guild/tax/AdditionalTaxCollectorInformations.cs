using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class AdditionalTaxCollectorInformations : NetworkType
{

	public const uint Id = 165;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String CollectorCallerName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Date { get; set; }

	public AdditionalTaxCollectorInformations() {}


	public AdditionalTaxCollectorInformations InitAdditionalTaxCollectorInformations(String CollectorCallerName, int Date)
	{
		this.CollectorCallerName = CollectorCallerName;
		this.Date = Date;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.CollectorCallerName);
		writer.WriteInt(this.Date);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CollectorCallerName = reader.ReadUTF();
		this.Date = reader.ReadInt();
	}
}
}
