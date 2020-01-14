using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyRestrictedMessage : AbstractPartyMessage
{

	public const uint Id = 6175;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Restricted { get; set; }

	public PartyRestrictedMessage() {}


	public PartyRestrictedMessage InitPartyRestrictedMessage(bool Restricted)
	{
		this.Restricted = Restricted;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(this.Restricted);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Restricted = reader.ReadBoolean();
	}
}
}
