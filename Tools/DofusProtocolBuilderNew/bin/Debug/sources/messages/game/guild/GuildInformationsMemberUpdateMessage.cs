using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildInformationsMemberUpdateMessage : NetworkMessage
{

	public const uint Id = 5597;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildMember Member { get; set; }

	public GuildInformationsMemberUpdateMessage() {}


	public GuildInformationsMemberUpdateMessage InitGuildInformationsMemberUpdateMessage(GuildMember Member)
	{
		this.Member = Member;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Member.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Member = new GuildMember();
		this.Member.Deserialize(reader);
	}
}
}
