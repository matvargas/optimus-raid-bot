using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class JobMultiCraftAvailableSkillsMessage : JobAllowMultiCraftRequestMessage
{

	public const uint Id = 5747;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long PlayerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] Skills { get; set; }

	public JobMultiCraftAvailableSkillsMessage() {}


	public JobMultiCraftAvailableSkillsMessage InitJobMultiCraftAvailableSkillsMessage(long PlayerId, short[] Skills)
	{
		this.PlayerId = PlayerId;
		this.Skills = Skills;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.PlayerId);
		writer.WriteShort(this.Skills.Length);
		foreach (short item in this.Skills)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.PlayerId = reader.ReadVarLong();
		int SkillsLen = reader.ReadShort();
		Skills = new short[SkillsLen];
		for (int i = 0; i < SkillsLen; i++)
		{
			this.Skills[i] = reader.ReadVarShort();
		}
	}
}
}
