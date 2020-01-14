using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PrismSetSabotagedRefusedMessage : NetworkMessage
{

	public const uint Id = 6466;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SubAreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Reason { get; set; }

	public PrismSetSabotagedRefusedMessage() {}


	public PrismSetSabotagedRefusedMessage InitPrismSetSabotagedRefusedMessage(short SubAreaId, byte Reason)
	{
		this.SubAreaId = SubAreaId;
		this.Reason = Reason;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SubAreaId);
		writer.WriteByte(this.Reason);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SubAreaId = reader.ReadVarShort();
		this.Reason = reader.ReadByte();
	}
}
}