using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class DareInformations : NetworkType
{

	public const uint Id = 502;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double DareId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterBasicMinimalInformations Creator { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long SubscriptionFee { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long Jackpot { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MaxCountWinners { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double EndDate { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsPrivate { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int GuildId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AllianceId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DareCriteria[] Criterions { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double StartDate { get; set; }

	public DareInformations() {}


	public DareInformations InitDareInformations(double DareId, CharacterBasicMinimalInformations Creator, long SubscriptionFee, long Jackpot, short MaxCountWinners, double EndDate, bool IsPrivate, int GuildId, int AllianceId, DareCriteria[] Criterions, double StartDate)
	{
		this.DareId = DareId;
		this.Creator = Creator;
		this.SubscriptionFee = SubscriptionFee;
		this.Jackpot = Jackpot;
		this.MaxCountWinners = MaxCountWinners;
		this.EndDate = EndDate;
		this.IsPrivate = IsPrivate;
		this.GuildId = GuildId;
		this.AllianceId = AllianceId;
		this.Criterions = Criterions;
		this.StartDate = StartDate;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.DareId);
		this.Creator.Serialize(writer);
		writer.WriteVarLong(this.SubscriptionFee);
		writer.WriteVarLong(this.Jackpot);
		writer.WriteShort(this.MaxCountWinners);
		writer.WriteDouble(this.EndDate);
		writer.WriteBoolean(this.IsPrivate);
		writer.WriteVarInt(this.GuildId);
		writer.WriteVarInt(this.AllianceId);
		writer.WriteShort(this.Criterions.Length);
		foreach (DareCriteria item in this.Criterions)
		{
			item.Serialize(writer);
		}
		writer.WriteDouble(this.StartDate);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DareId = reader.ReadDouble();
		this.Creator = new CharacterBasicMinimalInformations();
		this.Creator.Deserialize(reader);
		this.SubscriptionFee = reader.ReadVarLong();
		this.Jackpot = reader.ReadVarLong();
		this.MaxCountWinners = reader.ReadShort();
		this.EndDate = reader.ReadDouble();
		this.IsPrivate = reader.ReadBoolean();
		this.GuildId = reader.ReadVarInt();
		this.AllianceId = reader.ReadVarInt();
		int CriterionsLen = reader.ReadShort();
		Criterions = new DareCriteria[CriterionsLen];
		for (int i = 0; i < CriterionsLen; i++)
		{
			this.Criterions[i] = new DareCriteria();
			this.Criterions[i].Deserialize(reader);
		}
		this.StartDate = reader.ReadDouble();
	}
}
}
