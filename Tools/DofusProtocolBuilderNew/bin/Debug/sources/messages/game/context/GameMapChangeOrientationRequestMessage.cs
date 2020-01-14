using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameMapChangeOrientationRequestMessage : NetworkMessage
{

	public const uint Id = 945;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Direction { get; set; }

	public GameMapChangeOrientationRequestMessage() {}


	public GameMapChangeOrientationRequestMessage InitGameMapChangeOrientationRequestMessage(byte Direction)
	{
		this.Direction = Direction;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Direction);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Direction = reader.ReadByte();
	}
}
}
