using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightResultFighterListEntry : FightResultListEntry
{

	public const uint Id = 189;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double Id_ { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Alive { get; set; }

	public FightResultFighterListEntry() {}


	public FightResultFighterListEntry InitFightResultFighterListEntry(double Id_, bool Alive)
	{
		this.Id_ = Id_;
		this.Alive = Alive;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.Id_);
		writer.WriteBoolean(this.Alive);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Id_ = reader.ReadDouble();
		this.Alive = reader.ReadBoolean();
	}
}
}
