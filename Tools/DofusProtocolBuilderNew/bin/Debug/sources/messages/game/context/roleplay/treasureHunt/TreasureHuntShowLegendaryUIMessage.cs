using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TreasureHuntShowLegendaryUIMessage : NetworkMessage
{

	public const uint Id = 6498;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] AvailableLegendaryIds { get; set; }

	public TreasureHuntShowLegendaryUIMessage() {}


	public TreasureHuntShowLegendaryUIMessage InitTreasureHuntShowLegendaryUIMessage(short[] AvailableLegendaryIds)
	{
		this.AvailableLegendaryIds = AvailableLegendaryIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.AvailableLegendaryIds.Length);
		foreach (short item in this.AvailableLegendaryIds)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int AvailableLegendaryIdsLen = reader.ReadShort();
		AvailableLegendaryIds = new short[AvailableLegendaryIdsLen];
		for (int i = 0; i < AvailableLegendaryIdsLen; i++)
		{
			this.AvailableLegendaryIds[i] = reader.ReadVarShort();
		}
	}
}
}
