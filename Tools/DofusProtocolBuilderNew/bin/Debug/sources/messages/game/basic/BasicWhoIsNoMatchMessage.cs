using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class BasicWhoIsNoMatchMessage : NetworkMessage
{

	public const uint Id = 179;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Search { get; set; }

	public BasicWhoIsNoMatchMessage() {}


	public BasicWhoIsNoMatchMessage InitBasicWhoIsNoMatchMessage(String Search)
	{
		this.Search = Search;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.Search);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Search = reader.ReadUTF();
	}
}
}
