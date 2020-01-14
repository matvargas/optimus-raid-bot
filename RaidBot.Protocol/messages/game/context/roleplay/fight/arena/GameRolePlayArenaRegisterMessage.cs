using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayArenaRegisterMessage : NetworkMessage
{

	public const uint Id = 6280;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int BattleMode { get; set; }

	public GameRolePlayArenaRegisterMessage() {}


	public GameRolePlayArenaRegisterMessage InitGameRolePlayArenaRegisterMessage(int BattleMode)
	{
		this.BattleMode = BattleMode;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.BattleMode);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.BattleMode = reader.ReadInt();
	}
}
}
