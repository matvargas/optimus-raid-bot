using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildPaddockTeleportRequestMessage : NetworkMessage
{

	public const uint Id = 5957;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double PaddockId { get; set; }

	public GuildPaddockTeleportRequestMessage() {}


	public GuildPaddockTeleportRequestMessage InitGuildPaddockTeleportRequestMessage(double PaddockId)
	{
		this.PaddockId = PaddockId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.PaddockId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PaddockId = reader.ReadDouble();
	}
}
}
