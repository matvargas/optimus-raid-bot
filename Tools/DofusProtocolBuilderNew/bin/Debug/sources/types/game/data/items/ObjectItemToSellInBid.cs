using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ObjectItemToSellInBid : ObjectItemToSell
{

	public const uint Id = 164;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int UnsoldDelay { get; set; }

	public ObjectItemToSellInBid() {}


	public ObjectItemToSellInBid InitObjectItemToSellInBid(int UnsoldDelay)
	{
		this.UnsoldDelay = UnsoldDelay;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(this.UnsoldDelay);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.UnsoldDelay = reader.ReadInt();
	}
}
}
