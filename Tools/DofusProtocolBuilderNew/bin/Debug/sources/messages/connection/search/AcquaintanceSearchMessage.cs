using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class AcquaintanceSearchMessage : NetworkMessage
{

	public const uint Id = 6144;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Nickname { get; set; }

	public AcquaintanceSearchMessage() {}


	public AcquaintanceSearchMessage InitAcquaintanceSearchMessage(String Nickname)
	{
		this.Nickname = Nickname;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.Nickname);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Nickname = reader.ReadUTF();
	}
}
}
