using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyLeaderUpdateMessage : AbstractPartyEventMessage
{

	public const uint Id = 5578;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long PartyLeaderId { get; set; }

	public PartyLeaderUpdateMessage() {}


	public PartyLeaderUpdateMessage InitPartyLeaderUpdateMessage(long PartyLeaderId)
	{
		this.PartyLeaderId = PartyLeaderId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.PartyLeaderId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.PartyLeaderId = reader.ReadVarLong();
	}
}
}
