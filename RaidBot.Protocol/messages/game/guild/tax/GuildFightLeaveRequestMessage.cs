using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildFightLeaveRequestMessage : NetworkMessage
{

	public const uint Id = 5715;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double TaxCollectorId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long CharacterId { get; set; }

	public GuildFightLeaveRequestMessage() {}


	public GuildFightLeaveRequestMessage InitGuildFightLeaveRequestMessage(double TaxCollectorId, long CharacterId)
	{
		this.TaxCollectorId = TaxCollectorId;
		this.CharacterId = CharacterId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.TaxCollectorId);
		writer.WriteVarLong(this.CharacterId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.TaxCollectorId = reader.ReadDouble();
		this.CharacterId = reader.ReadVarLong();
	}
}
}
