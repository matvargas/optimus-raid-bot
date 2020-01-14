using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyLocateMembersMessage : AbstractPartyMessage
{

	public const uint Id = 5595;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PartyMemberGeoPosition[] Geopositions { get; set; }

	public PartyLocateMembersMessage() {}


	public PartyLocateMembersMessage InitPartyLocateMembersMessage(PartyMemberGeoPosition[] Geopositions)
	{
		this.Geopositions = Geopositions;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Geopositions.Length);
		foreach (PartyMemberGeoPosition item in this.Geopositions)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int GeopositionsLen = reader.ReadShort();
		Geopositions = new PartyMemberGeoPosition[GeopositionsLen];
		for (int i = 0; i < GeopositionsLen; i++)
		{
			this.Geopositions[i] = new PartyMemberGeoPosition();
			this.Geopositions[i].Deserialize(reader);
		}
	}
}
}
