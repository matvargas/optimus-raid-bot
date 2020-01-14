using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MapRunningFightListMessage : NetworkMessage
{

	public const uint Id = 5743;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightExternalInformations[] Fights { get; set; }

	public MapRunningFightListMessage() {}


	public MapRunningFightListMessage InitMapRunningFightListMessage(FightExternalInformations[] Fights)
	{
		this.Fights = Fights;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Fights.Length);
		foreach (FightExternalInformations item in this.Fights)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int FightsLen = reader.ReadShort();
		Fights = new FightExternalInformations[FightsLen];
		for (int i = 0; i < FightsLen; i++)
		{
			this.Fights[i] = new FightExternalInformations();
			this.Fights[i].Deserialize(reader);
		}
	}
}
}
