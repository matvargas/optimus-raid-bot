using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ItemsPreset : Preset
{

	public const uint Id = 517;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ItemForPreset[] Items { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool MountEquipped { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityLook Look { get; set; }

	public ItemsPreset() {}


	public ItemsPreset InitItemsPreset(ItemForPreset[] Items, bool MountEquipped, EntityLook Look)
	{
		this.Items = Items;
		this.MountEquipped = MountEquipped;
		this.Look = Look;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Items.Length);
		foreach (ItemForPreset item in this.Items)
		{
			item.Serialize(writer);
		}
		writer.WriteBoolean(this.MountEquipped);
		this.Look.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int ItemsLen = reader.ReadShort();
		Items = new ItemForPreset[ItemsLen];
		for (int i = 0; i < ItemsLen; i++)
		{
			this.Items[i] = new ItemForPreset();
			this.Items[i].Deserialize(reader);
		}
		this.MountEquipped = reader.ReadBoolean();
		this.Look = new EntityLook();
		this.Look.Deserialize(reader);
	}
}
}
