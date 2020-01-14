using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class DecraftedItemStackInfo : NetworkType
{

	public const uint Id = 481;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ObjectUID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public float BonusMin { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public float BonusMax { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] RunesId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] RunesQty { get; set; }

	public DecraftedItemStackInfo() {}


	public DecraftedItemStackInfo InitDecraftedItemStackInfo(int ObjectUID, float BonusMin, float BonusMax, short[] RunesId, int[] RunesQty)
	{
		this.ObjectUID = ObjectUID;
		this.BonusMin = BonusMin;
		this.BonusMax = BonusMax;
		this.RunesId = RunesId;
		this.RunesQty = RunesQty;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.ObjectUID);
		writer.WriteFloat(this.BonusMin);
		writer.WriteFloat(this.BonusMax);
		writer.WriteShort(this.RunesId.Length);
		foreach (short item in this.RunesId)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.RunesQty.Length);
		foreach (int item in this.RunesQty)
		{
			writer.WriteVarInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ObjectUID = reader.ReadVarInt();
		this.BonusMin = reader.ReadFloat();
		this.BonusMax = reader.ReadFloat();
		int RunesIdLen = reader.ReadShort();
		RunesId = new short[RunesIdLen];
		for (int i = 0; i < RunesIdLen; i++)
		{
			this.RunesId[i] = reader.ReadVarShort();
		}
		int RunesQtyLen = reader.ReadShort();
		RunesQty = new int[RunesQtyLen];
		for (int i = 0; i < RunesQtyLen; i++)
		{
			this.RunesQty[i] = reader.ReadVarInt();
		}
	}
}
}
