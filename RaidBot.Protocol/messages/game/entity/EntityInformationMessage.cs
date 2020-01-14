using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class EntityInformationMessage : NetworkMessage
{

	public const uint Id = 6771;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityInformation Entity { get; set; }

	public EntityInformationMessage() {}


	public EntityInformationMessage InitEntityInformationMessage(EntityInformation Entity)
	{
		this.Entity = Entity;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Entity.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Entity = new EntityInformation();
		this.Entity.Deserialize(reader);
	}
}
}
