using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class AlliancedGuildFactSheetInformations : GuildInformations
{

	public const uint Id = 422;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public BasicNamedAllianceInformations AllianceInfos { get; set; }

	public AlliancedGuildFactSheetInformations() {}


	public AlliancedGuildFactSheetInformations InitAlliancedGuildFactSheetInformations(BasicNamedAllianceInformations AllianceInfos)
	{
		this.AllianceInfos = AllianceInfos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.AllianceInfos.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.AllianceInfos = new BasicNamedAllianceInformations();
		this.AllianceInfos.Deserialize(reader);
	}
}
}
