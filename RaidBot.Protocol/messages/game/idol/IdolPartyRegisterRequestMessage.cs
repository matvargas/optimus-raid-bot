using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdolPartyRegisterRequestMessage : NetworkMessage
{

	public const uint Id = 6582;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Register { get; set; }

	public IdolPartyRegisterRequestMessage() {}


	public IdolPartyRegisterRequestMessage InitIdolPartyRegisterRequestMessage(bool Register)
	{
		this.Register = Register;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Register);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Register = reader.ReadBoolean();
	}
}
}
