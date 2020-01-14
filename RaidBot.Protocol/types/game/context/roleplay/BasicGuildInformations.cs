using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class BasicGuildInformations : AbstractSocialGroupInfos
{

	public const uint Id = 365;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int GuildId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String GuildName { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte GuildLevel { get; set; }

	public BasicGuildInformations() {}


	public BasicGuildInformations InitBasicGuildInformations(int GuildId, String GuildName, byte GuildLevel)
	{
		this.GuildId = GuildId;
		this.GuildName = GuildName;
		this.GuildLevel = GuildLevel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.GuildId);
		writer.WriteUTF(this.GuildName);
		writer.WriteByte(this.GuildLevel);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.GuildId = reader.ReadVarInt();
		this.GuildName = reader.ReadUTF();
		this.GuildLevel = reader.ReadByte();
	}
}
}
