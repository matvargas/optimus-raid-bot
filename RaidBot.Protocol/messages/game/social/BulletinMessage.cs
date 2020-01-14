using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class BulletinMessage : SocialNoticeMessage
{

	public const uint Id = 6695;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LastNotifiedTimestamp { get; set; }

	public BulletinMessage() {}


	public BulletinMessage InitBulletinMessage(int LastNotifiedTimestamp)
	{
		this.LastNotifiedTimestamp = LastNotifiedTimestamp;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(this.LastNotifiedTimestamp);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.LastNotifiedTimestamp = reader.ReadInt();
	}
}
}
