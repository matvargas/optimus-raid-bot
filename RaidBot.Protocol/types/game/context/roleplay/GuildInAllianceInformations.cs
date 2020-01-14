using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GuildInAllianceInformations : GuildInformations
{

	public const uint Id = 420;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NbMembers { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int JoinDate { get; set; }

	public GuildInAllianceInformations() {}


	public GuildInAllianceInformations InitGuildInAllianceInformations(byte NbMembers, int JoinDate)
	{
		this.NbMembers = NbMembers;
		this.JoinDate = JoinDate;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.NbMembers);
		writer.WriteInt(this.JoinDate);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.NbMembers = reader.ReadByte();
		this.JoinDate = reader.ReadInt();
	}
}
}
