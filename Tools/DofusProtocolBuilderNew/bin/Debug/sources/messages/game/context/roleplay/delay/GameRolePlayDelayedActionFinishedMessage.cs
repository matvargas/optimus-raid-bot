using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayDelayedActionFinishedMessage : NetworkMessage
{

	public const uint Id = 6150;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double DelayedCharacterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte DelayTypeId { get; set; }

	public GameRolePlayDelayedActionFinishedMessage() {}


	public GameRolePlayDelayedActionFinishedMessage InitGameRolePlayDelayedActionFinishedMessage(double DelayedCharacterId, byte DelayTypeId)
	{
		this.DelayedCharacterId = DelayedCharacterId;
		this.DelayTypeId = DelayTypeId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.DelayedCharacterId);
		writer.WriteByte(this.DelayTypeId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DelayedCharacterId = reader.ReadDouble();
		this.DelayTypeId = reader.ReadByte();
	}
}
}
