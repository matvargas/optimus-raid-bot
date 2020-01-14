using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdolPartyRefreshMessage : NetworkMessage
{

	public const uint Id = 6583;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PartyIdol PartyIdol { get; set; }

	public IdolPartyRefreshMessage() {}


	public IdolPartyRefreshMessage InitIdolPartyRefreshMessage(PartyIdol PartyIdol)
	{
		this.PartyIdol = PartyIdol;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.PartyIdol.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PartyIdol = new PartyIdol();
		this.PartyIdol.Deserialize(reader);
	}
}
}
