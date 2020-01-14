using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class InviteInHavenBagMessage : NetworkMessage
{

	public const uint Id = 6642;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterMinimalInformations GuestInformations { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Accept { get; set; }

	public InviteInHavenBagMessage() {}


	public InviteInHavenBagMessage InitInviteInHavenBagMessage(CharacterMinimalInformations GuestInformations, bool Accept)
	{
		this.GuestInformations = GuestInformations;
		this.Accept = Accept;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.GuestInformations.Serialize(writer);
		writer.WriteBoolean(this.Accept);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.GuestInformations = new CharacterMinimalInformations();
		this.GuestInformations.Deserialize(reader);
		this.Accept = reader.ReadBoolean();
	}
}
}
