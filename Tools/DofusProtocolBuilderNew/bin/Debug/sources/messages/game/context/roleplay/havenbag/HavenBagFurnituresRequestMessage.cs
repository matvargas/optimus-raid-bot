using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HavenBagFurnituresRequestMessage : NetworkMessage
{

	public const uint Id = 6637;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] CellIds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] FunitureIds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] Orientations { get; set; }

	public HavenBagFurnituresRequestMessage() {}


	public HavenBagFurnituresRequestMessage InitHavenBagFurnituresRequestMessage(short[] CellIds, int[] FunitureIds, byte[] Orientations)
	{
		this.CellIds = CellIds;
		this.FunitureIds = FunitureIds;
		this.Orientations = Orientations;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.CellIds.Length);
		foreach (short item in this.CellIds)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.FunitureIds.Length);
		foreach (int item in this.FunitureIds)
		{
			writer.WriteInt(item);
		}
		writer.WriteShort(this.Orientations.Length);
		foreach (byte item in this.Orientations)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int CellIdsLen = reader.ReadShort();
		CellIds = new short[CellIdsLen];
		for (int i = 0; i < CellIdsLen; i++)
		{
			this.CellIds[i] = reader.ReadVarShort();
		}
		int FunitureIdsLen = reader.ReadShort();
		FunitureIds = new int[FunitureIdsLen];
		for (int i = 0; i < FunitureIdsLen; i++)
		{
			this.FunitureIds[i] = reader.ReadInt();
		}
		int OrientationsLen = reader.ReadShort();
		Orientations = new byte[OrientationsLen];
		for (int i = 0; i < OrientationsLen; i++)
		{
			this.Orientations[i] = reader.ReadByte();
		}
	}
}
}
