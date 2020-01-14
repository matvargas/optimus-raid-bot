using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MountInformationInPaddockRequestMessage : NetworkMessage
{

	public const uint Id = 5975;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MapRideId { get; set; }

	public MountInformationInPaddockRequestMessage() {}


	public MountInformationInPaddockRequestMessage InitMountInformationInPaddockRequestMessage(int MapRideId)
	{
		this.MapRideId = MapRideId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.MapRideId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MapRideId = reader.ReadVarInt();
	}
}
}
