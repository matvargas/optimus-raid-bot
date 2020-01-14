using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class TaxCollectorLootInformations : TaxCollectorComplementaryInformations
{

	public const uint Id = 372;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Kamas { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Experience { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Pods { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long ItemsValue { get; set; }

	public TaxCollectorLootInformations() {}


	public TaxCollectorLootInformations InitTaxCollectorLootInformations(long Kamas, long Experience, int Pods, long ItemsValue)
	{
		this.Kamas = Kamas;
		this.Experience = Experience;
		this.Pods = Pods;
		this.ItemsValue = ItemsValue;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.Kamas);
		writer.WriteVarLong(this.Experience);
		writer.WriteVarInt(this.Pods);
		writer.WriteVarLong(this.ItemsValue);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Kamas = reader.ReadVarLong();
		this.Experience = reader.ReadVarLong();
		this.Pods = reader.ReadVarInt();
		this.ItemsValue = reader.ReadVarLong();
	}
}
}
