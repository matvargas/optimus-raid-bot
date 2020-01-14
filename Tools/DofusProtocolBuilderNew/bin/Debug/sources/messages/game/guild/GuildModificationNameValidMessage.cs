using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildModificationNameValidMessage : NetworkMessage
{

	public const uint Id = 6327;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String GuildName { get; set; }

	public GuildModificationNameValidMessage() {}


	public GuildModificationNameValidMessage InitGuildModificationNameValidMessage(String GuildName)
	{
		this.GuildName = GuildName;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.GuildName);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.GuildName = reader.ReadUTF();
	}
}
}
