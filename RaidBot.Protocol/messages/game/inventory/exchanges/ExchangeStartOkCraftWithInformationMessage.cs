using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeStartOkCraftWithInformationMessage : ExchangeStartOkCraftMessage
{

	public const uint Id = 5941;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SkillId { get; set; }

	public ExchangeStartOkCraftWithInformationMessage() {}


	public ExchangeStartOkCraftWithInformationMessage InitExchangeStartOkCraftWithInformationMessage(int SkillId)
	{
		this.SkillId = SkillId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.SkillId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.SkillId = reader.ReadVarInt();
	}
}
}
