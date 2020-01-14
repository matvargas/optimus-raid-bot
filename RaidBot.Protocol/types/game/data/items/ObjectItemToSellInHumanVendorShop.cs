using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ObjectItemToSellInHumanVendorShop : Item
{

	public const uint Id = 359;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ObjectGID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectEffect[] Effects { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ObjectUID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Quantity { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long ObjectPrice { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long PublicPrice { get; set; }

	public ObjectItemToSellInHumanVendorShop() {}


	public ObjectItemToSellInHumanVendorShop InitObjectItemToSellInHumanVendorShop(short ObjectGID, ObjectEffect[] Effects, int ObjectUID, int Quantity, long ObjectPrice, long PublicPrice)
	{
		this.ObjectGID = ObjectGID;
		this.Effects = Effects;
		this.ObjectUID = ObjectUID;
		this.Quantity = Quantity;
		this.ObjectPrice = ObjectPrice;
		this.PublicPrice = PublicPrice;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.ObjectGID);
		writer.WriteShort(this.Effects.Length);
		foreach (ObjectEffect item in this.Effects)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteVarInt(this.ObjectUID);
		writer.WriteVarInt(this.Quantity);
		writer.WriteVarLong(this.ObjectPrice);
		writer.WriteVarLong(this.PublicPrice);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.ObjectGID = reader.ReadVarShort();
		int EffectsLen = reader.ReadShort();
		Effects = new ObjectEffect[EffectsLen];
		for (int i = 0; i < EffectsLen; i++)
		{
			this.Effects[i] = ProtocolTypeManager.GetInstance<ObjectEffect>(reader.ReadShort());
			this.Effects[i].Deserialize(reader);
		}
		this.ObjectUID = reader.ReadVarInt();
		this.Quantity = reader.ReadVarInt();
		this.ObjectPrice = reader.ReadVarLong();
		this.PublicPrice = reader.ReadVarLong();
	}
}
}
