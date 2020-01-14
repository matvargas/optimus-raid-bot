using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightTemporarySpellImmunityEffect : AbstractFightDispellableEffect
{

	public const uint Id = 366;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ImmuneSpellId { get; set; }

	public FightTemporarySpellImmunityEffect() {}


	public FightTemporarySpellImmunityEffect InitFightTemporarySpellImmunityEffect(int ImmuneSpellId)
	{
		this.ImmuneSpellId = ImmuneSpellId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(this.ImmuneSpellId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.ImmuneSpellId = reader.ReadInt();
	}
}
}
