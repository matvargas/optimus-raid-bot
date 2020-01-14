using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HavenBagFurnituresMessage : NetworkMessage
{

	public const uint Id = 6634;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public HavenBagFurnitureInformation[] FurnituresInfos { get; set; }

	public HavenBagFurnituresMessage() {}


	public HavenBagFurnituresMessage InitHavenBagFurnituresMessage(HavenBagFurnitureInformation[] FurnituresInfos)
	{
		this.FurnituresInfos = FurnituresInfos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.FurnituresInfos.Length);
		foreach (HavenBagFurnitureInformation item in this.FurnituresInfos)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int FurnituresInfosLen = reader.ReadShort();
		FurnituresInfos = new HavenBagFurnitureInformation[FurnituresInfosLen];
		for (int i = 0; i < FurnituresInfosLen; i++)
		{
			this.FurnituresInfos[i] = new HavenBagFurnitureInformation();
			this.FurnituresInfos[i].Deserialize(reader);
		}
	}
}
}
