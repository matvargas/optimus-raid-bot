using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class EntitiesPreset : Preset
{

	public const uint Id = 545;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short IconId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] EntityIds { get; set; }

	public EntitiesPreset() {}


	public EntitiesPreset InitEntitiesPreset(short IconId, short[] EntityIds)
	{
		this.IconId = IconId;
		this.EntityIds = EntityIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.IconId);
		writer.WriteShort(this.EntityIds.Length);
		foreach (short item in this.EntityIds)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.IconId = reader.ReadShort();
		int EntityIdsLen = reader.ReadShort();
		EntityIds = new short[EntityIdsLen];
		for (int i = 0; i < EntityIdsLen; i++)
		{
			this.EntityIds[i] = reader.ReadVarShort();
		}
	}
}
}
