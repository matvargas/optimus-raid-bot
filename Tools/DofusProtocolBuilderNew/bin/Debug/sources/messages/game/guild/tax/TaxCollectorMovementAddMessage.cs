using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TaxCollectorMovementAddMessage : NetworkMessage
{

	public const uint Id = 5917;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public TaxCollectorInformations Informations { get; set; }

	public TaxCollectorMovementAddMessage() {}


	public TaxCollectorMovementAddMessage InitTaxCollectorMovementAddMessage(TaxCollectorInformations Informations)
	{
		this.Informations = Informations;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
writer.WriteShort(Informations.MessageId);
		Informations.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
this.Informations = ProtocolTypeManager.GetInstance<TaxCollectorInformations>(reader.ReadShort());
		this.Informations.Deserialize(reader);
	}
}
}
