using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyCancelInvitationNotificationMessage : AbstractPartyEventMessage
{

	public const uint Id = 6251;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long CancelerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long GuestId { get; set; }

	public PartyCancelInvitationNotificationMessage() {}


	public PartyCancelInvitationNotificationMessage InitPartyCancelInvitationNotificationMessage(long CancelerId, long GuestId)
	{
		this.CancelerId = CancelerId;
		this.GuestId = GuestId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.CancelerId);
		writer.WriteVarLong(this.GuestId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.CancelerId = reader.ReadVarLong();
		this.GuestId = reader.ReadVarLong();
	}
}
}
