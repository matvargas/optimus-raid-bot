using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class StorageObjectsUpdateMessage : NetworkMessage
{

	public const uint Id = 6036;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ObjectItem[] ObjectList { get; set; }

	public StorageObjectsUpdateMessage() {}


	public StorageObjectsUpdateMessage InitStorageObjectsUpdateMessage(ObjectItem[] ObjectList)
	{
		this.ObjectList = ObjectList;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.ObjectList.Length);
		foreach (ObjectItem item in this.ObjectList)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ObjectListLen = reader.ReadShort();
		ObjectList = new ObjectItem[ObjectListLen];
		for (int i = 0; i < ObjectListLen; i++)
		{
			this.ObjectList[i] = new ObjectItem();
			this.ObjectList[i].Deserialize(reader);
		}
	}
}
}
