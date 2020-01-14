using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayShowActorMessage : NetworkMessage
{

	public const uint Id = 5632;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameRolePlayActorInformations Informations { get; set; }

	public GameRolePlayShowActorMessage() {}


	public GameRolePlayShowActorMessage InitGameRolePlayShowActorMessage(GameRolePlayActorInformations Informations)
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
this.Informations = ProtocolTypeManager.GetInstance<GameRolePlayActorInformations>(reader.ReadShort());
		this.Informations.Deserialize(reader);
	}
}
}
