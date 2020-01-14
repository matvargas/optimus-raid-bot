using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameContextMoveMultipleElementsMessage : NetworkMessage
{

	public const uint Id = 254;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityMovementInformations[] Movements { get; set; }

	public GameContextMoveMultipleElementsMessage() {}


	public GameContextMoveMultipleElementsMessage InitGameContextMoveMultipleElementsMessage(EntityMovementInformations[] Movements)
	{
		this.Movements = Movements;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Movements.Length);
		foreach (EntityMovementInformations item in this.Movements)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int MovementsLen = reader.ReadShort();
		Movements = new EntityMovementInformations[MovementsLen];
		for (int i = 0; i < MovementsLen; i++)
		{
			this.Movements[i] = new EntityMovementInformations();
			this.Movements[i].Deserialize(reader);
		}
	}
}
}
