using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightSynchronizeMessage : NetworkMessage
{

	public const uint Id = 5921;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameFightFighterInformations[] Fighters { get; set; }

	public GameFightSynchronizeMessage() {}


	public GameFightSynchronizeMessage InitGameFightSynchronizeMessage(GameFightFighterInformations[] Fighters)
	{
		this.Fighters = Fighters;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Fighters.Length);
		foreach (GameFightFighterInformations item in this.Fighters)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int FightersLen = reader.ReadShort();
		Fighters = new GameFightFighterInformations[FightersLen];
		for (int i = 0; i < FightersLen; i++)
		{
			this.Fighters[i] = ProtocolTypeManager.GetInstance<GameFightFighterInformations>(reader.ReadShort());
			this.Fighters[i].Deserialize(reader);
		}
	}
}
}
