using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PartyMemberArenaInformations : PartyMemberInformations
{

	public const uint Id = 391;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Rank { get; set; }

	public PartyMemberArenaInformations() {}


	public PartyMemberArenaInformations InitPartyMemberArenaInformations(short Rank)
	{
		this.Rank = Rank;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.Rank);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.Rank = reader.ReadVarShort();
	}
}
}
