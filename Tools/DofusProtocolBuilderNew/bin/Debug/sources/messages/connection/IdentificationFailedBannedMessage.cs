using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdentificationFailedBannedMessage : IdentificationFailedMessage
{

	public const uint Id = 6174;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double BanEndDate { get; set; }

	public IdentificationFailedBannedMessage() {}


	public IdentificationFailedBannedMessage InitIdentificationFailedBannedMessage(double BanEndDate)
	{
		this.BanEndDate = BanEndDate;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.BanEndDate);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.BanEndDate = reader.ReadDouble();
	}
}
}
