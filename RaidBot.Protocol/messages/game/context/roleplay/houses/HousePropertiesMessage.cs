using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HousePropertiesMessage : NetworkMessage
{

	public const uint Id = 5734;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int HouseId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] DoorsOnMap { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public HouseInstanceInformations Properties { get; set; }

	public HousePropertiesMessage() {}


	public HousePropertiesMessage InitHousePropertiesMessage(int HouseId, int[] DoorsOnMap, HouseInstanceInformations Properties)
	{
		this.HouseId = HouseId;
		this.DoorsOnMap = DoorsOnMap;
		this.Properties = Properties;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.HouseId);
		writer.WriteShort(this.DoorsOnMap.Length);
		foreach (int item in this.DoorsOnMap)
		{
			writer.WriteInt(item);
		}
writer.WriteShort(Properties.MessageId);
		Properties.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.HouseId = reader.ReadVarInt();
		int DoorsOnMapLen = reader.ReadShort();
		DoorsOnMap = new int[DoorsOnMapLen];
		for (int i = 0; i < DoorsOnMapLen; i++)
		{
			this.DoorsOnMap[i] = reader.ReadInt();
		}
this.Properties = ProtocolTypeManager.GetInstance<HouseInstanceInformations>(reader.ReadShort());
		this.Properties.Deserialize(reader);
	}
}
}
