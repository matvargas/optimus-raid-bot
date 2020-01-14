using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MapComplementaryInformationsDataInHouseMessage : MapComplementaryInformationsDataMessage
{

	public const uint Id = 6130;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public HouseInformationsInside CurrentHouse { get; set; }

	public MapComplementaryInformationsDataInHouseMessage() {}


	public MapComplementaryInformationsDataInHouseMessage InitMapComplementaryInformationsDataInHouseMessage(HouseInformationsInside CurrentHouse)
	{
		this.CurrentHouse = CurrentHouse;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.CurrentHouse.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.CurrentHouse = new HouseInformationsInside();
		this.CurrentHouse.Deserialize(reader);
	}
}
}
