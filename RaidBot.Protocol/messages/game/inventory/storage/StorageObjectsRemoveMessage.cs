using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class StorageObjectsRemoveMessage : NetworkMessage
{

	public const uint Id = 6035;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] ObjectUIDList { get; set; }

	public StorageObjectsRemoveMessage() {}


	public StorageObjectsRemoveMessage InitStorageObjectsRemoveMessage(int[] ObjectUIDList)
	{
		this.ObjectUIDList = ObjectUIDList;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.ObjectUIDList.Length);
		foreach (int item in this.ObjectUIDList)
		{
			writer.WriteVarInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ObjectUIDListLen = reader.ReadShort();
		ObjectUIDList = new int[ObjectUIDListLen];
		for (int i = 0; i < ObjectUIDListLen; i++)
		{
			this.ObjectUIDList[i] = reader.ReadVarInt();
		}
	}
}
}
