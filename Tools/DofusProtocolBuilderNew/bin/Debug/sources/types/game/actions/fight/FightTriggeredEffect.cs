using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightTriggeredEffect : AbstractFightDispellableEffect
{

	public const uint Id = 210;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Delay { get; set; }

	public FightTriggeredEffect() {}


	public FightTriggeredEffect InitFightTriggeredEffect(short Delay)
	{
		this.Delay = Delay;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Delay);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Delay = reader.ReadShort();
	}
}
}
