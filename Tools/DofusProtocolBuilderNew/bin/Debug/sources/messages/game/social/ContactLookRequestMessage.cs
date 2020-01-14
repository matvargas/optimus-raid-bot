using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ContactLookRequestMessage : NetworkMessage
{

	public const uint Id = 5932;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte RequestId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte ContactType { get; set; }

	public ContactLookRequestMessage() {}


	public ContactLookRequestMessage InitContactLookRequestMessage(byte RequestId, byte ContactType)
	{
		this.RequestId = RequestId;
		this.ContactType = ContactType;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.RequestId);
		writer.WriteByte(this.ContactType);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.RequestId = reader.ReadByte();
		this.ContactType = reader.ReadByte();
	}
}
}
