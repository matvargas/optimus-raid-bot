using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildListMessage : NetworkMessage
{

	public const uint Id = 6413;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildInformations[] Guilds { get; set; }

	public GuildListMessage() {}


	public GuildListMessage InitGuildListMessage(GuildInformations[] Guilds)
	{
		this.Guilds = Guilds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Guilds.Length);
		foreach (GuildInformations item in this.Guilds)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int GuildsLen = reader.ReadShort();
		Guilds = new GuildInformations[GuildsLen];
		for (int i = 0; i < GuildsLen; i++)
		{
			this.Guilds[i] = new GuildInformations();
			this.Guilds[i].Deserialize(reader);
		}
	}
}
}
