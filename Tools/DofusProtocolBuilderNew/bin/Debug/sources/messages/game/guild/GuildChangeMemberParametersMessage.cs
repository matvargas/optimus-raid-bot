using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuildChangeMemberParametersMessage : NetworkMessage
{

	public const uint Id = 5549;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long MemberId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Rank { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte ExperienceGivenPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Rights { get; set; }

	public GuildChangeMemberParametersMessage() {}


	public GuildChangeMemberParametersMessage InitGuildChangeMemberParametersMessage(long MemberId, short Rank, byte ExperienceGivenPercent, int Rights)
	{
		this.MemberId = MemberId;
		this.Rank = Rank;
		this.ExperienceGivenPercent = ExperienceGivenPercent;
		this.Rights = Rights;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.MemberId);
		writer.WriteVarShort(this.Rank);
		writer.WriteByte(this.ExperienceGivenPercent);
		writer.WriteVarInt(this.Rights);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MemberId = reader.ReadVarLong();
		this.Rank = reader.ReadVarShort();
		this.ExperienceGivenPercent = reader.ReadByte();
		this.Rights = reader.ReadVarInt();
	}
}
}
