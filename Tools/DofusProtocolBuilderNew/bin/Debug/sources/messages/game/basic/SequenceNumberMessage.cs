using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class SequenceNumberMessage : NetworkMessage
{

	public const uint Id = 6317;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Number { get; set; }

	public SequenceNumberMessage() {}


	public SequenceNumberMessage InitSequenceNumberMessage(short Number)
	{
		this.Number = Number;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Number);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Number = reader.ReadShort();
	}
}
}
