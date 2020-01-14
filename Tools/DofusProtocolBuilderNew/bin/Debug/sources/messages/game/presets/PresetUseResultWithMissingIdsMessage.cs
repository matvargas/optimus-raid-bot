using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PresetUseResultWithMissingIdsMessage : PresetUseResultMessage
{

	public const uint Id = 6757;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] MissingIds { get; set; }

	public PresetUseResultWithMissingIdsMessage() {}


	public PresetUseResultWithMissingIdsMessage InitPresetUseResultWithMissingIdsMessage(short[] MissingIds)
	{
		this.MissingIds = MissingIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.MissingIds.Length);
		foreach (short item in this.MissingIds)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int MissingIdsLen = reader.ReadShort();
		MissingIds = new short[MissingIdsLen];
		for (int i = 0; i < MissingIdsLen; i++)
		{
			this.MissingIds[i] = reader.ReadVarShort();
		}
	}
}
}
