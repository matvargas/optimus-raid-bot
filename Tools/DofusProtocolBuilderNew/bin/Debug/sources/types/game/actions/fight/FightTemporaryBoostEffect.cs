using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightTemporaryBoostEffect : AbstractFightDispellableEffect
{

	public const uint Id = 209;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Delta { get; set; }

	public FightTemporaryBoostEffect() {}


	public FightTemporaryBoostEffect InitFightTemporaryBoostEffect(short Delta)
	{
		this.Delta = Delta;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Delta);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Delta = reader.ReadShort();
	}
}
}
