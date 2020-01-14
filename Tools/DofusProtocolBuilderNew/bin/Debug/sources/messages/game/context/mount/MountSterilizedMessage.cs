using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MountSterilizedMessage : NetworkMessage
{

	public const uint Id = 5977;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MountId { get; set; }

	public MountSterilizedMessage() {}


	public MountSterilizedMessage InitMountSterilizedMessage(int MountId)
	{
		this.MountId = MountId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.MountId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MountId = reader.ReadVarInt();
	}
}
}
