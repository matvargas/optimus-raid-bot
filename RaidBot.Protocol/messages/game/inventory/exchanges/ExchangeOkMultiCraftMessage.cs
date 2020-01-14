using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ExchangeOkMultiCraftMessage : NetworkMessage
{

	public const uint Id = 5768;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long InitiatorId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long OtherId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Role { get; set; }

	public ExchangeOkMultiCraftMessage() {}


	public ExchangeOkMultiCraftMessage InitExchangeOkMultiCraftMessage(long InitiatorId, long OtherId, byte Role)
	{
		this.InitiatorId = InitiatorId;
		this.OtherId = OtherId;
		this.Role = Role;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.InitiatorId);
		writer.WriteVarLong(this.OtherId);
		writer.WriteByte(this.Role);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.InitiatorId = reader.ReadVarLong();
		this.OtherId = reader.ReadVarLong();
		this.Role = reader.ReadByte();
	}
}
}
