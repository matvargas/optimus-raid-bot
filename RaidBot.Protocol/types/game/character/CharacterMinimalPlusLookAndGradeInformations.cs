using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class CharacterMinimalPlusLookAndGradeInformations : CharacterMinimalPlusLookInformations
{

	public const uint Id = 193;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Grade { get; set; }

	public CharacterMinimalPlusLookAndGradeInformations() {}


	public CharacterMinimalPlusLookAndGradeInformations InitCharacterMinimalPlusLookAndGradeInformations(int Grade)
	{
		this.Grade = Grade;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.Grade);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Grade = reader.ReadVarInt();
	}
}
}
