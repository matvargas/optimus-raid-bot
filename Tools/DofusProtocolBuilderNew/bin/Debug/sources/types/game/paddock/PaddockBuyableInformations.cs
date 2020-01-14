using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PaddockBuyableInformations : NetworkType
{

	public const uint Id = 130;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Price { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Locked { get; set; }

	public PaddockBuyableInformations() {}


	public PaddockBuyableInformations InitPaddockBuyableInformations(long Price, bool Locked)
	{
		this.Price = Price;
		this.Locked = Locked;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.Price);
		writer.WriteBoolean(this.Locked);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Price = reader.ReadVarLong();
		this.Locked = reader.ReadBoolean();
	}
}
}
