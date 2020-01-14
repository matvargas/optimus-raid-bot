using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayMonsterNotAngryAtPlayerMessage : NetworkMessage
{

	public const uint Id = 6742;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long PlayerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MonsterGroupId { get; set; }

	public GameRolePlayMonsterNotAngryAtPlayerMessage() {}


	public GameRolePlayMonsterNotAngryAtPlayerMessage InitGameRolePlayMonsterNotAngryAtPlayerMessage(long PlayerId, double MonsterGroupId)
	{
		this.PlayerId = PlayerId;
		this.MonsterGroupId = MonsterGroupId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.PlayerId);
		writer.WriteDouble(this.MonsterGroupId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PlayerId = reader.ReadVarLong();
		this.MonsterGroupId = reader.ReadDouble();
	}
}
}
