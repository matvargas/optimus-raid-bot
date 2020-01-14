using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AllianceChangeGuildRightsMessage : NetworkMessage
{

	public const uint Id = 6426;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int GuildId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Rights { get; set; }

	public AllianceChangeGuildRightsMessage() {}


	public AllianceChangeGuildRightsMessage InitAllianceChangeGuildRightsMessage(int GuildId, byte Rights)
	{
		this.GuildId = GuildId;
		this.Rights = Rights;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.GuildId);
		writer.WriteByte(this.Rights);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.GuildId = reader.ReadVarInt();
		this.Rights = reader.ReadByte();
	}
}
}
