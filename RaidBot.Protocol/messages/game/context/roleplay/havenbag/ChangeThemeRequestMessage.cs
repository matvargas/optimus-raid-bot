using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ChangeThemeRequestMessage : NetworkMessage
{

	public const uint Id = 6639;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Theme { get; set; }

	public ChangeThemeRequestMessage() {}


	public ChangeThemeRequestMessage InitChangeThemeRequestMessage(byte Theme)
	{
		this.Theme = Theme;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Theme);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Theme = reader.ReadByte();
	}
}
}
