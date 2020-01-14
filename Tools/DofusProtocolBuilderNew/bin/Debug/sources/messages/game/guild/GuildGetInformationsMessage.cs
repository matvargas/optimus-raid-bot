using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildGetInformationsMessage : NetworkMessage
{

	public const uint Id = 5550;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte InfoType { get; set; }

	public GuildGetInformationsMessage() {}


	public GuildGetInformationsMessage InitGuildGetInformationsMessage(byte InfoType)
	{
		this.InfoType = InfoType;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.InfoType);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.InfoType = reader.ReadByte();
	}
}
}
