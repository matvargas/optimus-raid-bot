using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class NotificationListMessage : NetworkMessage
{

	public const uint Id = 6087;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] Flags { get; set; }

	public NotificationListMessage() {}


	public NotificationListMessage InitNotificationListMessage(int[] Flags)
	{
		this.Flags = Flags;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Flags.Length);
		foreach (int item in this.Flags)
		{
			writer.WriteVarInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int FlagsLen = reader.ReadShort();
		Flags = new int[FlagsLen];
		for (int i = 0; i < FlagsLen; i++)
		{
			this.Flags[i] = reader.ReadVarInt();
		}
	}
}
}
