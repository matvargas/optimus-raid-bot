using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AllianceVersatileInfoListMessage : NetworkMessage
{

	public const uint Id = 6436;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AllianceVersatileInformations[] Alliances { get; set; }

	public AllianceVersatileInfoListMessage() {}


	public AllianceVersatileInfoListMessage InitAllianceVersatileInfoListMessage(AllianceVersatileInformations[] Alliances)
	{
		this.Alliances = Alliances;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Alliances.Length);
		foreach (AllianceVersatileInformations item in this.Alliances)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int AlliancesLen = reader.ReadShort();
		Alliances = new AllianceVersatileInformations[AlliancesLen];
		for (int i = 0; i < AlliancesLen; i++)
		{
			this.Alliances[i] = new AllianceVersatileInformations();
			this.Alliances[i].Deserialize(reader);
		}
	}
}
}
