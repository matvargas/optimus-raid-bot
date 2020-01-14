using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdentificationAccountForceMessage : IdentificationMessage
{

	public const uint Id = 6119;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String ForcedAccountLogin { get; set; }

	public IdentificationAccountForceMessage() {}


	public IdentificationAccountForceMessage InitIdentificationAccountForceMessage(String ForcedAccountLogin)
	{
		this.ForcedAccountLogin = ForcedAccountLogin;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUTF(this.ForcedAccountLogin);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.ForcedAccountLogin = reader.ReadUTF();
	}
}
}
