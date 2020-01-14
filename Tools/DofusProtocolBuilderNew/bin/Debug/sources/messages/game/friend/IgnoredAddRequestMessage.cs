using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IgnoredAddRequestMessage : NetworkMessage
{

	public const uint Id = 5673;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String Name { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Session { get; set; }

	public IgnoredAddRequestMessage() {}


	public IgnoredAddRequestMessage InitIgnoredAddRequestMessage(String Name, bool Session)
	{
		this.Name = Name;
		this.Session = Session;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteUTF(this.Name);
		writer.WriteBoolean(this.Session);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Name = reader.ReadUTF();
		this.Session = reader.ReadBoolean();
	}
}
}
