using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class NamedPartyTeam : NetworkType
{

	public const uint Id = 469;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TeamId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String PartyName { get; set; }

	public NamedPartyTeam() {}


	public NamedPartyTeam InitNamedPartyTeam(byte TeamId, String PartyName)
	{
		this.TeamId = TeamId;
		this.PartyName = PartyName;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.TeamId);
		writer.WriteUTF(this.PartyName);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.TeamId = reader.ReadByte();
		this.PartyName = reader.ReadUTF();
	}
}
}
