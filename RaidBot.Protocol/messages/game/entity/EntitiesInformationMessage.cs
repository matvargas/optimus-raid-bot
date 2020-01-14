using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class EntitiesInformationMessage : NetworkMessage
{

	public const uint Id = 6775;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityInformation[] Entities { get; set; }

	public EntitiesInformationMessage() {}


	public EntitiesInformationMessage InitEntitiesInformationMessage(EntityInformation[] Entities)
	{
		this.Entities = Entities;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Entities.Length);
		foreach (EntityInformation item in this.Entities)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int EntitiesLen = reader.ReadShort();
		Entities = new EntityInformation[EntitiesLen];
		for (int i = 0; i < EntitiesLen; i++)
		{
			this.Entities[i] = new EntityInformation();
			this.Entities[i].Deserialize(reader);
		}
	}
}
}
