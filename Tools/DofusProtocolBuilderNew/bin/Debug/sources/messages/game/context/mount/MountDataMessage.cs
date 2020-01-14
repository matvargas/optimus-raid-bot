using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MountDataMessage : NetworkMessage
{

	public const uint Id = 5973;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public MountClientData MountData { get; set; }

	public MountDataMessage() {}


	public MountDataMessage InitMountDataMessage(MountClientData MountData)
	{
		this.MountData = MountData;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.MountData.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MountData = new MountClientData();
		this.MountData.Deserialize(reader);
	}
}
}
