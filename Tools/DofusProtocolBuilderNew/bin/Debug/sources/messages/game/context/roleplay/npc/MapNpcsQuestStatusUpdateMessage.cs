using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MapNpcsQuestStatusUpdateMessage : NetworkMessage
{

	public const uint Id = 5642;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MapId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] NpcsIdsWithQuest { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameRolePlayNpcQuestFlag[] QuestFlags { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] NpcsIdsWithoutQuest { get; set; }

	public MapNpcsQuestStatusUpdateMessage() {}


	public MapNpcsQuestStatusUpdateMessage InitMapNpcsQuestStatusUpdateMessage(double MapId, int[] NpcsIdsWithQuest, GameRolePlayNpcQuestFlag[] QuestFlags, int[] NpcsIdsWithoutQuest)
	{
		this.MapId = MapId;
		this.NpcsIdsWithQuest = NpcsIdsWithQuest;
		this.QuestFlags = QuestFlags;
		this.NpcsIdsWithoutQuest = NpcsIdsWithoutQuest;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.MapId);
		writer.WriteShort(this.NpcsIdsWithQuest.Length);
		foreach (int item in this.NpcsIdsWithQuest)
		{
			writer.WriteInt(item);
		}
		writer.WriteShort(this.QuestFlags.Length);
		foreach (GameRolePlayNpcQuestFlag item in this.QuestFlags)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.NpcsIdsWithoutQuest.Length);
		foreach (int item in this.NpcsIdsWithoutQuest)
		{
			writer.WriteInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MapId = reader.ReadDouble();
		int NpcsIdsWithQuestLen = reader.ReadShort();
		NpcsIdsWithQuest = new int[NpcsIdsWithQuestLen];
		for (int i = 0; i < NpcsIdsWithQuestLen; i++)
		{
			this.NpcsIdsWithQuest[i] = reader.ReadInt();
		}
		int QuestFlagsLen = reader.ReadShort();
		QuestFlags = new GameRolePlayNpcQuestFlag[QuestFlagsLen];
		for (int i = 0; i < QuestFlagsLen; i++)
		{
			this.QuestFlags[i] = new GameRolePlayNpcQuestFlag();
			this.QuestFlags[i].Deserialize(reader);
		}
		int NpcsIdsWithoutQuestLen = reader.ReadShort();
		NpcsIdsWithoutQuest = new int[NpcsIdsWithoutQuestLen];
		for (int i = 0; i < NpcsIdsWithoutQuestLen; i++)
		{
			this.NpcsIdsWithoutQuest[i] = reader.ReadInt();
		}
	}
}
}
