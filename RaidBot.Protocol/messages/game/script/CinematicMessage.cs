using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CinematicMessage : NetworkMessage
{

	public const uint Id = 6053;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CinematicId { get; set; }

	public CinematicMessage() {}


	public CinematicMessage InitCinematicMessage(short CinematicId)
	{
		this.CinematicId = CinematicId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.CinematicId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CinematicId = reader.ReadVarShort();
	}
}
}
