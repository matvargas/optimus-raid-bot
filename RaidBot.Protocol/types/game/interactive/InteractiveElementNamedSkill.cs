using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class InteractiveElementNamedSkill : InteractiveElementSkill
{

	public const uint Id = 220;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int NameId { get; set; }

	public InteractiveElementNamedSkill() {}


	public InteractiveElementNamedSkill InitInteractiveElementNamedSkill(int NameId)
	{
		this.NameId = NameId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.NameId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.NameId = reader.ReadVarInt();
	}
}
}
