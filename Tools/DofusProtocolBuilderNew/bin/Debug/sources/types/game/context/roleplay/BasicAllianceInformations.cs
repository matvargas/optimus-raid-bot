using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class BasicAllianceInformations : AbstractSocialGroupInfos
{

	public const uint Id = 419;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int AllianceId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String AllianceTag { get; set; }

	public BasicAllianceInformations() {}


	public BasicAllianceInformations InitBasicAllianceInformations(int AllianceId, String AllianceTag)
	{
		this.AllianceId = AllianceId;
		this.AllianceTag = AllianceTag;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.AllianceId);
		writer.WriteUTF(this.AllianceTag);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.AllianceId = reader.ReadVarInt();
		this.AllianceTag = reader.ReadUTF();
	}
}
}
