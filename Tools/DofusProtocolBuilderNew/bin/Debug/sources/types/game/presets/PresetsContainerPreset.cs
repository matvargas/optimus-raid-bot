using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PresetsContainerPreset : Preset
{

	public const uint Id = 520;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public Preset[] Presets { get; set; }

	public PresetsContainerPreset() {}


	public PresetsContainerPreset InitPresetsContainerPreset(Preset[] Presets)
	{
		this.Presets = Presets;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Presets.Length);
		foreach (Preset item in this.Presets)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int PresetsLen = reader.ReadShort();
		Presets = new Preset[PresetsLen];
		for (int i = 0; i < PresetsLen; i++)
		{
			this.Presets[i] = ProtocolTypeManager.GetInstance<Preset>(reader.ReadShort());
			this.Presets[i].Deserialize(reader);
		}
	}
}
}
