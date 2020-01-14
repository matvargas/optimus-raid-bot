using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameMapNoMovementMessage : NetworkMessage
{

	public const uint Id = 954;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CellX { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CellY { get; set; }

	public GameMapNoMovementMessage() {}


	public GameMapNoMovementMessage InitGameMapNoMovementMessage(short CellX, short CellY)
	{
		this.CellX = CellX;
		this.CellY = CellY;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.CellX);
		writer.WriteShort(this.CellY);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CellX = reader.ReadShort();
		this.CellY = reader.ReadShort();
	}
}
}
