using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class SellerBuyerDescriptor : NetworkType
{

	public const uint Id = 121;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] Quantities { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] Types { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public float TaxPercentage { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public float TaxModificationPercentage { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte MaxItemLevel { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MaxItemPerAccount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int NpcContextualId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short UnsoldDelay { get; set; }

	public SellerBuyerDescriptor() {}


	public SellerBuyerDescriptor InitSellerBuyerDescriptor(int[] Quantities, int[] Types, float TaxPercentage, float TaxModificationPercentage, byte MaxItemLevel, int MaxItemPerAccount, int NpcContextualId, short UnsoldDelay)
	{
		this.Quantities = Quantities;
		this.Types = Types;
		this.TaxPercentage = TaxPercentage;
		this.TaxModificationPercentage = TaxModificationPercentage;
		this.MaxItemLevel = MaxItemLevel;
		this.MaxItemPerAccount = MaxItemPerAccount;
		this.NpcContextualId = NpcContextualId;
		this.UnsoldDelay = UnsoldDelay;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Quantities.Length);
		foreach (int item in this.Quantities)
		{
			writer.WriteVarInt(item);
		}
		writer.WriteShort(this.Types.Length);
		foreach (int item in this.Types)
		{
			writer.WriteVarInt(item);
		}
		writer.WriteFloat(this.TaxPercentage);
		writer.WriteFloat(this.TaxModificationPercentage);
		writer.WriteByte(this.MaxItemLevel);
		writer.WriteVarInt(this.MaxItemPerAccount);
		writer.WriteInt(this.NpcContextualId);
		writer.WriteVarShort(this.UnsoldDelay);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int QuantitiesLen = reader.ReadShort();
		Quantities = new int[QuantitiesLen];
		for (int i = 0; i < QuantitiesLen; i++)
		{
			this.Quantities[i] = reader.ReadVarInt();
		}
		int TypesLen = reader.ReadShort();
		Types = new int[TypesLen];
		for (int i = 0; i < TypesLen; i++)
		{
			this.Types[i] = reader.ReadVarInt();
		}
		this.TaxPercentage = reader.ReadFloat();
		this.TaxModificationPercentage = reader.ReadFloat();
		this.MaxItemLevel = reader.ReadByte();
		this.MaxItemPerAccount = reader.ReadVarInt();
		this.NpcContextualId = reader.ReadInt();
		this.UnsoldDelay = reader.ReadVarShort();
	}
}
}
