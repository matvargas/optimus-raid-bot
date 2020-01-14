using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ContactLookErrorMessage : NetworkMessage
{

	public const uint Id = 6045;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int RequestId { get; set; }

	public ContactLookErrorMessage() {}


	public ContactLookErrorMessage InitContactLookErrorMessage(int RequestId)
	{
		this.RequestId = RequestId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.RequestId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.RequestId = reader.ReadVarInt();
	}
}
}
