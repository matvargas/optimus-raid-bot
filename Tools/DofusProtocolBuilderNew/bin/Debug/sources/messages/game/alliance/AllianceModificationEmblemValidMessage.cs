using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AllianceModificationEmblemValidMessage : NetworkMessage
{

	public const uint Id = 6447;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildEmblem Alliancemblem { get; set; }

	public AllianceModificationEmblemValidMessage() {}


	public AllianceModificationEmblemValidMessage InitAllianceModificationEmblemValidMessage(GuildEmblem Alliancemblem)
	{
		this.Alliancemblem = Alliancemblem;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Alliancemblem.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Alliancemblem = new GuildEmblem();
		this.Alliancemblem.Deserialize(reader);
	}
}
}
