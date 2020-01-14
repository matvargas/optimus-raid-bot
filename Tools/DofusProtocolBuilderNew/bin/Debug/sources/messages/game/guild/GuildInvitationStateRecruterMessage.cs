using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildInvitationStateRecruterMessage : NetworkMessage
{

	public const uint Id = 5563;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String RecrutedName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte InvitationState { get; set; }

	public GuildInvitationStateRecruterMessage() {}


	public GuildInvitationStateRecruterMessage InitGuildInvitationStateRecruterMessage(String RecrutedName, byte InvitationState)
	{
		this.RecrutedName = RecrutedName;
		this.InvitationState = InvitationState;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.RecrutedName);
		writer.WriteByte(this.InvitationState);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.RecrutedName = reader.ReadUTF();
		this.InvitationState = reader.ReadByte();
	}
}
}
