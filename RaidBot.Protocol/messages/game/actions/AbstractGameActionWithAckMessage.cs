using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AbstractGameActionWithAckMessage : AbstractGameActionMessage
{

	public const uint Id = 1001;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WaitAckId { get; set; }

	public AbstractGameActionWithAckMessage() {}


	public AbstractGameActionWithAckMessage InitAbstractGameActionWithAckMessage(short WaitAckId)
	{
		this.WaitAckId = WaitAckId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.WaitAckId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.WaitAckId = reader.ReadShort();
	}
}
}
