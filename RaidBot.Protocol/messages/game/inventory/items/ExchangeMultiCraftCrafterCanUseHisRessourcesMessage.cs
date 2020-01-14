using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeMultiCraftCrafterCanUseHisRessourcesMessage : NetworkMessage
{

	public const uint Id = 6020;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Allowed { get; set; }

	public ExchangeMultiCraftCrafterCanUseHisRessourcesMessage() {}


	public ExchangeMultiCraftCrafterCanUseHisRessourcesMessage InitExchangeMultiCraftCrafterCanUseHisRessourcesMessage(bool Allowed)
	{
		this.Allowed = Allowed;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Allowed);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Allowed = reader.ReadBoolean();
	}
}
}
