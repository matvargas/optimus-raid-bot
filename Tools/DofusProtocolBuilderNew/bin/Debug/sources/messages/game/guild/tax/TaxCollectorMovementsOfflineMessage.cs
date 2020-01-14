using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TaxCollectorMovementsOfflineMessage : NetworkMessage
{

	public const uint Id = 6611;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public TaxCollectorMovement[] Movements { get; set; }

	public TaxCollectorMovementsOfflineMessage() {}


	public TaxCollectorMovementsOfflineMessage InitTaxCollectorMovementsOfflineMessage(TaxCollectorMovement[] Movements)
	{
		this.Movements = Movements;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Movements.Length);
		foreach (TaxCollectorMovement item in this.Movements)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int MovementsLen = reader.ReadShort();
		Movements = new TaxCollectorMovement[MovementsLen];
		for (int i = 0; i < MovementsLen; i++)
		{
			this.Movements[i] = new TaxCollectorMovement();
			this.Movements[i].Deserialize(reader);
		}
	}
}
}
