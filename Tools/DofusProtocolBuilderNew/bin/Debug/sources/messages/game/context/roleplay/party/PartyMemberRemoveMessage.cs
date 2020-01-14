using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyMemberRemoveMessage : AbstractPartyEventMessage
{

	public const uint Id = 5579;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long LeavingPlayerId { get; set; }

	public PartyMemberRemoveMessage() {}


	public PartyMemberRemoveMessage InitPartyMemberRemoveMessage(long LeavingPlayerId)
	{
		this.LeavingPlayerId = LeavingPlayerId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.LeavingPlayerId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.LeavingPlayerId = reader.ReadVarLong();
	}
}
}
