using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class IdolsPreset : Preset
{

	public const uint Id = 491;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short IconId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] IdolIds { get; set; }

	public IdolsPreset() {}


	public IdolsPreset InitIdolsPreset(short IconId, short[] IdolIds)
	{
		this.IconId = IconId;
		this.IdolIds = IdolIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.IconId);
		writer.WriteShort(this.IdolIds.Length);
		foreach (short item in this.IdolIds)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.IconId = reader.ReadShort();
		int IdolIdsLen = reader.ReadShort();
		IdolIds = new short[IdolIdsLen];
		for (int i = 0; i < IdolIdsLen; i++)
		{
			this.IdolIds[i] = reader.ReadVarShort();
		}
	}
}
}
