using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GoldAddedMessage : NetworkMessage
{

	public const uint Id = 6030;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GoldItem Gold { get; set; }

	public GoldAddedMessage() {}


	public GoldAddedMessage InitGoldAddedMessage(GoldItem Gold)
	{
		this.Gold = Gold;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Gold.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Gold = new GoldItem();
		this.Gold.Deserialize(reader);
	}
}
}
