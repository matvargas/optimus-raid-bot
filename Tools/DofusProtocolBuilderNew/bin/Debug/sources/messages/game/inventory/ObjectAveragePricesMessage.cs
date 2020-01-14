using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ObjectAveragePricesMessage : NetworkMessage
{

	public const uint Id = 6335;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Ids { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long[] AvgPrices { get; set; }

	public ObjectAveragePricesMessage() {}


	public ObjectAveragePricesMessage InitObjectAveragePricesMessage(short[] Ids, long[] AvgPrices)
	{
		this.Ids = Ids;
		this.AvgPrices = AvgPrices;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Ids.Length);
		foreach (short item in this.Ids)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.AvgPrices.Length);
		foreach (long item in this.AvgPrices)
		{
			writer.WriteVarLong(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int IdsLen = reader.ReadShort();
		Ids = new short[IdsLen];
		for (int i = 0; i < IdsLen; i++)
		{
			this.Ids[i] = reader.ReadVarShort();
		}
		int AvgPricesLen = reader.ReadShort();
		AvgPrices = new long[AvgPricesLen];
		for (int i = 0; i < AvgPricesLen; i++)
		{
			this.AvgPrices[i] = reader.ReadVarLong();
		}
	}
}
}
