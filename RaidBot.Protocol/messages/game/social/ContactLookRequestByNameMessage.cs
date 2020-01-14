using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ContactLookRequestByNameMessage : ContactLookRequestMessage
{

	public const uint Id = 5933;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String PlayerName { get; set; }

	public ContactLookRequestByNameMessage() {}


	public ContactLookRequestByNameMessage InitContactLookRequestByNameMessage(String PlayerName)
	{
		this.PlayerName = PlayerName;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUTF(this.PlayerName);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.PlayerName = reader.ReadUTF();
	}
}
}
