using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class CharacterSpellModification : NetworkType
{

	public const uint Id = 215;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte ModificationType { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SpellId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterBaseCharacteristic Value { get; set; }

	public CharacterSpellModification() {}


	public CharacterSpellModification InitCharacterSpellModification(byte ModificationType, short SpellId, CharacterBaseCharacteristic Value)
	{
		this.ModificationType = ModificationType;
		this.SpellId = SpellId;
		this.Value = Value;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.ModificationType);
		writer.WriteVarShort(this.SpellId);
		this.Value.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ModificationType = reader.ReadByte();
		this.SpellId = reader.ReadVarShort();
		this.Value = new CharacterBaseCharacteristic();
		this.Value.Deserialize(reader);
	}
}
}
