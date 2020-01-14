using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeStartedWithStorageMessage : ExchangeStartedMessage
{

	public const uint Id = 6236;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int StorageMaxSlot { get; set; }

	public ExchangeStartedWithStorageMessage() {}


	public ExchangeStartedWithStorageMessage InitExchangeStartedWithStorageMessage(int StorageMaxSlot)
	{
		this.StorageMaxSlot = StorageMaxSlot;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.StorageMaxSlot);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.StorageMaxSlot = reader.ReadVarInt();
	}
}
}
