using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class LockableStateUpdateStorageMessage : LockableStateUpdateAbstractMessage
{

	public const uint Id = 5669;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MapId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ElementId { get; set; }

	public LockableStateUpdateStorageMessage() {}


	public LockableStateUpdateStorageMessage InitLockableStateUpdateStorageMessage(double MapId, int ElementId)
	{
		this.MapId = MapId;
		this.ElementId = ElementId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.MapId);
		writer.WriteVarInt(this.ElementId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.MapId = reader.ReadDouble();
		this.ElementId = reader.ReadVarInt();
	}
}
}
