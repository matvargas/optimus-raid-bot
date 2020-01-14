using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class InventoryContentMessage : NetworkMessage
{

	public const uint Id = 3016;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItem[] Objects { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Kamas { get; set; }

	public InventoryContentMessage() {}


	public InventoryContentMessage InitInventoryContentMessage(ObjectItem[] Objects, long Kamas)
	{
		this.Objects = Objects;
		this.Kamas = Kamas;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Objects.Length);
		foreach (ObjectItem item in this.Objects)
		{
			item.Serialize(writer);
		}
		writer.WriteVarLong(this.Kamas);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ObjectsLen = reader.ReadShort();
		Objects = new ObjectItem[ObjectsLen];
		for (int i = 0; i < ObjectsLen; i++)
		{
			this.Objects[i] = new ObjectItem();
			this.Objects[i].Deserialize(reader);
		}
		this.Kamas = reader.ReadVarLong();
	}
}
}
