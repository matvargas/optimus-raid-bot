using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildLevelUpMessage : NetworkMessage
{

	public const uint Id = 6062;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NewLevel { get; set; }

	public GuildLevelUpMessage() {}


	public GuildLevelUpMessage InitGuildLevelUpMessage(byte NewLevel)
	{
		this.NewLevel = NewLevel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.NewLevel);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.NewLevel = reader.ReadByte();
	}
}
}
