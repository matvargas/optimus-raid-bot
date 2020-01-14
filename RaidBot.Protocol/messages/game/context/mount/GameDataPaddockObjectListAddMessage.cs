using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameDataPaddockObjectListAddMessage : NetworkMessage
{

	public const uint Id = 5992;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PaddockItem[] PaddockItemDescription { get; set; }

	public GameDataPaddockObjectListAddMessage() {}


	public GameDataPaddockObjectListAddMessage InitGameDataPaddockObjectListAddMessage(PaddockItem[] PaddockItemDescription)
	{
		this.PaddockItemDescription = PaddockItemDescription;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.PaddockItemDescription.Length);
		foreach (PaddockItem item in this.PaddockItemDescription)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int PaddockItemDescriptionLen = reader.ReadShort();
		PaddockItemDescription = new PaddockItem[PaddockItemDescriptionLen];
		for (int i = 0; i < PaddockItemDescriptionLen; i++)
		{
			this.PaddockItemDescription[i] = new PaddockItem();
			this.PaddockItemDescription[i].Deserialize(reader);
		}
	}
}
}
