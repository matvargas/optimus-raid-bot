using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionAcknowledgementMessage : NetworkMessage
{

	public const uint Id = 957;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Valid { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte ActionId { get; set; }

	public GameActionAcknowledgementMessage() {}


	public GameActionAcknowledgementMessage InitGameActionAcknowledgementMessage(bool Valid, byte ActionId)
	{
		this.Valid = Valid;
		this.ActionId = ActionId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Valid);
		writer.WriteByte(this.ActionId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Valid = reader.ReadBoolean();
		this.ActionId = reader.ReadByte();
	}
}
}
