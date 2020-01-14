using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildInvitedMessage : NetworkMessage
{

	public const uint Id = 5552;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long RecruterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String RecruterName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public BasicGuildInformations GuildInfo { get; set; }

	public GuildInvitedMessage() {}


	public GuildInvitedMessage InitGuildInvitedMessage(long RecruterId, String RecruterName, BasicGuildInformations GuildInfo)
	{
		this.RecruterId = RecruterId;
		this.RecruterName = RecruterName;
		this.GuildInfo = GuildInfo;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.RecruterId);
		writer.WriteUTF(this.RecruterName);
		this.GuildInfo.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.RecruterId = reader.ReadVarLong();
		this.RecruterName = reader.ReadUTF();
		this.GuildInfo = new BasicGuildInformations();
		this.GuildInfo.Deserialize(reader);
	}
}
}
