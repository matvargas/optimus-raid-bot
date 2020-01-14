using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class InteractiveMapUpdateMessage : NetworkMessage
{

	public const uint Id = 5002;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public InteractiveElement[] InteractiveElements { get; set; }

	public InteractiveMapUpdateMessage() {}


	public InteractiveMapUpdateMessage InitInteractiveMapUpdateMessage(InteractiveElement[] InteractiveElements)
	{
		this.InteractiveElements = InteractiveElements;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.InteractiveElements.Length);
		foreach (InteractiveElement item in this.InteractiveElements)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int InteractiveElementsLen = reader.ReadShort();
		InteractiveElements = new InteractiveElement[InteractiveElementsLen];
		for (int i = 0; i < InteractiveElementsLen; i++)
		{
			this.InteractiveElements[i] = ProtocolTypeManager.GetInstance<InteractiveElement>(reader.ReadShort());
			this.InteractiveElements[i].Deserialize(reader);
		}
	}
}
}
