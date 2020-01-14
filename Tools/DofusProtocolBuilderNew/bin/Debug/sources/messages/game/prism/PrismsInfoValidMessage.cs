using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PrismsInfoValidMessage : NetworkMessage
{

	public const uint Id = 6451;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PrismFightersInformation[] Fights { get; set; }

	public PrismsInfoValidMessage() {}


	public PrismsInfoValidMessage InitPrismsInfoValidMessage(PrismFightersInformation[] Fights)
	{
		this.Fights = Fights;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Fights.Length);
		foreach (PrismFightersInformation item in this.Fights)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int FightsLen = reader.ReadShort();
		Fights = new PrismFightersInformation[FightsLen];
		for (int i = 0; i < FightsLen; i++)
		{
			this.Fights[i] = new PrismFightersInformation();
			this.Fights[i].Deserialize(reader);
		}
	}
}
}
