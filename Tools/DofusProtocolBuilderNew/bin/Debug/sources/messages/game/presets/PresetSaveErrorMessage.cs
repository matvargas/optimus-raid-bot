using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PresetSaveErrorMessage : NetworkMessage
{

	public const uint Id = 6762;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PresetId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Code { get; set; }

	public PresetSaveErrorMessage() {}


	public PresetSaveErrorMessage InitPresetSaveErrorMessage(short PresetId, byte Code)
	{
		this.PresetId = PresetId;
		this.Code = Code;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.PresetId);
		writer.WriteByte(this.Code);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PresetId = reader.ReadShort();
		this.Code = reader.ReadByte();
	}
}
}
