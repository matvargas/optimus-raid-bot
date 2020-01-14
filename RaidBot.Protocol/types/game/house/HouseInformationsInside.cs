using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HouseInformationsInside : HouseInformations
{

	public const uint Id = 218;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public HouseInstanceInformations HouseInfos { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldX { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WorldY { get; set; }

	public HouseInformationsInside() {}


	public HouseInformationsInside InitHouseInformationsInside(HouseInstanceInformations HouseInfos, short WorldX, short WorldY)
	{
		this.HouseInfos = HouseInfos;
		this.WorldX = WorldX;
		this.WorldY = WorldY;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
writer.WriteShort(HouseInfos.MessageId);
		HouseInfos.Serialize(writer);
		writer.WriteShort(this.WorldX);
		writer.WriteShort(this.WorldY);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
this.HouseInfos = ProtocolTypeManager.GetInstance<HouseInstanceInformations>(reader.ReadShort());
		this.HouseInfos.Deserialize(reader);
		this.WorldX = reader.ReadShort();
		this.WorldY = reader.ReadShort();
	}
}
}
