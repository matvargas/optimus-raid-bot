using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeMountsPaddockAddMessage : NetworkMessage
{

	public const uint Id = 6561;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public MountClientData[] MountDescription { get; set; }

	public ExchangeMountsPaddockAddMessage() {}


	public ExchangeMountsPaddockAddMessage InitExchangeMountsPaddockAddMessage(MountClientData[] MountDescription)
	{
		this.MountDescription = MountDescription;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.MountDescription.Length);
		foreach (MountClientData item in this.MountDescription)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int MountDescriptionLen = reader.ReadShort();
		MountDescription = new MountClientData[MountDescriptionLen];
		for (int i = 0; i < MountDescriptionLen; i++)
		{
			this.MountDescription[i] = new MountClientData();
			this.MountDescription[i].Deserialize(reader);
		}
	}
}
}
