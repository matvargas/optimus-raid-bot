using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeBuyMessage : NetworkMessage
{

	public const uint Id = 5774;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ObjectToBuyId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Quantity { get; set; }

	public ExchangeBuyMessage() {}


	public ExchangeBuyMessage InitExchangeBuyMessage(int ObjectToBuyId, int Quantity)
	{
		this.ObjectToBuyId = ObjectToBuyId;
		this.Quantity = Quantity;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.ObjectToBuyId);
		writer.WriteVarInt(this.Quantity);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ObjectToBuyId = reader.ReadVarInt();
		this.Quantity = reader.ReadVarInt();
	}
}
}
