using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class NotificationByServerMessage : NetworkMessage
{

	public const uint Id = 6103;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String[] Parameters { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool ForceOpen { get; set; }

	public NotificationByServerMessage() {}


	public NotificationByServerMessage InitNotificationByServerMessage(short Id_, String[] Parameters, bool ForceOpen)
	{
		this.Id_ = Id_;
		this.Parameters = Parameters;
		this.ForceOpen = ForceOpen;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.Id_);
		writer.WriteShort(this.Parameters.Length);
		foreach (String item in this.Parameters)
		{
			writer.WriteUTF(item);
		}
		writer.WriteBoolean(this.ForceOpen);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Id_ = reader.ReadVarShort();
		int ParametersLen = reader.ReadShort();
		Parameters = new String[ParametersLen];
		for (int i = 0; i < ParametersLen; i++)
		{
			this.Parameters[i] = reader.ReadUTF();
		}
		this.ForceOpen = reader.ReadBoolean();
	}
}
}
