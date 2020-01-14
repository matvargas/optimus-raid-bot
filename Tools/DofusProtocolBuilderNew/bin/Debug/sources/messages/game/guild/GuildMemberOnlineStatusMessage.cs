using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildMemberOnlineStatusMessage : NetworkMessage
{

	public const uint Id = 6061;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long MemberId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Online { get; set; }

	public GuildMemberOnlineStatusMessage() {}


	public GuildMemberOnlineStatusMessage InitGuildMemberOnlineStatusMessage(long MemberId, bool Online)
	{
		this.MemberId = MemberId;
		this.Online = Online;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.MemberId);
		writer.WriteBoolean(this.Online);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MemberId = reader.ReadVarLong();
		this.Online = reader.ReadBoolean();
	}
}
}
