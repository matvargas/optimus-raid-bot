using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyUpdateMessage : AbstractPartyEventMessage
{

	public const uint Id = 5575;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PartyMemberInformations MemberInformations { get; set; }

	public PartyUpdateMessage() {}


	public PartyUpdateMessage InitPartyUpdateMessage(PartyMemberInformations MemberInformations)
	{
		this.MemberInformations = MemberInformations;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
writer.WriteShort(MemberInformations.MessageId);
		MemberInformations.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
this.MemberInformations = ProtocolTypeManager.GetInstance<PartyMemberInformations>(reader.ReadShort());
		this.MemberInformations.Deserialize(reader);
	}
}
}
