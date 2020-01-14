using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildVersatileInfoListMessage : NetworkMessage
{

	public const uint Id = 6435;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildVersatileInformations[] Guilds { get; set; }

	public GuildVersatileInfoListMessage() {}


	public GuildVersatileInfoListMessage InitGuildVersatileInfoListMessage(GuildVersatileInformations[] Guilds)
	{
		this.Guilds = Guilds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Guilds.Length);
		foreach (GuildVersatileInformations item in this.Guilds)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int GuildsLen = reader.ReadShort();
		Guilds = new GuildVersatileInformations[GuildsLen];
		for (int i = 0; i < GuildsLen; i++)
		{
			this.Guilds[i] = ProtocolTypeManager.GetInstance<GuildVersatileInformations>(reader.ReadShort());
			this.Guilds[i].Deserialize(reader);
		}
	}
}
}
