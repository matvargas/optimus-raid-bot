using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class StatedMapUpdateMessage : NetworkMessage
{

	public const uint Id = 5716;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public StatedElement[] StatedElements { get; set; }

	public StatedMapUpdateMessage() {}


	public StatedMapUpdateMessage InitStatedMapUpdateMessage(StatedElement[] StatedElements)
	{
		this.StatedElements = StatedElements;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.StatedElements.Length);
		foreach (StatedElement item in this.StatedElements)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int StatedElementsLen = reader.ReadShort();
		StatedElements = new StatedElement[StatedElementsLen];
		for (int i = 0; i < StatedElementsLen; i++)
		{
			this.StatedElements[i] = new StatedElement();
			this.StatedElements[i].Deserialize(reader);
		}
	}
}
}
