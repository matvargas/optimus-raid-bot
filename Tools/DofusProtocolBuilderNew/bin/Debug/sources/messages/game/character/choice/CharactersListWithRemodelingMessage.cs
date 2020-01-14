using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CharactersListWithRemodelingMessage : CharactersListMessage
{

	public const uint Id = 6550;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterToRemodelInformations[] CharactersToRemodel { get; set; }

	public CharactersListWithRemodelingMessage() {}


	public CharactersListWithRemodelingMessage InitCharactersListWithRemodelingMessage(CharacterToRemodelInformations[] CharactersToRemodel)
	{
		this.CharactersToRemodel = CharactersToRemodel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.CharactersToRemodel.Length);
		foreach (CharacterToRemodelInformations item in this.CharactersToRemodel)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int CharactersToRemodelLen = reader.ReadShort();
		CharactersToRemodel = new CharacterToRemodelInformations[CharactersToRemodelLen];
		for (int i = 0; i < CharactersToRemodelLen; i++)
		{
			this.CharactersToRemodel[i] = new CharacterToRemodelInformations();
			this.CharactersToRemodel[i].Deserialize(reader);
		}
	}
}
}
