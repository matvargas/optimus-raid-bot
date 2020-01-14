using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ObjectQuantityMessage : NetworkMessage
{

	public const uint Id = 3023;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ObjectUID { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int Quantity { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Origin { get; set; }

	public ObjectQuantityMessage() {}


	public ObjectQuantityMessage InitObjectQuantityMessage(int ObjectUID, int Quantity, byte Origin)
	{
		this.ObjectUID = ObjectUID;
		this.Quantity = Quantity;
		this.Origin = Origin;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.ObjectUID);
		writer.WriteVarInt(this.Quantity);
		writer.WriteByte(this.Origin);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ObjectUID = reader.ReadVarInt();
		this.Quantity = reader.ReadVarInt();
		this.Origin = reader.ReadByte();
	}
}
}
