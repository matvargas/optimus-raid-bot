using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyCannotJoinErrorMessage : AbstractPartyMessage
{

	public const uint Id = 5583;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Reason { get; set; }

	public PartyCannotJoinErrorMessage() {}


	public PartyCannotJoinErrorMessage InitPartyCannotJoinErrorMessage(byte Reason)
	{
		this.Reason = Reason;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.Reason);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Reason = reader.ReadByte();
	}
}
}
