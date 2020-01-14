using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HaapiApiKeyMessage : NetworkMessage
{

	public const uint Id = 6649;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Token { get; set; }

	public HaapiApiKeyMessage() {}


	public HaapiApiKeyMessage InitHaapiApiKeyMessage(String Token)
	{
		this.Token = Token;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.Token);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Token = reader.ReadUTF();
	}
}
}
