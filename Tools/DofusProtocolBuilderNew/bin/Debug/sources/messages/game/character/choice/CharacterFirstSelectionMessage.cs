using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CharacterFirstSelectionMessage : CharacterSelectionMessage
{

	public const uint Id = 6084;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool DoTutorial { get; set; }

	public CharacterFirstSelectionMessage() {}


	public CharacterFirstSelectionMessage InitCharacterFirstSelectionMessage(bool DoTutorial)
	{
		this.DoTutorial = DoTutorial;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteBoolean(this.DoTutorial);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.DoTutorial = reader.ReadBoolean();
	}
}
}
