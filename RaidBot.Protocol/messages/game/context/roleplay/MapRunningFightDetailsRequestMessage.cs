using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MapRunningFightDetailsRequestMessage : NetworkMessage
{

	public const uint Id = 5750;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }

	public MapRunningFightDetailsRequestMessage() {}


	public MapRunningFightDetailsRequestMessage InitMapRunningFightDetailsRequestMessage(short FightId)
	{
		this.FightId = FightId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.FightId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FightId = reader.ReadVarShort();
	}
}
}
