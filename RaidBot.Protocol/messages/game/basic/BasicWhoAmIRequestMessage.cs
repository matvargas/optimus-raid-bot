using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class BasicWhoAmIRequestMessage : NetworkMessage
{

	public const uint Id = 5664;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Verbose { get; set; }

	public BasicWhoAmIRequestMessage() {}


	public BasicWhoAmIRequestMessage InitBasicWhoAmIRequestMessage(bool Verbose)
	{
		this.Verbose = Verbose;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Verbose);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Verbose = reader.ReadBoolean();
	}
}
}
