using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ObtainedItemWithBonusMessage : ObtainedItemMessage
{

	public const uint Id = 6520;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int BonusQuantity { get; set; }

	public ObtainedItemWithBonusMessage() {}


	public ObtainedItemWithBonusMessage InitObtainedItemWithBonusMessage(int BonusQuantity)
	{
		this.BonusQuantity = BonusQuantity;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.BonusQuantity);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.BonusQuantity = reader.ReadVarInt();
	}
}
}
