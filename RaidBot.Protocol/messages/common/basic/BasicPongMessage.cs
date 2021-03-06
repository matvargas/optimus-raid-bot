using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class BasicPongMessage : NetworkMessage
{

	public const uint Id = 183;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Quiet { get; set; }

	public BasicPongMessage() {}


	public BasicPongMessage InitBasicPongMessage(bool Quiet)
	{
		this.Quiet = Quiet;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Quiet);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Quiet = reader.ReadBoolean();
	}
}
}
