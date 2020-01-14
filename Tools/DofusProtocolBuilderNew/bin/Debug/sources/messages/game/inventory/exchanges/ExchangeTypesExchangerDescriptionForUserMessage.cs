using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeTypesExchangerDescriptionForUserMessage : NetworkMessage
{

	public const uint Id = 5765;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] TypeDescription { get; set; }

	public ExchangeTypesExchangerDescriptionForUserMessage() {}


	public ExchangeTypesExchangerDescriptionForUserMessage InitExchangeTypesExchangerDescriptionForUserMessage(int[] TypeDescription)
	{
		this.TypeDescription = TypeDescription;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.TypeDescription.Length);
		foreach (int item in this.TypeDescription)
		{
			writer.WriteVarInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int TypeDescriptionLen = reader.ReadShort();
		TypeDescription = new int[TypeDescriptionLen];
		for (int i = 0; i < TypeDescriptionLen; i++)
		{
			this.TypeDescription[i] = reader.ReadVarInt();
		}
	}
}
}
