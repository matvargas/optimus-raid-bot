using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PresetUseRequestMessage : NetworkMessage
{

	public const uint Id = 6759;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PresetId { get; set; }

	public PresetUseRequestMessage() {}


	public PresetUseRequestMessage InitPresetUseRequestMessage(short PresetId)
	{
		this.PresetId = PresetId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.PresetId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PresetId = reader.ReadShort();
	}
}
}
