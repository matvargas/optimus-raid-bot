using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ObjectItemToSellInNpcShop : ObjectItemMinimalInformation
{

	public const uint Id = 352;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long ObjectPrice { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String BuyCriterion { get; set; }

	public ObjectItemToSellInNpcShop() {}


	public ObjectItemToSellInNpcShop InitObjectItemToSellInNpcShop(long ObjectPrice, String BuyCriterion)
	{
		this.ObjectPrice = ObjectPrice;
		this.BuyCriterion = BuyCriterion;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.ObjectPrice);
		writer.WriteUTF(this.BuyCriterion);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.ObjectPrice = reader.ReadVarLong();
		this.BuyCriterion = reader.ReadUTF();
	}
}
}
