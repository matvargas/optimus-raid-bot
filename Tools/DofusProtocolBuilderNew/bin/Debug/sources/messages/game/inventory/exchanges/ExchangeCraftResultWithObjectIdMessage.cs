using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeCraftResultWithObjectIdMessage : ExchangeCraftResultMessage
{

	public const uint Id = 6000;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ObjectGenericId { get; set; }

	public ExchangeCraftResultWithObjectIdMessage() {}


	public ExchangeCraftResultWithObjectIdMessage InitExchangeCraftResultWithObjectIdMessage(short ObjectGenericId)
	{
		this.ObjectGenericId = ObjectGenericId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.ObjectGenericId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.ObjectGenericId = reader.ReadVarShort();
	}
}
}
