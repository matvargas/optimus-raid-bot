using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayPlayerFightFriendlyAnswerMessage : NetworkMessage
{

	public const uint Id = 5732;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Accept { get; set; }

	public GameRolePlayPlayerFightFriendlyAnswerMessage() {}


	public GameRolePlayPlayerFightFriendlyAnswerMessage InitGameRolePlayPlayerFightFriendlyAnswerMessage(short FightId, bool Accept)
	{
		this.FightId = FightId;
		this.Accept = Accept;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.FightId);
		writer.WriteBoolean(this.Accept);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FightId = reader.ReadVarShort();
		this.Accept = reader.ReadBoolean();
	}
}
}
