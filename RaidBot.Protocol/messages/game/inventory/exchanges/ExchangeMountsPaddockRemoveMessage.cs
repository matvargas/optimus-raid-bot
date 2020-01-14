using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeMountsPaddockRemoveMessage : NetworkMessage
{

	public const uint Id = 6559;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] MountsId { get; set; }

	public ExchangeMountsPaddockRemoveMessage() {}


	public ExchangeMountsPaddockRemoveMessage InitExchangeMountsPaddockRemoveMessage(int[] MountsId)
	{
		this.MountsId = MountsId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.MountsId.Length);
		foreach (int item in this.MountsId)
		{
			writer.WriteVarInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int MountsIdLen = reader.ReadShort();
		MountsId = new int[MountsIdLen];
		for (int i = 0; i < MountsIdLen; i++)
		{
			this.MountsId[i] = reader.ReadVarInt();
		}
	}
}
}
