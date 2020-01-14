using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PaddockPropertiesMessage : NetworkMessage
{

	public const uint Id = 5824;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PaddockInstancesInformations Properties { get; set; }

	public PaddockPropertiesMessage() {}


	public PaddockPropertiesMessage InitPaddockPropertiesMessage(PaddockInstancesInformations Properties)
	{
		this.Properties = Properties;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Properties.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Properties = new PaddockInstancesInformations();
		this.Properties.Deserialize(reader);
	}
}
}
