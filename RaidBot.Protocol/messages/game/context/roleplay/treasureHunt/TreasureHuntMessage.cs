using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TreasureHuntMessage : NetworkMessage
{

	public const uint Id = 6486;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte QuestType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double StartMapId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public TreasureHuntStep[] KnownStepsList { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TotalStepCount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int CheckPointCurrent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int CheckPointTotal { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AvailableRetryCount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public TreasureHuntFlag[] Flags { get; set; }

	public TreasureHuntMessage() {}


	public TreasureHuntMessage InitTreasureHuntMessage(byte QuestType, double StartMapId, TreasureHuntStep[] KnownStepsList, byte TotalStepCount, int CheckPointCurrent, int CheckPointTotal, int AvailableRetryCount, TreasureHuntFlag[] Flags)
	{
		this.QuestType = QuestType;
		this.StartMapId = StartMapId;
		this.KnownStepsList = KnownStepsList;
		this.TotalStepCount = TotalStepCount;
		this.CheckPointCurrent = CheckPointCurrent;
		this.CheckPointTotal = CheckPointTotal;
		this.AvailableRetryCount = AvailableRetryCount;
		this.Flags = Flags;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.QuestType);
		writer.WriteDouble(this.StartMapId);
		writer.WriteShort(this.KnownStepsList.Length);
		foreach (TreasureHuntStep item in this.KnownStepsList)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteByte(this.TotalStepCount);
		writer.WriteVarInt(this.CheckPointCurrent);
		writer.WriteVarInt(this.CheckPointTotal);
		writer.WriteInt(this.AvailableRetryCount);
		writer.WriteShort(this.Flags.Length);
		foreach (TreasureHuntFlag item in this.Flags)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.QuestType = reader.ReadByte();
		this.StartMapId = reader.ReadDouble();
		int KnownStepsListLen = reader.ReadShort();
		KnownStepsList = new TreasureHuntStep[KnownStepsListLen];
		for (int i = 0; i < KnownStepsListLen; i++)
		{
			this.KnownStepsList[i] = ProtocolTypeManager.GetInstance<TreasureHuntStep>(reader.ReadShort());
			this.KnownStepsList[i].Deserialize(reader);
		}
		this.TotalStepCount = reader.ReadByte();
		this.CheckPointCurrent = reader.ReadVarInt();
		this.CheckPointTotal = reader.ReadVarInt();
		this.AvailableRetryCount = reader.ReadInt();
		int FlagsLen = reader.ReadShort();
		Flags = new TreasureHuntFlag[FlagsLen];
		for (int i = 0; i < FlagsLen; i++)
		{
			this.Flags[i] = new TreasureHuntFlag();
			this.Flags[i].Deserialize(reader);
		}
	}
}
}
