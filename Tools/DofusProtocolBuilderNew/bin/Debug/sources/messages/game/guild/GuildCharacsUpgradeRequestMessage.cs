using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildCharacsUpgradeRequestMessage : NetworkMessage
{

	public const uint Id = 5706;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte CharaTypeTarget { get; set; }

	public GuildCharacsUpgradeRequestMessage() {}


	public GuildCharacsUpgradeRequestMessage InitGuildCharacsUpgradeRequestMessage(byte CharaTypeTarget)
	{
		this.CharaTypeTarget = CharaTypeTarget;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.CharaTypeTarget);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CharaTypeTarget = reader.ReadByte();
	}
}
}
