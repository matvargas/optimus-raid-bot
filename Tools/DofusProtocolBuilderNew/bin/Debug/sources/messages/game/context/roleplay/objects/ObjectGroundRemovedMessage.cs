using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ObjectGroundRemovedMessage : NetworkMessage
{

	public const uint Id = 3014;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Cell { get; set; }

	public ObjectGroundRemovedMessage() {}


	public ObjectGroundRemovedMessage InitObjectGroundRemovedMessage(short Cell)
	{
		this.Cell = Cell;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.Cell);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Cell = reader.ReadVarShort();
	}
}
}
