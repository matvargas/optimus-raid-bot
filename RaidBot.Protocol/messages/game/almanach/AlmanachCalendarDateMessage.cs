using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AlmanachCalendarDateMessage : NetworkMessage
{

	public const uint Id = 6341;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Date { get; set; }

	public AlmanachCalendarDateMessage() {}


	public AlmanachCalendarDateMessage InitAlmanachCalendarDateMessage(int Date)
	{
		this.Date = Date;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.Date);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Date = reader.ReadInt();
	}
}
}
