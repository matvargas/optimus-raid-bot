using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PortalUseRequestMessage : NetworkMessage
{

	public const uint Id = 6492;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int PortalId { get; set; }

	public PortalUseRequestMessage() {}


	public PortalUseRequestMessage InitPortalUseRequestMessage(int PortalId)
	{
		this.PortalId = PortalId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.PortalId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.PortalId = reader.ReadVarInt();
	}
}
}
