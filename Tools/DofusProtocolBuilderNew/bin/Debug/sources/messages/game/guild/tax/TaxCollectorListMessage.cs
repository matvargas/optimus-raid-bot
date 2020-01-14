using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TaxCollectorListMessage : AbstractTaxCollectorListMessage
{

	public const uint Id = 5930;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NbcollectorMax { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public TaxCollectorFightersInformation[] FightersInformations { get; set; }

	public TaxCollectorListMessage() {}


	public TaxCollectorListMessage InitTaxCollectorListMessage(byte NbcollectorMax, TaxCollectorFightersInformation[] FightersInformations)
	{
		this.NbcollectorMax = NbcollectorMax;
		this.FightersInformations = FightersInformations;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.NbcollectorMax);
		writer.WriteShort(this.FightersInformations.Length);
		foreach (TaxCollectorFightersInformation item in this.FightersInformations)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.NbcollectorMax = reader.ReadByte();
		int FightersInformationsLen = reader.ReadShort();
		FightersInformations = new TaxCollectorFightersInformation[FightersInformationsLen];
		for (int i = 0; i < FightersInformationsLen; i++)
		{
			this.FightersInformations[i] = new TaxCollectorFightersInformation();
			this.FightersInformations[i].Deserialize(reader);
		}
	}
}
}
