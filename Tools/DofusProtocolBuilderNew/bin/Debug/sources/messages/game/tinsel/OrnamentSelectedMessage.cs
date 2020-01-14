using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class OrnamentSelectedMessage : NetworkMessage
{

	public const uint Id = 6369;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short OrnamentId { get; set; }

	public OrnamentSelectedMessage() {}


	public OrnamentSelectedMessage InitOrnamentSelectedMessage(short OrnamentId)
	{
		this.OrnamentId = OrnamentId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.OrnamentId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.OrnamentId = reader.ReadVarShort();
	}
}
}
