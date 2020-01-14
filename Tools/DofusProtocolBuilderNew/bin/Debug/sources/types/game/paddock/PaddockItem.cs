using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PaddockItem : ObjectItemInRolePlay
{

	public const uint Id = 185;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ItemDurability Durability { get; set; }

	public PaddockItem() {}


	public PaddockItem InitPaddockItem(ItemDurability Durability)
	{
		this.Durability = Durability;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.Durability.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Durability = new ItemDurability();
		this.Durability.Deserialize(reader);
	}
}
}
