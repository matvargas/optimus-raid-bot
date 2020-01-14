using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class NewMailMessage : MailStatusMessage
{

	public const uint Id = 6292;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int[] SendersAccountId { get; set; }

	public NewMailMessage() {}


	public NewMailMessage InitNewMailMessage(int[] SendersAccountId)
	{
		this.SendersAccountId = SendersAccountId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.SendersAccountId.Length);
		foreach (int item in this.SendersAccountId)
		{
			writer.WriteInt(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int SendersAccountIdLen = reader.ReadShort();
		SendersAccountId = new int[SendersAccountIdLen];
		for (int i = 0; i < SendersAccountIdLen; i++)
		{
			this.SendersAccountId[i] = reader.ReadInt();
		}
	}
}
}
