using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightShowFighterMessage : NetworkMessage
{

	public const uint Id = 5864;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameFightFighterInformations Informations { get; set; }

	public GameFightShowFighterMessage() {}


	public GameFightShowFighterMessage InitGameFightShowFighterMessage(GameFightFighterInformations Informations)
	{
		this.Informations = Informations;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
writer.WriteShort(Informations.MessageId);
		Informations.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
this.Informations = ProtocolTypeManager.GetInstance<GameFightFighterInformations>(reader.ReadShort());
		this.Informations.Deserialize(reader);
	}
}
}
