using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameFightFighterLightInformations : NetworkType
{

	public const uint Id = 413;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Sex { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Alive { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Wave { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Level { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Breed { get; set; }

	public GameFightFighterLightInformations() {}


	public GameFightFighterLightInformations InitGameFightFighterLightInformations(bool Sex, bool Alive, double Id_, byte Wave, short Level, byte Breed)
	{
		this.Sex = Sex;
		this.Alive = Alive;
		this.Id_ = Id_;
		this.Wave = Wave;
		this.Level = Level;
		this.Breed = Breed;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, Sex);
		box = BooleanByteWrapper.SetFlag(box, 1, Alive);
		writer.WriteByte(box);
		writer.WriteDouble(this.Id_);
		writer.WriteByte(this.Wave);
		writer.WriteVarShort(this.Level);
		writer.WriteByte(this.Breed);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.Sex = BooleanByteWrapper.GetFlag(box, 0);
		this.Alive = BooleanByteWrapper.GetFlag(box, 1);
		this.Id_ = reader.ReadDouble();
		this.Wave = reader.ReadByte();
		this.Level = reader.ReadVarShort();
		this.Breed = reader.ReadByte();
	}
}
}
