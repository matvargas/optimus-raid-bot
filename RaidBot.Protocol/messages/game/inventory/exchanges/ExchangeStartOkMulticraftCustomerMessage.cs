using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeStartOkMulticraftCustomerMessage : NetworkMessage
{

	public const uint Id = 5817;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SkillId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte CrafterJobLevel { get; set; }

	public ExchangeStartOkMulticraftCustomerMessage() {}


	public ExchangeStartOkMulticraftCustomerMessage InitExchangeStartOkMulticraftCustomerMessage(int SkillId, byte CrafterJobLevel)
	{
		this.SkillId = SkillId;
		this.CrafterJobLevel = CrafterJobLevel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.SkillId);
		writer.WriteByte(this.CrafterJobLevel);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SkillId = reader.ReadVarInt();
		this.CrafterJobLevel = reader.ReadByte();
	}
}
}
