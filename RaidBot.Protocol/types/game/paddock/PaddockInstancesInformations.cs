using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PaddockInstancesInformations : PaddockInformations
{

	public const uint Id = 509;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PaddockBuyableInformations[] Paddocks { get; set; }

	public PaddockInstancesInformations() {}


	public PaddockInstancesInformations InitPaddockInstancesInformations(PaddockBuyableInformations[] Paddocks)
	{
		this.Paddocks = Paddocks;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Paddocks.Length);
		foreach (PaddockBuyableInformations item in this.Paddocks)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int PaddocksLen = reader.ReadShort();
		Paddocks = new PaddockBuyableInformations[PaddocksLen];
		for (int i = 0; i < PaddocksLen; i++)
		{
			this.Paddocks[i] = ProtocolTypeManager.GetInstance<PaddockBuyableInformations>(reader.ReadShort());
			this.Paddocks[i].Deserialize(reader);
		}
	}
}
}
