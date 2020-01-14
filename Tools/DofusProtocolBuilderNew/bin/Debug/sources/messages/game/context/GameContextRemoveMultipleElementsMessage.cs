using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameContextRemoveMultipleElementsMessage : NetworkMessage
{

	public const uint Id = 252;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double[] ElementsIds { get; set; }

	public GameContextRemoveMultipleElementsMessage() {}


	public GameContextRemoveMultipleElementsMessage InitGameContextRemoveMultipleElementsMessage(double[] ElementsIds)
	{
		this.ElementsIds = ElementsIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.ElementsIds.Length);
		foreach (double item in this.ElementsIds)
		{
			writer.WriteDouble(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int ElementsIdsLen = reader.ReadShort();
		ElementsIds = new double[ElementsIdsLen];
		for (int i = 0; i < ElementsIdsLen; i++)
		{
			this.ElementsIds[i] = reader.ReadDouble();
		}
	}
}
}
