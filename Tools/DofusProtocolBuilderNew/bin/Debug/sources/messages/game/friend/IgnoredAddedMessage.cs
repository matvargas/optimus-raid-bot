using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IgnoredAddedMessage : NetworkMessage
{

	public const uint Id = 5678;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public IgnoredInformations IgnoreAdded { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Session { get; set; }

	public IgnoredAddedMessage() {}


	public IgnoredAddedMessage InitIgnoredAddedMessage(IgnoredInformations IgnoreAdded, bool Session)
	{
		this.IgnoreAdded = IgnoreAdded;
		this.Session = Session;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
writer.WriteShort(IgnoreAdded.MessageId);
		IgnoreAdded.Serialize(writer);
		writer.WriteBoolean(this.Session);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
this.IgnoreAdded = ProtocolTypeManager.GetInstance<IgnoredInformations>(reader.ReadShort());
		this.IgnoreAdded.Deserialize(reader);
		this.Session = reader.ReadBoolean();
	}
}
}
