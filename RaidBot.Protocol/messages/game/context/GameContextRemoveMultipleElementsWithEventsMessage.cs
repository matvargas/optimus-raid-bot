using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameContextRemoveMultipleElementsWithEventsMessage : GameContextRemoveMultipleElementsMessage
{

	public const uint Id = 6416;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] ElementEventIds { get; set; }

	public GameContextRemoveMultipleElementsWithEventsMessage() {}


	public GameContextRemoveMultipleElementsWithEventsMessage InitGameContextRemoveMultipleElementsWithEventsMessage(byte[] ElementEventIds)
	{
		this.ElementEventIds = ElementEventIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.ElementEventIds.Length);
		foreach (byte item in this.ElementEventIds)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int ElementEventIdsLen = reader.ReadShort();
		ElementEventIds = new byte[ElementEventIdsLen];
		for (int i = 0; i < ElementEventIdsLen; i++)
		{
			this.ElementEventIds[i] = reader.ReadByte();
		}
	}
}
}
