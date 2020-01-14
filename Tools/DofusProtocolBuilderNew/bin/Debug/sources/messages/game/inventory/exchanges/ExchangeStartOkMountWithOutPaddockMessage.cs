using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeStartOkMountWithOutPaddockMessage : NetworkMessage
{

	public const uint Id = 5991;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public MountClientData[] StabledMountsDescription { get; set; }

	public ExchangeStartOkMountWithOutPaddockMessage() {}


	public ExchangeStartOkMountWithOutPaddockMessage InitExchangeStartOkMountWithOutPaddockMessage(MountClientData[] StabledMountsDescription)
	{
		this.StabledMountsDescription = StabledMountsDescription;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.StabledMountsDescription.Length);
		foreach (MountClientData item in this.StabledMountsDescription)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int StabledMountsDescriptionLen = reader.ReadShort();
		StabledMountsDescription = new MountClientData[StabledMountsDescriptionLen];
		for (int i = 0; i < StabledMountsDescriptionLen; i++)
		{
			this.StabledMountsDescription[i] = new MountClientData();
			this.StabledMountsDescription[i].Deserialize(reader);
		}
	}
}
}
