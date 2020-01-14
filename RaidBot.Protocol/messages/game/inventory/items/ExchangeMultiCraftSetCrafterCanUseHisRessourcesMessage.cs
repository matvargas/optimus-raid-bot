using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage : NetworkMessage
{

	public const uint Id = 6021;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Allow { get; set; }

	public ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage() {}


	public ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage InitExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage(bool Allow)
	{
		this.Allow = Allow;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Allow);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Allow = reader.ReadBoolean();
	}
}
}
