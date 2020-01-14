using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdolPartyLostMessage : NetworkMessage
{

	public const uint Id = 6580;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short IdolId { get; set; }

	public IdolPartyLostMessage() {}


	public IdolPartyLostMessage InitIdolPartyLostMessage(short IdolId)
	{
		this.IdolId = IdolId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.IdolId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.IdolId = reader.ReadVarShort();
	}
}
}
