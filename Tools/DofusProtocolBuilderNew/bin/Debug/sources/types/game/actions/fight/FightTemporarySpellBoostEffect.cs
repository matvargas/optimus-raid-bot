using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightTemporarySpellBoostEffect : FightTemporaryBoostEffect
{

	public const uint Id = 207;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short BoostedSpellId { get; set; }

	public FightTemporarySpellBoostEffect() {}


	public FightTemporarySpellBoostEffect InitFightTemporarySpellBoostEffect(short BoostedSpellId)
	{
		this.BoostedSpellId = BoostedSpellId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.BoostedSpellId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.BoostedSpellId = reader.ReadVarShort();
	}
}
}
