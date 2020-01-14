using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ObjectEffectCreature : ObjectEffect
{

	public const uint Id = 71;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MonsterFamilyId { get; set; }

	public ObjectEffectCreature() {}


	public ObjectEffectCreature InitObjectEffectCreature(short MonsterFamilyId)
	{
		this.MonsterFamilyId = MonsterFamilyId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.MonsterFamilyId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.MonsterFamilyId = reader.ReadVarShort();
	}
}
}
