using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class LockableChangeCodeMessage : NetworkMessage
{

	public const uint Id = 5666;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Code { get; set; }

	public LockableChangeCodeMessage() {}


	public LockableChangeCodeMessage InitLockableChangeCodeMessage(String Code)
	{
		this.Code = Code;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.Code);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Code = reader.ReadUTF();
	}
}
}
