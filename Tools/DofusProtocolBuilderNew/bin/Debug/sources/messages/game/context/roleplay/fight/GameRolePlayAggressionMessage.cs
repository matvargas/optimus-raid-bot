using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayAggressionMessage : NetworkMessage
{

	public const uint Id = 6073;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long AttackerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long DefenderId { get; set; }

	public GameRolePlayAggressionMessage() {}


	public GameRolePlayAggressionMessage InitGameRolePlayAggressionMessage(long AttackerId, long DefenderId)
	{
		this.AttackerId = AttackerId;
		this.DefenderId = DefenderId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.AttackerId);
		writer.WriteVarLong(this.DefenderId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AttackerId = reader.ReadVarLong();
		this.DefenderId = reader.ReadVarLong();
	}
}
}
