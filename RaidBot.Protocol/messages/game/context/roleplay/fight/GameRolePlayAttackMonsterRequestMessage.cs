using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayAttackMonsterRequestMessage : NetworkMessage
{

	public const uint Id = 6191;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MonsterGroupId { get; set; }

	public GameRolePlayAttackMonsterRequestMessage() {}


	public GameRolePlayAttackMonsterRequestMessage InitGameRolePlayAttackMonsterRequestMessage(double MonsterGroupId)
	{
		this.MonsterGroupId = MonsterGroupId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.MonsterGroupId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MonsterGroupId = reader.ReadDouble();
	}
}
}
