using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PaddockSellBuyDialogMessage : NetworkMessage
{

	public const uint Id = 6018;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Bsell { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int OwnerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Price { get; set; }

	public PaddockSellBuyDialogMessage() {}


	public PaddockSellBuyDialogMessage InitPaddockSellBuyDialogMessage(bool Bsell, int OwnerId, long Price)
	{
		this.Bsell = Bsell;
		this.OwnerId = OwnerId;
		this.Price = Price;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Bsell);
		writer.WriteVarInt(this.OwnerId);
		writer.WriteVarLong(this.Price);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Bsell = reader.ReadBoolean();
		this.OwnerId = reader.ReadVarInt();
		this.Price = reader.ReadVarLong();
	}
}
}
