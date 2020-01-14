using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeBidHouseGenericItemAddedMessage : NetworkMessage
{

	public const uint Id = 5947;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ObjGenericId { get; set; }

	public ExchangeBidHouseGenericItemAddedMessage() {}


	public ExchangeBidHouseGenericItemAddedMessage InitExchangeBidHouseGenericItemAddedMessage(short ObjGenericId)
	{
		this.ObjGenericId = ObjGenericId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.ObjGenericId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ObjGenericId = reader.ReadVarShort();
	}
}
}
