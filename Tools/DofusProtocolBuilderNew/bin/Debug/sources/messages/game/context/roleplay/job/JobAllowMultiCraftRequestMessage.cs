using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class JobAllowMultiCraftRequestMessage : NetworkMessage
{

	public const uint Id = 5748;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Enabled { get; set; }

	public JobAllowMultiCraftRequestMessage() {}


	public JobAllowMultiCraftRequestMessage InitJobAllowMultiCraftRequestMessage(bool Enabled)
	{
		this.Enabled = Enabled;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Enabled);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Enabled = reader.ReadBoolean();
	}
}
}
