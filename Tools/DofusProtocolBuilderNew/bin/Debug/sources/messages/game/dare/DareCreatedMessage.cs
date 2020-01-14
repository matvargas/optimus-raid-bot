using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DareCreatedMessage : NetworkMessage
{

	public const uint Id = 6668;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DareInformations DareInfos { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool NeedNotifications { get; set; }

	public DareCreatedMessage() {}


	public DareCreatedMessage InitDareCreatedMessage(DareInformations DareInfos, bool NeedNotifications)
	{
		this.DareInfos = DareInfos;
		this.NeedNotifications = NeedNotifications;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.DareInfos.Serialize(writer);
		writer.WriteBoolean(this.NeedNotifications);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DareInfos = new DareInformations();
		this.DareInfos.Deserialize(reader);
		this.NeedNotifications = reader.ReadBoolean();
	}
}
}
