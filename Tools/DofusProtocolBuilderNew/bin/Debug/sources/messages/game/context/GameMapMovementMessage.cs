using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameMapMovementMessage : NetworkMessage
{

	public const uint Id = 951;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] KeyMovements { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ForcedDirection { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double ActorId { get; set; }

	public GameMapMovementMessage() {}


	public GameMapMovementMessage InitGameMapMovementMessage(short[] KeyMovements, short ForcedDirection, double ActorId)
	{
		this.KeyMovements = KeyMovements;
		this.ForcedDirection = ForcedDirection;
		this.ActorId = ActorId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.KeyMovements.Length);
		foreach (short item in this.KeyMovements)
		{
			writer.WriteShort(item);
		}
		writer.WriteShort(this.ForcedDirection);
		writer.WriteDouble(this.ActorId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int KeyMovementsLen = reader.ReadShort();
		KeyMovements = new short[KeyMovementsLen];
		for (int i = 0; i < KeyMovementsLen; i++)
		{
			this.KeyMovements[i] = reader.ReadShort();
		}
		this.ForcedDirection = reader.ReadShort();
		this.ActorId = reader.ReadDouble();
	}
}
}
