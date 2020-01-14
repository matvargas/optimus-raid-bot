using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayDelayedActionMessage : NetworkMessage
{

	public const uint Id = 6153;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double DelayedCharacterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte DelayTypeId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double DelayEndTime { get; set; }

	public GameRolePlayDelayedActionMessage() {}


	public GameRolePlayDelayedActionMessage InitGameRolePlayDelayedActionMessage(double DelayedCharacterId, byte DelayTypeId, double DelayEndTime)
	{
		this.DelayedCharacterId = DelayedCharacterId;
		this.DelayTypeId = DelayTypeId;
		this.DelayEndTime = DelayEndTime;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.DelayedCharacterId);
		writer.WriteByte(this.DelayTypeId);
		writer.WriteDouble(this.DelayEndTime);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DelayedCharacterId = reader.ReadDouble();
		this.DelayTypeId = reader.ReadByte();
		this.DelayEndTime = reader.ReadDouble();
	}
}
}
