using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ShortcutBarRefreshMessage : NetworkMessage
{

	public const uint Id = 6229;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte BarType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public Shortcut Shortcut { get; set; }

	public ShortcutBarRefreshMessage() {}


	public ShortcutBarRefreshMessage InitShortcutBarRefreshMessage(byte BarType, Shortcut Shortcut)
	{
		this.BarType = BarType;
		this.Shortcut = Shortcut;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.BarType);
writer.WriteShort(Shortcut.MessageId);
		Shortcut.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.BarType = reader.ReadByte();
this.Shortcut = ProtocolTypeManager.GetInstance<Shortcut>(reader.ReadShort());
		this.Shortcut.Deserialize(reader);
	}
}
}
