using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightResultPvpData : FightResultAdditionalData
{

	public const uint Id = 190;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Grade { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MinHonorForGrade { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MaxHonorForGrade { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Honor { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short HonorDelta { get; set; }

	public FightResultPvpData() {}


	public FightResultPvpData InitFightResultPvpData(byte Grade, short MinHonorForGrade, short MaxHonorForGrade, short Honor, short HonorDelta)
	{
		this.Grade = Grade;
		this.MinHonorForGrade = MinHonorForGrade;
		this.MaxHonorForGrade = MaxHonorForGrade;
		this.Honor = Honor;
		this.HonorDelta = HonorDelta;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.Grade);
		writer.WriteVarShort(this.MinHonorForGrade);
		writer.WriteVarShort(this.MaxHonorForGrade);
		writer.WriteVarShort(this.Honor);
		writer.WriteVarShort(this.HonorDelta);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Grade = reader.ReadByte();
		this.MinHonorForGrade = reader.ReadVarShort();
		this.MaxHonorForGrade = reader.ReadVarShort();
		this.Honor = reader.ReadVarShort();
		this.HonorDelta = reader.ReadVarShort();
	}
}
}
