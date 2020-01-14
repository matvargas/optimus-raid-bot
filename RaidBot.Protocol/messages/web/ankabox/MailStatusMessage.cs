using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MailStatusMessage : NetworkMessage
{

	public const uint Id = 6275;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Unread { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Total { get; set; }

	public MailStatusMessage() {}


	public MailStatusMessage InitMailStatusMessage(short Unread, short Total)
	{
		this.Unread = Unread;
		this.Total = Total;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.Unread);
		writer.WriteVarShort(this.Total);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Unread = reader.ReadVarShort();
		this.Total = reader.ReadVarShort();
	}
}
}
