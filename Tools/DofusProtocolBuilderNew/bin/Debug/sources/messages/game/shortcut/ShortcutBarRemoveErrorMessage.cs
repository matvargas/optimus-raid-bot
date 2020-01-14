using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ShortcutBarRemoveErrorMessage : NetworkMessage
{

	public const uint Id = 6222;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Error { get; set; }

	public ShortcutBarRemoveErrorMessage() {}


	public ShortcutBarRemoveErrorMessage InitShortcutBarRemoveErrorMessage(byte Error)
	{
		this.Error = Error;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Error);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Error = reader.ReadByte();
	}
}
}
