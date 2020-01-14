using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AllianceJoinedMessage : NetworkMessage
{

	public const uint Id = 6402;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AllianceInformations AllianceInfo { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Enabled { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LeadingGuildId { get; set; }

	public AllianceJoinedMessage() {}


	public AllianceJoinedMessage InitAllianceJoinedMessage(AllianceInformations AllianceInfo, bool Enabled, int LeadingGuildId)
	{
		this.AllianceInfo = AllianceInfo;
		this.Enabled = Enabled;
		this.LeadingGuildId = LeadingGuildId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.AllianceInfo.Serialize(writer);
		writer.WriteBoolean(this.Enabled);
		writer.WriteVarInt(this.LeadingGuildId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.AllianceInfo = new AllianceInformations();
		this.AllianceInfo.Deserialize(reader);
		this.Enabled = reader.ReadBoolean();
		this.LeadingGuildId = reader.ReadVarInt();
	}
}
}
