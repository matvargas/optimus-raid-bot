using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ShortcutBarContentMessage : NetworkMessage
{

	public const uint Id = 6231;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte BarType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public Shortcut[] Shortcuts { get; set; }

	public ShortcutBarContentMessage() {}


	public ShortcutBarContentMessage InitShortcutBarContentMessage(byte BarType, Shortcut[] Shortcuts)
	{
		this.BarType = BarType;
		this.Shortcuts = Shortcuts;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.BarType);
		writer.WriteShort(this.Shortcuts.Length);
		foreach (Shortcut item in this.Shortcuts)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.BarType = reader.ReadByte();
		int ShortcutsLen = reader.ReadShort();
		Shortcuts = new Shortcut[ShortcutsLen];
		for (int i = 0; i < ShortcutsLen; i++)
		{
			this.Shortcuts[i] = ProtocolTypeManager.GetInstance<Shortcut>(reader.ReadShort());
			this.Shortcuts[i].Deserialize(reader);
		}
	}
}
}
