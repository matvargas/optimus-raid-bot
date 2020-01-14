using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class EvolutiveObjectRecycleResultMessage : NetworkMessage
{

	public const uint Id = 6779;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public RecycledItem[] RecycledItems { get; set; }

	public EvolutiveObjectRecycleResultMessage() {}


	public EvolutiveObjectRecycleResultMessage InitEvolutiveObjectRecycleResultMessage(RecycledItem[] RecycledItems)
	{
		this.RecycledItems = RecycledItems;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.RecycledItems.Length);
		foreach (RecycledItem item in this.RecycledItems)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int RecycledItemsLen = reader.ReadShort();
		RecycledItems = new RecycledItem[RecycledItemsLen];
		for (int i = 0; i < RecycledItemsLen; i++)
		{
			this.RecycledItems[i] = new RecycledItem();
			this.RecycledItems[i].Deserialize(reader);
		}
	}
}
}
