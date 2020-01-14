using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CharacterCanBeCreatedResultMessage : NetworkMessage
{

	public const uint Id = 6733;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool YesYouCan { get; set; }

	public CharacterCanBeCreatedResultMessage() {}


	public CharacterCanBeCreatedResultMessage InitCharacterCanBeCreatedResultMessage(bool YesYouCan)
	{
		this.YesYouCan = YesYouCan;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.YesYouCan);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.YesYouCan = reader.ReadBoolean();
	}
}
}
