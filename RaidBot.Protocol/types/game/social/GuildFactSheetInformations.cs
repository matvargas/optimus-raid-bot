using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GuildFactSheetInformations : GuildInformations
{

	public const uint Id = 424;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long LeaderId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NbMembers { get; set; }

	public GuildFactSheetInformations() {}


	public GuildFactSheetInformations InitGuildFactSheetInformations(long LeaderId, short NbMembers)
	{
		this.LeaderId = LeaderId;
		this.NbMembers = NbMembers;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarLong(this.LeaderId);
		writer.WriteVarShort(this.NbMembers);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.LeaderId = reader.ReadVarLong();
		this.NbMembers = reader.ReadVarShort();
	}
}
}
