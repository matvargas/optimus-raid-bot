using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ShortcutBarRemoveRequestMessage : NetworkMessage
{

	public const uint Id = 6228;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte BarType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Slot { get; set; }

	public ShortcutBarRemoveRequestMessage() {}


	public ShortcutBarRemoveRequestMessage InitShortcutBarRemoveRequestMessage(byte BarType, byte Slot)
	{
		this.BarType = BarType;
		this.Slot = Slot;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.BarType);
		writer.WriteByte(this.Slot);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.BarType = reader.ReadByte();
		this.Slot = reader.ReadByte();
	}
}
}
