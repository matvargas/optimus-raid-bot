using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AccountLoggingKickedMessage : NetworkMessage
{

	public const uint Id = 6029;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Days { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Hours { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Minutes { get; set; }

	public AccountLoggingKickedMessage() {}


	public AccountLoggingKickedMessage InitAccountLoggingKickedMessage(short Days, byte Hours, byte Minutes)
	{
		this.Days = Days;
		this.Hours = Hours;
		this.Minutes = Minutes;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.Days);
		writer.WriteByte(this.Hours);
		writer.WriteByte(this.Minutes);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Days = reader.ReadVarShort();
		this.Hours = reader.ReadByte();
		this.Minutes = reader.ReadByte();
	}
}
}
