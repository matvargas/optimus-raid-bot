using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class BasicNamedAllianceInformations : BasicAllianceInformations
{

	public const uint Id = 418;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String AllianceName { get; set; }

	public BasicNamedAllianceInformations() {}


	public BasicNamedAllianceInformations InitBasicNamedAllianceInformations(String AllianceName)
	{
		this.AllianceName = AllianceName;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUTF(this.AllianceName);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.AllianceName = reader.ReadUTF();
	}
}
}
