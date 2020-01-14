using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TaxCollectorDialogQuestionExtendedMessage : TaxCollectorDialogQuestionBasicMessage
{

	public const uint Id = 5615;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MaxPods { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Prospecting { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Wisdom { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TaxCollectorsCount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int TaxCollectorAttack { get; set; }
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

	public TaxCollectorDialogQuestionExtendedMessage() {}


	public TaxCollectorDialogQuestionExtendedMessage InitTaxCollectorDialogQuestionExtendedMessage(short MaxPods, short Prospecting, short Wisdom, byte TaxCollectorsCount, int TaxCollectorAttack, long Kamas, long Experience, int Pods, long ItemsValue)
	{
		this.MaxPods = MaxPods;
		this.Prospecting = Prospecting;
		this.Wisdom = Wisdom;
		this.TaxCollectorsCount = TaxCollectorsCount;
		this.TaxCollectorAttack = TaxCollectorAttack;
		this.Kamas = Kamas;
		this.Experience = Experience;
		this.Pods = Pods;
		this.ItemsValue = ItemsValue;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.MaxPods);
		writer.WriteVarShort(this.Prospecting);
		writer.WriteVarShort(this.Wisdom);
		writer.WriteByte(this.TaxCollectorsCount);
		writer.WriteInt(this.TaxCollectorAttack);
		writer.WriteVarLong(this.Kamas);
		writer.WriteVarLong(this.Experience);
		writer.WriteVarInt(this.Pods);
		writer.WriteVarLong(this.ItemsValue);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.MaxPods = reader.ReadVarShort();
		this.Prospecting = reader.ReadVarShort();
		this.Wisdom = reader.ReadVarShort();
		this.TaxCollectorsCount = reader.ReadByte();
		this.TaxCollectorAttack = reader.ReadInt();
		this.Kamas = reader.ReadVarLong();
		this.Experience = reader.ReadVarLong();
		this.Pods = reader.ReadVarInt();
		this.ItemsValue = reader.ReadVarLong();
	}
}
}
