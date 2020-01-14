using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyPledgeLoyaltyRequestMessage : AbstractPartyMessage
{

	public const uint Id = 6269;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Loyal { get; set; }

	public PartyPledgeLoyaltyRequestMessage() {}


	public PartyPledgeLoyaltyRequestMessage InitPartyPledgeLoyaltyRequestMessage(bool Loyal)
	{
		this.Loyal = Loyal;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(this.Loyal);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Loyal = reader.ReadBoolean();
	}
}
}