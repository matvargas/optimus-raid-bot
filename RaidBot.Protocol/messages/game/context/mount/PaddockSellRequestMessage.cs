using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PaddockSellRequestMessage : NetworkMessage
{

	public const uint Id = 5953;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Price { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool ForSale { get; set; }

	public PaddockSellRequestMessage() {}


	public PaddockSellRequestMessage InitPaddockSellRequestMessage(long Price, bool ForSale)
	{
		this.Price = Price;
		this.ForSale = ForSale;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.Price);
		writer.WriteBoolean(this.ForSale);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Price = reader.ReadVarLong();
		this.ForSale = reader.ReadBoolean();
	}
}
}
