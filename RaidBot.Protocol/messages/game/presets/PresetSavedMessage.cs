using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PresetSavedMessage : NetworkMessage
{

	public const uint Id = 6763;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PresetId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public Preset Preset { get; set; }

	public PresetSavedMessage() {}


	public PresetSavedMessage InitPresetSavedMessage(short PresetId, Preset Preset)
	{
		this.PresetId = PresetId;
		this.Preset = Preset;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.PresetId);
writer.WriteShort(Preset.MessageId);
		Preset.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PresetId = reader.ReadShort();
this.Preset = ProtocolTypeManager.GetInstance<Preset>(reader.ReadShort());
		this.Preset.Deserialize(reader);
	}
}
}
