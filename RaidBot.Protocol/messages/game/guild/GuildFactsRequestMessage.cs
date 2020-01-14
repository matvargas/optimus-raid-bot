using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildFactsRequestMessage : NetworkMessage
{

	public const uint Id = 6404;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int GuildId { get; set; }

	public GuildFactsRequestMessage() {}


	public GuildFactsRequestMessage InitGuildFactsRequestMessage(int GuildId)
	{
		this.GuildId = GuildId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.GuildId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.GuildId = reader.ReadVarInt();
	}
}
}