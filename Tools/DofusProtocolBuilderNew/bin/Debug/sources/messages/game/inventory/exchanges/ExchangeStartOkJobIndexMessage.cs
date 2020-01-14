using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeStartOkJobIndexMessage : NetworkMessage
{

	public const uint Id = 5819;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] Jobs { get; set; }

	public ExchangeStartOkJobIndexMessage() {}


	public ExchangeStartOkJobIndexMessage InitExchangeStartOkJobIndexMessage(int[] Jobs)
	{
		this.Jobs = Jobs;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Jobs.Length);
		foreach (int item in this.Jobs)
		{
			writer.WriteVarInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int JobsLen = reader.ReadShort();
		Jobs = new int[JobsLen];
		for (int i = 0; i < JobsLen; i++)
		{
			this.Jobs[i] = reader.ReadVarInt();
		}
	}
}
}
