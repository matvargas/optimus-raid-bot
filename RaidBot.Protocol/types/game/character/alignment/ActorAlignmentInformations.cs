using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ActorAlignmentInformations : NetworkType
{

	public const uint Id = 201;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AlignmentSide { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AlignmentValue { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte AlignmentGrade { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double CharacterPower { get; set; }

	public ActorAlignmentInformations() {}


	public ActorAlignmentInformations InitActorAlignmentInformations(byte AlignmentSide, byte AlignmentValue, byte AlignmentGrade, double CharacterPower)
	{
		this.AlignmentSide = AlignmentSide;
		this.AlignmentValue = AlignmentValue;
		this.AlignmentGrade = AlignmentGrade;
		this.CharacterPower = CharacterPower;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.AlignmentSide);
		writer.WriteByte(this.AlignmentValue);
		writer.WriteByte(this.AlignmentGrade);
		writer.WriteDouble(this.CharacterPower);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AlignmentSide = reader.ReadByte();
		this.AlignmentValue = reader.ReadByte();
		this.AlignmentGrade = reader.ReadByte();
		this.CharacterPower = reader.ReadDouble();
	}
}
}
