using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightEntityDispositionInformations : EntityDispositionInformations
{

	public const uint Id = 217;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double CarryingCharacterId { get; set; }

	public FightEntityDispositionInformations() {}


	public FightEntityDispositionInformations InitFightEntityDispositionInformations(double CarryingCharacterId)
	{
		this.CarryingCharacterId = CarryingCharacterId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.CarryingCharacterId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.CarryingCharacterId = reader.ReadDouble();
	}
}
}
