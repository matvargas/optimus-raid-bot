using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameRolePlayNpcWithQuestInformations : GameRolePlayNpcInformations
{

	public const uint Id = 383;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameRolePlayNpcQuestFlag QuestFlag { get; set; }

	public GameRolePlayNpcWithQuestInformations() {}


	public GameRolePlayNpcWithQuestInformations InitGameRolePlayNpcWithQuestInformations(GameRolePlayNpcQuestFlag QuestFlag)
	{
		this.QuestFlag = QuestFlag;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.QuestFlag.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.QuestFlag = new GameRolePlayNpcQuestFlag();
		this.QuestFlag.Deserialize(reader);
	}
}
}
