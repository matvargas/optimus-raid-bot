using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ObjectItemGenericQuantityPrice : ObjectItemGenericQuantity
{

	public const uint Id = 494;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Price { get; set; }

	public ObjectItemGenericQuantityPrice() {}


	public ObjectItemGenericQuantityPrice InitObjectItemGenericQuantityPrice(long Price)
	{
		this.Price = Price;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.Price);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Price = reader.ReadVarLong();
	}
}
}
