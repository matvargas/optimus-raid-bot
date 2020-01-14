using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CharacterLevelUpMessage : NetworkMessage
{

	public const uint Id = 5670;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NewLevel { get; set; }

	public CharacterLevelUpMessage() {}


	public CharacterLevelUpMessage InitCharacterLevelUpMessage(short NewLevel)
	{
		this.NewLevel = NewLevel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.NewLevel);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.NewLevel = reader.ReadVarShort();
	}
}
}
