using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdentificationFailedForBadVersionMessage : IdentificationFailedMessage
{

	public const uint Id = 21;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public Version RequiredVersion { get; set; }

	public IdentificationFailedForBadVersionMessage() {}


	public IdentificationFailedForBadVersionMessage InitIdentificationFailedForBadVersionMessage(Version RequiredVersion)
	{
		this.RequiredVersion = RequiredVersion;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.RequiredVersion.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.RequiredVersion = new Version();
		this.RequiredVersion.Deserialize(reader);
	}
}
}
