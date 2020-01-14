using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class NpcGenericActionRequestMessage : NetworkMessage
{

	public const uint Id = 5898;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int NpcId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NpcActionId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double NpcMapId { get; set; }

	public NpcGenericActionRequestMessage() {}


	public NpcGenericActionRequestMessage InitNpcGenericActionRequestMessage(int NpcId, byte NpcActionId, double NpcMapId)
	{
		this.NpcId = NpcId;
		this.NpcActionId = NpcActionId;
		this.NpcMapId = NpcMapId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.NpcId);
		writer.WriteByte(this.NpcActionId);
		writer.WriteDouble(this.NpcMapId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.NpcId = reader.ReadInt();
		this.NpcActionId = reader.ReadByte();
		this.NpcMapId = reader.ReadDouble();
	}
}
}
