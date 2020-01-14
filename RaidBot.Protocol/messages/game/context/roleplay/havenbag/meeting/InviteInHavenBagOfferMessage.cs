using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class InviteInHavenBagOfferMessage : NetworkMessage
{

	public const uint Id = 6643;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterMinimalInformations HostInformations { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int TimeLeftBeforeCancel { get; set; }

	public InviteInHavenBagOfferMessage() {}


	public InviteInHavenBagOfferMessage InitInviteInHavenBagOfferMessage(CharacterMinimalInformations HostInformations, int TimeLeftBeforeCancel)
	{
		this.HostInformations = HostInformations;
		this.TimeLeftBeforeCancel = TimeLeftBeforeCancel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.HostInformations.Serialize(writer);
		writer.WriteVarInt(this.TimeLeftBeforeCancel);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.HostInformations = new CharacterMinimalInformations();
		this.HostInformations.Deserialize(reader);
		this.TimeLeftBeforeCancel = reader.ReadVarInt();
	}
}
}
