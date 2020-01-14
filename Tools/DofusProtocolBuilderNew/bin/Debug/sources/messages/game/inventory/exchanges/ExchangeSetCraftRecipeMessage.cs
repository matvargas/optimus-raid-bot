using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeSetCraftRecipeMessage : NetworkMessage
{

	public const uint Id = 6389;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ObjectGID { get; set; }

	public ExchangeSetCraftRecipeMessage() {}


	public ExchangeSetCraftRecipeMessage InitExchangeSetCraftRecipeMessage(short ObjectGID)
	{
		this.ObjectGID = ObjectGID;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.ObjectGID);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ObjectGID = reader.ReadVarShort();
	}
}
}
