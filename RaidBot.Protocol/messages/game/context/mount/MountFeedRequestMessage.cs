using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class MountFeedRequestMessage : NetworkMessage
{

	public const uint Id = 6189;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MountUid { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte MountLocation { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MountFoodUid { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Quantity { get; set; }

	public MountFeedRequestMessage() {}


	public MountFeedRequestMessage InitMountFeedRequestMessage(int MountUid, byte MountLocation, int MountFoodUid, int Quantity)
	{
		this.MountUid = MountUid;
		this.MountLocation = MountLocation;
		this.MountFoodUid = MountFoodUid;
		this.Quantity = Quantity;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.MountUid);
		writer.WriteByte(this.MountLocation);
		writer.WriteVarInt(this.MountFoodUid);
		writer.WriteVarInt(this.Quantity);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MountUid = reader.ReadVarInt();
		this.MountLocation = reader.ReadByte();
		this.MountFoodUid = reader.ReadVarInt();
		this.Quantity = reader.ReadVarInt();
	}
}
}
