using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class InteractiveElementWithAgeBonus : InteractiveElement
{

	public const uint Id = 398;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short AgeBonus { get; set; }

	public InteractiveElementWithAgeBonus() {}


	public InteractiveElementWithAgeBonus InitInteractiveElementWithAgeBonus(short AgeBonus)
	{
		this.AgeBonus = AgeBonus;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.AgeBonus);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.AgeBonus = reader.ReadShort();
	}
}
}
