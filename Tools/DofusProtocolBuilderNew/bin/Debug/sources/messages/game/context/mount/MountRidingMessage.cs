using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MountRidingMessage : NetworkMessage
{

	public const uint Id = 5967;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsRiding { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsAutopilot { get; set; }

	public MountRidingMessage() {}


	public MountRidingMessage InitMountRidingMessage(bool IsRiding, bool IsAutopilot)
	{
		this.IsRiding = IsRiding;
		this.IsAutopilot = IsAutopilot;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, IsRiding);
		box = BooleanByteWrapper.SetFlag(box, 1, IsAutopilot);
		writer.WriteByte(box);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.IsRiding = BooleanByteWrapper.GetFlag(box, 0);
		this.IsAutopilot = BooleanByteWrapper.GetFlag(box, 1);
	}
}
}
