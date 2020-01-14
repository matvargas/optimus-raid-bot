using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AllianceCreationValidMessage : NetworkMessage
{

	public const uint Id = 6393;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String AllianceName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String AllianceTag { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GuildEmblem AllianceEmblem { get; set; }

	public AllianceCreationValidMessage() {}


	public AllianceCreationValidMessage InitAllianceCreationValidMessage(String AllianceName, String AllianceTag, GuildEmblem AllianceEmblem)
	{
		this.AllianceName = AllianceName;
		this.AllianceTag = AllianceTag;
		this.AllianceEmblem = AllianceEmblem;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.AllianceName);
		writer.WriteUTF(this.AllianceTag);
		this.AllianceEmblem.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AllianceName = reader.ReadUTF();
		this.AllianceTag = reader.ReadUTF();
		this.AllianceEmblem = new GuildEmblem();
		this.AllianceEmblem.Deserialize(reader);
	}
}
}
