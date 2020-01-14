using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeBidHouseBuyMessage : NetworkMessage
{

	public const uint Id = 5804;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Uid { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Qty { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Price { get; set; }

	public ExchangeBidHouseBuyMessage() {}


	public ExchangeBidHouseBuyMessage InitExchangeBidHouseBuyMessage(int Uid, int Qty, long Price)
	{
		this.Uid = Uid;
		this.Qty = Qty;
		this.Price = Price;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.Uid);
		writer.WriteVarInt(this.Qty);
		writer.WriteVarLong(this.Price);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Uid = reader.ReadVarInt();
		this.Qty = reader.ReadVarInt();
		this.Price = reader.ReadVarLong();
	}
}
}
