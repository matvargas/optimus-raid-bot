using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ServerSelectionMessage : NetworkMessage
{

	public const uint Id = 40;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ServerId { get; set; }

	public ServerSelectionMessage() {}


	public ServerSelectionMessage InitServerSelectionMessage(short ServerId)
	{
		this.ServerId = ServerId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.ServerId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ServerId = reader.ReadVarShort();
	}
}
}
