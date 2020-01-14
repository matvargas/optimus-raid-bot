using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ObjectEffectDate : ObjectEffect
{

	public const uint Id = 72;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Year { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Month { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Day { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Hour { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Minute { get; set; }

	public ObjectEffectDate() {}


	public ObjectEffectDate InitObjectEffectDate(short Year, byte Month, byte Day, byte Hour, byte Minute)
	{
		this.Year = Year;
		this.Month = Month;
		this.Day = Day;
		this.Hour = Hour;
		this.Minute = Minute;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.Year);
		writer.WriteByte(this.Month);
		writer.WriteByte(this.Day);
		writer.WriteByte(this.Hour);
		writer.WriteByte(this.Minute);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Year = reader.ReadVarShort();
		this.Month = reader.ReadByte();
		this.Day = reader.ReadByte();
		this.Hour = reader.ReadByte();
		this.Minute = reader.ReadByte();
	}
}
}
