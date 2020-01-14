using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class StorageKamasUpdateMessage : NetworkMessage
{

	public const uint Id = 5645;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long KamasTotal { get; set; }

	public StorageKamasUpdateMessage() {}


	public StorageKamasUpdateMessage InitStorageKamasUpdateMessage(long KamasTotal)
	{
		this.KamasTotal = KamasTotal;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.KamasTotal);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.KamasTotal = reader.ReadVarLong();
	}
}
}
