using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AreaFightModificatorUpdateMessage : NetworkMessage
{

	public const uint Id = 6493;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SpellPairId { get; set; }

	public AreaFightModificatorUpdateMessage() {}


	public AreaFightModificatorUpdateMessage InitAreaFightModificatorUpdateMessage(int SpellPairId)
	{
		this.SpellPairId = SpellPairId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.SpellPairId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SpellPairId = reader.ReadInt();
	}
}
}
