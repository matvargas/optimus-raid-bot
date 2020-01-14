using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class HouseTeleportRequestMessage : NetworkMessage
{

	public const uint Id = 6726;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int HouseId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int HouseInstanceId { get; set; }

	public HouseTeleportRequestMessage() {}


	public HouseTeleportRequestMessage InitHouseTeleportRequestMessage(int HouseId, int HouseInstanceId)
	{
		this.HouseId = HouseId;
		this.HouseInstanceId = HouseInstanceId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.HouseId);
		writer.WriteInt(this.HouseInstanceId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.HouseId = reader.ReadVarInt();
		this.HouseInstanceId = reader.ReadInt();
	}
}
}
