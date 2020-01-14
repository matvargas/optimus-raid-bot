using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HouseBuyRequestMessage : NetworkMessage
{

	public const uint Id = 5738;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long ProposedPrice { get; set; }

	public HouseBuyRequestMessage() {}


	public HouseBuyRequestMessage InitHouseBuyRequestMessage(long ProposedPrice)
	{
		this.ProposedPrice = ProposedPrice;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.ProposedPrice);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ProposedPrice = reader.ReadVarLong();
	}
}
}
