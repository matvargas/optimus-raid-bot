using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class OrnamentLostMessage : NetworkMessage
{

	public const uint Id = 6770;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short OrnamentId { get; set; }

	public OrnamentLostMessage() {}


	public OrnamentLostMessage InitOrnamentLostMessage(short OrnamentId)
	{
		this.OrnamentId = OrnamentId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.OrnamentId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.OrnamentId = reader.ReadShort();
	}
}
}
