using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeObjectsRemovedMessage : ExchangeObjectMessage
{

	public const uint Id = 6532;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] ObjectUID { get; set; }

	public ExchangeObjectsRemovedMessage() {}


	public ExchangeObjectsRemovedMessage InitExchangeObjectsRemovedMessage(int[] ObjectUID)
	{
		this.ObjectUID = ObjectUID;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.ObjectUID.Length);
		foreach (int item in this.ObjectUID)
		{
			writer.WriteVarInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int ObjectUIDLen = reader.ReadShort();
		ObjectUID = new int[ObjectUIDLen];
		for (int i = 0; i < ObjectUIDLen; i++)
		{
			this.ObjectUID[i] = reader.ReadVarInt();
		}
	}
}
}
