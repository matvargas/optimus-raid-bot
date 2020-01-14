using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class URLOpenMessage : NetworkMessage
{

	public const uint Id = 6266;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte UrlId { get; set; }

	public URLOpenMessage() {}


	public URLOpenMessage InitURLOpenMessage(byte UrlId)
	{
		this.UrlId = UrlId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.UrlId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.UrlId = reader.ReadByte();
	}
}
}
