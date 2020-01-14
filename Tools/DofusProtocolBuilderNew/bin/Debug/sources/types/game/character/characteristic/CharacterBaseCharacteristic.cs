using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class CharacterBaseCharacteristic : NetworkType
{

	public const uint Id = 4;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Base { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Additionnal { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ObjectsAndMountBonus { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short AlignGiftBonus { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ContextModif { get; set; }

	public CharacterBaseCharacteristic() {}


	public CharacterBaseCharacteristic InitCharacterBaseCharacteristic(short Base, short Additionnal, short ObjectsAndMountBonus, short AlignGiftBonus, short ContextModif)
	{
		this.Base = Base;
		this.Additionnal = Additionnal;
		this.ObjectsAndMountBonus = ObjectsAndMountBonus;
		this.AlignGiftBonus = AlignGiftBonus;
		this.ContextModif = ContextModif;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.Base);
		writer.WriteVarShort(this.Additionnal);
		writer.WriteVarShort(this.ObjectsAndMountBonus);
		writer.WriteVarShort(this.AlignGiftBonus);
		writer.WriteVarShort(this.ContextModif);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Base = reader.ReadVarShort();
		this.Additionnal = reader.ReadVarShort();
		this.ObjectsAndMountBonus = reader.ReadVarShort();
		this.AlignGiftBonus = reader.ReadVarShort();
		this.ContextModif = reader.ReadVarShort();
	}
}
}
