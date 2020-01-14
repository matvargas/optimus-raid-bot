using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildHousesInformationMessage : NetworkMessage
{

	public const uint Id = 5919;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public HouseInformationsForGuild[] HousesInformations { get; set; }

	public GuildHousesInformationMessage() {}


	public GuildHousesInformationMessage InitGuildHousesInformationMessage(HouseInformationsForGuild[] HousesInformations)
	{
		this.HousesInformations = HousesInformations;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.HousesInformations.Length);
		foreach (HouseInformationsForGuild item in this.HousesInformations)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int HousesInformationsLen = reader.ReadShort();
		HousesInformations = new HouseInformationsForGuild[HousesInformationsLen];
		for (int i = 0; i < HousesInformationsLen; i++)
		{
			this.HousesInformations[i] = new HouseInformationsForGuild();
			this.HousesInformations[i].Deserialize(reader);
		}
	}
}
}
