using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HouseSellRequestMessage : NetworkMessage
{

	public const uint Id = 5697;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int InstanceId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Amount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool ForSale { get; set; }

	public HouseSellRequestMessage() {}


	public HouseSellRequestMessage InitHouseSellRequestMessage(int InstanceId, long Amount, bool ForSale)
	{
		this.InstanceId = InstanceId;
		this.Amount = Amount;
		this.ForSale = ForSale;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.InstanceId);
		writer.WriteVarLong(this.Amount);
		writer.WriteBoolean(this.ForSale);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.InstanceId = reader.ReadInt();
		this.Amount = reader.ReadVarLong();
		this.ForSale = reader.ReadBoolean();
	}
}
}
