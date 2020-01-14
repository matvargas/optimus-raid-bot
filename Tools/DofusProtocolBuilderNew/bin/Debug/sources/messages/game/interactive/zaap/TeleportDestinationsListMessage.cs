using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TeleportDestinationsListMessage : NetworkMessage
{

	public const uint Id = 5960;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TeleporterType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double[] MapIds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] SubAreaIds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Costs { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] DestTeleporterType { get; set; }

	public TeleportDestinationsListMessage() {}


	public TeleportDestinationsListMessage InitTeleportDestinationsListMessage(byte TeleporterType, double[] MapIds, short[] SubAreaIds, short[] Costs, byte[] DestTeleporterType)
	{
		this.TeleporterType = TeleporterType;
		this.MapIds = MapIds;
		this.SubAreaIds = SubAreaIds;
		this.Costs = Costs;
		this.DestTeleporterType = DestTeleporterType;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.TeleporterType);
		writer.WriteShort(this.MapIds.Length);
		foreach (double item in this.MapIds)
		{
			writer.WriteDouble(item);
		}
		writer.WriteShort(this.SubAreaIds.Length);
		foreach (short item in this.SubAreaIds)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.Costs.Length);
		foreach (short item in this.Costs)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.DestTeleporterType.Length);
		foreach (byte item in this.DestTeleporterType)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.TeleporterType = reader.ReadByte();
		int MapIdsLen = reader.ReadShort();
		MapIds = new double[MapIdsLen];
		for (int i = 0; i < MapIdsLen; i++)
		{
			this.MapIds[i] = reader.ReadDouble();
		}
		int SubAreaIdsLen = reader.ReadShort();
		SubAreaIds = new short[SubAreaIdsLen];
		for (int i = 0; i < SubAreaIdsLen; i++)
		{
			this.SubAreaIds[i] = reader.ReadVarShort();
		}
		int CostsLen = reader.ReadShort();
		Costs = new short[CostsLen];
		for (int i = 0; i < CostsLen; i++)
		{
			this.Costs[i] = reader.ReadVarShort();
		}
		int DestTeleporterTypeLen = reader.ReadShort();
		DestTeleporterType = new byte[DestTeleporterTypeLen];
		for (int i = 0; i < DestTeleporterTypeLen; i++)
		{
			this.DestTeleporterType[i] = reader.ReadByte();
		}
	}
}
}
