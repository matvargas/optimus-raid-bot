using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ItemDurability : NetworkType
{

	public const uint Id = 168;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Durability { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DurabilityMax { get; set; }

	public ItemDurability() {}


	public ItemDurability InitItemDurability(short Durability, short DurabilityMax)
	{
		this.Durability = Durability;
		this.DurabilityMax = DurabilityMax;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Durability);
		writer.WriteShort(this.DurabilityMax);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Durability = reader.ReadShort();
		this.DurabilityMax = reader.ReadShort();
	}
}
}
