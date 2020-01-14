using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildKickRequestMessage : NetworkMessage
{

	public const uint Id = 5887;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long KickedId { get; set; }

	public GuildKickRequestMessage() {}


	public GuildKickRequestMessage InitGuildKickRequestMessage(long KickedId)
	{
		this.KickedId = KickedId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.KickedId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.KickedId = reader.ReadVarLong();
	}
}
}
