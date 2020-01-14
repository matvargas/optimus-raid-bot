using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildInformationsMembersMessage : NetworkMessage
{

	public const uint Id = 5558;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildMember[] Members { get; set; }

	public GuildInformationsMembersMessage() {}


	public GuildInformationsMembersMessage InitGuildInformationsMembersMessage(GuildMember[] Members)
	{
		this.Members = Members;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Members.Length);
		foreach (GuildMember item in this.Members)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int MembersLen = reader.ReadShort();
		Members = new GuildMember[MembersLen];
		for (int i = 0; i < MembersLen; i++)
		{
			this.Members[i] = new GuildMember();
			this.Members[i].Deserialize(reader);
		}
	}
}
}
