using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildInvitationMessage : NetworkMessage
{

	public const uint Id = 5551;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long TargetId { get; set; }

	public GuildInvitationMessage() {}


	public GuildInvitationMessage InitGuildInvitationMessage(long TargetId)
	{
		this.TargetId = TargetId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.TargetId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.TargetId = reader.ReadVarLong();
	}
}
}