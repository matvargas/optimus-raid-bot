using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AbstractTaxCollectorListMessage : NetworkMessage
{

	public const uint Id = 6568;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public TaxCollectorInformations[] Informations { get; set; }

	public AbstractTaxCollectorListMessage() {}


	public AbstractTaxCollectorListMessage InitAbstractTaxCollectorListMessage(TaxCollectorInformations[] Informations)
	{
		this.Informations = Informations;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Informations.Length);
		foreach (TaxCollectorInformations item in this.Informations)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int InformationsLen = reader.ReadShort();
		Informations = new TaxCollectorInformations[InformationsLen];
		for (int i = 0; i < InformationsLen; i++)
		{
			this.Informations[i] = ProtocolTypeManager.GetInstance<TaxCollectorInformations>(reader.ReadShort());
			this.Informations[i].Deserialize(reader);
		}
	}
}
}
