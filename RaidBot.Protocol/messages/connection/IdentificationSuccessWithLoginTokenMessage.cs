using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdentificationSuccessWithLoginTokenMessage : IdentificationSuccessMessage
{

	public const uint Id = 6209;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String LoginToken { get; set; }

	public IdentificationSuccessWithLoginTokenMessage() {}


	public IdentificationSuccessWithLoginTokenMessage InitIdentificationSuccessWithLoginTokenMessage(String LoginToken)
	{
		this.LoginToken = LoginToken;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteUTF(this.LoginToken);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.LoginToken = reader.ReadUTF();
	}
}
}
