using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class SetEnablePVPRequestMessage : NetworkMessage
{

	public const uint Id = 1810;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Enable { get; set; }

	public SetEnablePVPRequestMessage() {}


	public SetEnablePVPRequestMessage InitSetEnablePVPRequestMessage(bool Enable)
	{
		this.Enable = Enable;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Enable);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Enable = reader.ReadBoolean();
	}
}
}
