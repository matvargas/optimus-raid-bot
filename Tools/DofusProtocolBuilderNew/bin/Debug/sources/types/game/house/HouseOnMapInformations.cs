using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HouseOnMapInformations : HouseInformations
{

	public const uint Id = 510;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] DoorsOnMap { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public HouseInstanceInformations[] HouseInstances { get; set; }

	public HouseOnMapInformations() {}


	public HouseOnMapInformations InitHouseOnMapInformations(int[] DoorsOnMap, HouseInstanceInformations[] HouseInstances)
	{
		this.DoorsOnMap = DoorsOnMap;
		this.HouseInstances = HouseInstances;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.DoorsOnMap.Length);
		foreach (int item in this.DoorsOnMap)
		{
			writer.WriteInt(item);
		}
		writer.WriteShort(this.HouseInstances.Length);
		foreach (HouseInstanceInformations item in this.HouseInstances)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int DoorsOnMapLen = reader.ReadShort();
		DoorsOnMap = new int[DoorsOnMapLen];
		for (int i = 0; i < DoorsOnMapLen; i++)
		{
			this.DoorsOnMap[i] = reader.ReadInt();
		}
		int HouseInstancesLen = reader.ReadShort();
		HouseInstances = new HouseInstanceInformations[HouseInstancesLen];
		for (int i = 0; i < HouseInstancesLen; i++)
		{
			this.HouseInstances[i] = new HouseInstanceInformations();
			this.HouseInstances[i].Deserialize(reader);
		}
	}
}
}
