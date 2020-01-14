using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class BasicCharactersListMessage : NetworkMessage
{

	public const uint Id = 6475;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterBaseInformations[] Characters { get; set; }

	public BasicCharactersListMessage() {}


	public BasicCharactersListMessage InitBasicCharactersListMessage(CharacterBaseInformations[] Characters)
	{
		this.Characters = Characters;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Characters.Length);
		foreach (CharacterBaseInformations item in this.Characters)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int CharactersLen = reader.ReadShort();
		Characters = new CharacterBaseInformations[CharactersLen];
		for (int i = 0; i < CharactersLen; i++)
		{
			this.Characters[i] = ProtocolTypeManager.GetInstance<CharacterBaseInformations>(reader.ReadShort());
			this.Characters[i].Deserialize(reader);
		}
	}
}
}
