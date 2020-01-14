using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AbstractPartyMessage : NetworkMessage
{

	public const uint Id = 6274;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int PartyId { get; set; }

	public AbstractPartyMessage() {}


	public AbstractPartyMessage InitAbstractPartyMessage(int PartyId)
	{
		this.PartyId = PartyId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.PartyId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PartyId = reader.ReadVarInt();
	}
}
}
