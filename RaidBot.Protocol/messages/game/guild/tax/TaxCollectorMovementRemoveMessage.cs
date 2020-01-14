using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TaxCollectorMovementRemoveMessage : NetworkMessage
{

	public const uint Id = 5915;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double CollectorId { get; set; }

	public TaxCollectorMovementRemoveMessage() {}


	public TaxCollectorMovementRemoveMessage InitTaxCollectorMovementRemoveMessage(double CollectorId)
	{
		this.CollectorId = CollectorId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.CollectorId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CollectorId = reader.ReadDouble();
	}
}
}
