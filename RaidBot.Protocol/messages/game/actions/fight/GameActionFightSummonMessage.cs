using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightSummonMessage : AbstractGameActionMessage
{

	public const uint Id = 5825;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameFightFighterInformations[] Summons { get; set; }

	public GameActionFightSummonMessage() {}


	public GameActionFightSummonMessage InitGameActionFightSummonMessage(GameFightFighterInformations[] Summons)
	{
		this.Summons = Summons;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Summons.Length);
		foreach (GameFightFighterInformations item in this.Summons)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int SummonsLen = reader.ReadShort();
		Summons = new GameFightFighterInformations[SummonsLen];
		for (int i = 0; i < SummonsLen; i++)
		{
			this.Summons[i] = ProtocolTypeManager.GetInstance<GameFightFighterInformations>(reader.ReadShort());
			this.Summons[i].Deserialize(reader);
		}
	}
}
}
