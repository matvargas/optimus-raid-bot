using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class CharacterMinimalPlusLookInformations : CharacterMinimalInformations
{

	public const uint Id = 163;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityLook EntityLook { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Breed { get; set; }

	public CharacterMinimalPlusLookInformations() {}


	public CharacterMinimalPlusLookInformations InitCharacterMinimalPlusLookInformations(EntityLook EntityLook, byte Breed)
	{
		this.EntityLook = EntityLook;
		this.Breed = Breed;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.EntityLook.Serialize(writer);
		writer.WriteByte(this.Breed);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.EntityLook = new EntityLook();
		this.EntityLook.Deserialize(reader);
		this.Breed = reader.ReadByte();
	}
}
}
