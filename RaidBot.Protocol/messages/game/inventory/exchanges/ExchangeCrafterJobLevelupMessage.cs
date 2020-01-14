using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeCrafterJobLevelupMessage : NetworkMessage
{

	public const uint Id = 6598;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte CrafterJobLevel { get; set; }

	public ExchangeCrafterJobLevelupMessage() {}


	public ExchangeCrafterJobLevelupMessage InitExchangeCrafterJobLevelupMessage(byte CrafterJobLevel)
	{
		this.CrafterJobLevel = CrafterJobLevel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.CrafterJobLevel);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CrafterJobLevel = reader.ReadByte();
	}
}
}
