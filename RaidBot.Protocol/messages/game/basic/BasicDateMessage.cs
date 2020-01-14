using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class BasicDateMessage : NetworkMessage
{

	public const uint Id = 177;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Day { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Month { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Year { get; set; }

	public BasicDateMessage() {}


	public BasicDateMessage InitBasicDateMessage(byte Day, byte Month, short Year)
	{
		this.Day = Day;
		this.Month = Month;
		this.Year = Year;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Day);
		writer.WriteByte(this.Month);
		writer.WriteShort(this.Year);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Day = reader.ReadByte();
		this.Month = reader.ReadByte();
		this.Year = reader.ReadShort();
	}
}
}
