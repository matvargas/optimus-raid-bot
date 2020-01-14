using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class BasicWhoIsRequestMessage : NetworkMessage
{

	public const uint Id = 181;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Verbose { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Search { get; set; }

	public BasicWhoIsRequestMessage() {}


	public BasicWhoIsRequestMessage InitBasicWhoIsRequestMessage(bool Verbose, String Search)
	{
		this.Verbose = Verbose;
		this.Search = Search;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Verbose);
		writer.WriteUTF(this.Search);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Verbose = reader.ReadBoolean();
		this.Search = reader.ReadUTF();
	}
}
}
