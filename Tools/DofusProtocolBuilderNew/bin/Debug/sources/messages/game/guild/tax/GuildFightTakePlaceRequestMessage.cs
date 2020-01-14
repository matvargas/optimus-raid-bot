using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildFightTakePlaceRequestMessage : GuildFightJoinRequestMessage
{

	public const uint Id = 6235;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ReplacedCharacterId { get; set; }

	public GuildFightTakePlaceRequestMessage() {}


	public GuildFightTakePlaceRequestMessage InitGuildFightTakePlaceRequestMessage(int ReplacedCharacterId)
	{
		this.ReplacedCharacterId = ReplacedCharacterId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(this.ReplacedCharacterId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.ReplacedCharacterId = reader.ReadInt();
	}
}
}
