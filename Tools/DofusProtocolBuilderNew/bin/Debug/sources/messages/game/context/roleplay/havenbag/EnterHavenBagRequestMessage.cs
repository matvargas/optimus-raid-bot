using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class EnterHavenBagRequestMessage : NetworkMessage
{

	public const uint Id = 6636;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long HavenBagOwner { get; set; }

	public EnterHavenBagRequestMessage() {}


	public EnterHavenBagRequestMessage InitEnterHavenBagRequestMessage(long HavenBagOwner)
	{
		this.HavenBagOwner = HavenBagOwner;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.HavenBagOwner);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.HavenBagOwner = reader.ReadVarLong();
	}
}
}
