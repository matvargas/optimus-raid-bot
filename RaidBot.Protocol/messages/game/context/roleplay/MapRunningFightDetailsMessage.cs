using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MapRunningFightDetailsMessage : NetworkMessage
{

	public const uint Id = 5751;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameFightFighterLightInformations[] Attackers { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameFightFighterLightInformations[] Defenders { get; set; }

	public MapRunningFightDetailsMessage() {}


	public MapRunningFightDetailsMessage InitMapRunningFightDetailsMessage(short FightId, GameFightFighterLightInformations[] Attackers, GameFightFighterLightInformations[] Defenders)
	{
		this.FightId = FightId;
		this.Attackers = Attackers;
		this.Defenders = Defenders;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.FightId);
		writer.WriteShort(this.Attackers.Length);
		foreach (GameFightFighterLightInformations item in this.Attackers)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteShort(this.Defenders.Length);
		foreach (GameFightFighterLightInformations item in this.Defenders)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FightId = reader.ReadVarShort();
		int AttackersLen = reader.ReadShort();
		Attackers = new GameFightFighterLightInformations[AttackersLen];
		for (int i = 0; i < AttackersLen; i++)
		{
			this.Attackers[i] = ProtocolTypeManager.GetInstance<GameFightFighterLightInformations>(reader.ReadShort());
			this.Attackers[i].Deserialize(reader);
		}
		int DefendersLen = reader.ReadShort();
		Defenders = new GameFightFighterLightInformations[DefendersLen];
		for (int i = 0; i < DefendersLen; i++)
		{
			this.Defenders[i] = ProtocolTypeManager.GetInstance<GameFightFighterLightInformations>(reader.ReadShort());
			this.Defenders[i].Deserialize(reader);
		}
	}
}
}
