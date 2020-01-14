using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeStartOkMountMessage : ExchangeStartOkMountWithOutPaddockMessage
{

	public const uint Id = 5979;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public MountClientData[] PaddockedMountsDescription { get; set; }

	public ExchangeStartOkMountMessage() {}


	public ExchangeStartOkMountMessage InitExchangeStartOkMountMessage(MountClientData[] PaddockedMountsDescription)
	{
		this.PaddockedMountsDescription = PaddockedMountsDescription;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.PaddockedMountsDescription.Length);
		foreach (MountClientData item in this.PaddockedMountsDescription)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int PaddockedMountsDescriptionLen = reader.ReadShort();
		PaddockedMountsDescription = new MountClientData[PaddockedMountsDescriptionLen];
		for (int i = 0; i < PaddockedMountsDescriptionLen; i++)
		{
			this.PaddockedMountsDescription[i] = new MountClientData();
			this.PaddockedMountsDescription[i].Deserialize(reader);
		}
	}
}
}
