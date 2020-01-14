using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class KohUpdateMessage : NetworkMessage
{

	public const uint Id = 6439;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AllianceInformations[] Alliances { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] AllianceNbMembers { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] AllianceRoundWeigth { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] AllianceMatchScore { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public BasicAllianceInformations[] AllianceMapWinners { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AllianceMapWinnerScore { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AllianceMapMyAllianceScore { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double NextTickTime { get; set; }

	public KohUpdateMessage() {}


	public KohUpdateMessage InitKohUpdateMessage(AllianceInformations[] Alliances, short[] AllianceNbMembers, int[] AllianceRoundWeigth, byte[] AllianceMatchScore, BasicAllianceInformations[] AllianceMapWinners, int AllianceMapWinnerScore, int AllianceMapMyAllianceScore, double NextTickTime)
	{
		this.Alliances = Alliances;
		this.AllianceNbMembers = AllianceNbMembers;
		this.AllianceRoundWeigth = AllianceRoundWeigth;
		this.AllianceMatchScore = AllianceMatchScore;
		this.AllianceMapWinners = AllianceMapWinners;
		this.AllianceMapWinnerScore = AllianceMapWinnerScore;
		this.AllianceMapMyAllianceScore = AllianceMapMyAllianceScore;
		this.NextTickTime = NextTickTime;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Alliances.Length);
		foreach (AllianceInformations item in this.Alliances)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.AllianceNbMembers.Length);
		foreach (short item in this.AllianceNbMembers)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.AllianceRoundWeigth.Length);
		foreach (int item in this.AllianceRoundWeigth)
		{
			writer.WriteVarInt(item);
		}
		writer.WriteShort(this.AllianceMatchScore.Length);
		foreach (byte item in this.AllianceMatchScore)
		{
			writer.WriteByte(item);
		}
		writer.WriteShort(this.AllianceMapWinners.Length);
		foreach (BasicAllianceInformations item in this.AllianceMapWinners)
		{
			item.Serialize(writer);
		}
		writer.WriteVarInt(this.AllianceMapWinnerScore);
		writer.WriteVarInt(this.AllianceMapMyAllianceScore);
		writer.WriteDouble(this.NextTickTime);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int AlliancesLen = reader.ReadShort();
		Alliances = new AllianceInformations[AlliancesLen];
		for (int i = 0; i < AlliancesLen; i++)
		{
			this.Alliances[i] = new AllianceInformations();
			this.Alliances[i].Deserialize(reader);
		}
		int AllianceNbMembersLen = reader.ReadShort();
		AllianceNbMembers = new short[AllianceNbMembersLen];
		for (int i = 0; i < AllianceNbMembersLen; i++)
		{
			this.AllianceNbMembers[i] = reader.ReadVarShort();
		}
		int AllianceRoundWeigthLen = reader.ReadShort();
		AllianceRoundWeigth = new int[AllianceRoundWeigthLen];
		for (int i = 0; i < AllianceRoundWeigthLen; i++)
		{
			this.AllianceRoundWeigth[i] = reader.ReadVarInt();
		}
		int AllianceMatchScoreLen = reader.ReadShort();
		AllianceMatchScore = new byte[AllianceMatchScoreLen];
		for (int i = 0; i < AllianceMatchScoreLen; i++)
		{
			this.AllianceMatchScore[i] = reader.ReadByte();
		}
		int AllianceMapWinnersLen = reader.ReadShort();
		AllianceMapWinners = new BasicAllianceInformations[AllianceMapWinnersLen];
		for (int i = 0; i < AllianceMapWinnersLen; i++)
		{
			this.AllianceMapWinners[i] = new BasicAllianceInformations();
			this.AllianceMapWinners[i].Deserialize(reader);
		}
		this.AllianceMapWinnerScore = reader.ReadVarInt();
		this.AllianceMapMyAllianceScore = reader.ReadVarInt();
		this.NextTickTime = reader.ReadDouble();
	}
}
}
