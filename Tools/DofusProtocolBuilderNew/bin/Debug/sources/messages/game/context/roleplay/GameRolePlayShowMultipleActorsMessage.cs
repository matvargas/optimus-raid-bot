using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayShowMultipleActorsMessage : NetworkMessage
{

	public const uint Id = 6712;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameRolePlayActorInformations[] InformationsList { get; set; }

	public GameRolePlayShowMultipleActorsMessage() {}


	public GameRolePlayShowMultipleActorsMessage InitGameRolePlayShowMultipleActorsMessage(GameRolePlayActorInformations[] InformationsList)
	{
		this.InformationsList = InformationsList;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.InformationsList.Length);
		foreach (GameRolePlayActorInformations item in this.InformationsList)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int InformationsListLen = reader.ReadShort();
		InformationsList = new GameRolePlayActorInformations[InformationsListLen];
		for (int i = 0; i < InformationsListLen; i++)
		{
			this.InformationsList[i] = ProtocolTypeManager.GetInstance<GameRolePlayActorInformations>(reader.ReadShort());
			this.InformationsList[i].Deserialize(reader);
		}
	}
}
}
