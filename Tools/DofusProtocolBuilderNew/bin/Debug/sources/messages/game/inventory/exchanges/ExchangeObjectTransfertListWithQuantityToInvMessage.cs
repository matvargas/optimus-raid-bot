using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeObjectTransfertListWithQuantityToInvMessage : NetworkMessage
{

	public const uint Id = 6470;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] Ids { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] Qtys { get; set; }

	public ExchangeObjectTransfertListWithQuantityToInvMessage() {}


	public ExchangeObjectTransfertListWithQuantityToInvMessage InitExchangeObjectTransfertListWithQuantityToInvMessage(int[] Ids, int[] Qtys)
	{
		this.Ids = Ids;
		this.Qtys = Qtys;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Ids.Length);
		foreach (int item in this.Ids)
		{
			writer.WriteVarInt(item);
		}
		writer.WriteShort(this.Qtys.Length);
		foreach (int item in this.Qtys)
		{
			writer.WriteVarInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int IdsLen = reader.ReadShort();
		Ids = new int[IdsLen];
		for (int i = 0; i < IdsLen; i++)
		{
			this.Ids[i] = reader.ReadVarInt();
		}
		int QtysLen = reader.ReadShort();
		Qtys = new int[QtysLen];
		for (int i = 0; i < QtysLen; i++)
		{
			this.Qtys[i] = reader.ReadVarInt();
		}
	}
}
}
