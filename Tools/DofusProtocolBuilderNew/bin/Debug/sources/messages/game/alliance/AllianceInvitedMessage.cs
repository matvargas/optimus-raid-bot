using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AllianceInvitedMessage : NetworkMessage
{

	public const uint Id = 6397;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long RecruterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String RecruterName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public BasicNamedAllianceInformations AllianceInfo { get; set; }

	public AllianceInvitedMessage() {}


	public AllianceInvitedMessage InitAllianceInvitedMessage(long RecruterId, String RecruterName, BasicNamedAllianceInformations AllianceInfo)
	{
		this.RecruterId = RecruterId;
		this.RecruterName = RecruterName;
		this.AllianceInfo = AllianceInfo;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.RecruterId);
		writer.WriteUTF(this.RecruterName);
		this.AllianceInfo.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.RecruterId = reader.ReadVarLong();
		this.RecruterName = reader.ReadUTF();
		this.AllianceInfo = new BasicNamedAllianceInformations();
		this.AllianceInfo.Deserialize(reader);
	}
}
}
