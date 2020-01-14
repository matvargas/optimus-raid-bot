using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyInvitationCancelledForGuestMessage : AbstractPartyMessage
{

	public const uint Id = 6256;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long CancelerId { get; set; }

	public PartyInvitationCancelledForGuestMessage() {}


	public PartyInvitationCancelledForGuestMessage InitPartyInvitationCancelledForGuestMessage(long CancelerId)
	{
		this.CancelerId = CancelerId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.CancelerId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.CancelerId = reader.ReadVarLong();
	}
}
}
