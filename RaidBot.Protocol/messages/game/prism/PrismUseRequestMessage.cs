using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PrismUseRequestMessage : NetworkMessage
{

	public const uint Id = 6041;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte ModuleToUse { get; set; }

	public PrismUseRequestMessage() {}


	public PrismUseRequestMessage InitPrismUseRequestMessage(byte ModuleToUse)
	{
		this.ModuleToUse = ModuleToUse;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.ModuleToUse);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ModuleToUse = reader.ReadByte();
	}
}
}
