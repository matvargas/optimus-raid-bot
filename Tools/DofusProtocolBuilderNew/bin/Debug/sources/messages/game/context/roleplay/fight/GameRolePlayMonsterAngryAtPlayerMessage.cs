using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayMonsterAngryAtPlayerMessage : NetworkMessage
{

	public const uint Id = 6741;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long PlayerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MonsterGroupId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double AngryStartTime { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double AttackTime { get; set; }

	public GameRolePlayMonsterAngryAtPlayerMessage() {}


	public GameRolePlayMonsterAngryAtPlayerMessage InitGameRolePlayMonsterAngryAtPlayerMessage(long PlayerId, double MonsterGroupId, double AngryStartTime, double AttackTime)
	{
		this.PlayerId = PlayerId;
		this.MonsterGroupId = MonsterGroupId;
		this.AngryStartTime = AngryStartTime;
		this.AttackTime = AttackTime;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.PlayerId);
		writer.WriteDouble(this.MonsterGroupId);
		writer.WriteDouble(this.AngryStartTime);
		writer.WriteDouble(this.AttackTime);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PlayerId = reader.ReadVarLong();
		this.MonsterGroupId = reader.ReadDouble();
		this.AngryStartTime = reader.ReadDouble();
		this.AttackTime = reader.ReadDouble();
	}
}
}
