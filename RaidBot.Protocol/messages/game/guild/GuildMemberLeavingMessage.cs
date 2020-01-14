using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildMemberLeavingMessage : NetworkMessage
{

	public const uint Id = 5923;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Kicked { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long MemberId { get; set; }

	public GuildMemberLeavingMessage() {}


	public GuildMemberLeavingMessage InitGuildMemberLeavingMessage(bool Kicked, long MemberId)
	{
		this.Kicked = Kicked;
		this.MemberId = MemberId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Kicked);
		writer.WriteVarLong(this.MemberId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Kicked = reader.ReadBoolean();
		this.MemberId = reader.ReadVarLong();
	}
}
}
