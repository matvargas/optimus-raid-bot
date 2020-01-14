using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildInvitationStateRecrutedMessage : NetworkMessage
{

	public const uint Id = 5548;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte InvitationState { get; set; }

	public GuildInvitationStateRecrutedMessage() {}


	public GuildInvitationStateRecrutedMessage InitGuildInvitationStateRecrutedMessage(byte InvitationState)
	{
		this.InvitationState = InvitationState;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.InvitationState);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.InvitationState = reader.ReadByte();
	}
}
}
