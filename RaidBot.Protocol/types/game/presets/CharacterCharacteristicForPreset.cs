using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class CharacterCharacteristicForPreset : SimpleCharacterCharacteristicForPreset
{

	public const uint Id = 539;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Stuff { get; set; }

	public CharacterCharacteristicForPreset() {}


	public CharacterCharacteristicForPreset InitCharacterCharacteristicForPreset(short Stuff)
	{
		this.Stuff = Stuff;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.Stuff);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Stuff = reader.ReadVarShort();
	}
}
}
