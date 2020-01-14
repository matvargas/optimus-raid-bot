using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyModifiableStatusMessage : AbstractPartyMessage
{

	public const uint Id = 6277;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Enabled { get; set; }

	public PartyModifiableStatusMessage() {}


	public PartyModifiableStatusMessage InitPartyModifiableStatusMessage(bool Enabled)
	{
		this.Enabled = Enabled;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(this.Enabled);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Enabled = reader.ReadBoolean();
	}
}
}
