using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AccessoryPreviewRequestMessage : NetworkMessage
{

	public const uint Id = 6518;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] GenericId { get; set; }

	public AccessoryPreviewRequestMessage() {}


	public AccessoryPreviewRequestMessage InitAccessoryPreviewRequestMessage(short[] GenericId)
	{
		this.GenericId = GenericId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.GenericId.Length);
		foreach (short item in this.GenericId)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int GenericIdLen = reader.ReadShort();
		GenericId = new short[GenericIdLen];
		for (int i = 0; i < GenericIdLen; i++)
		{
			this.GenericId[i] = reader.ReadVarShort();
		}
	}
}
}
