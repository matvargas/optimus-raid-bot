using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AllianceInvitationStateRecrutedMessage : NetworkMessage
{

	public const uint Id = 6392;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte InvitationState { get; set; }

	public AllianceInvitationStateRecrutedMessage() {}


	public AllianceInvitationStateRecrutedMessage InitAllianceInvitationStateRecrutedMessage(byte InvitationState)
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
