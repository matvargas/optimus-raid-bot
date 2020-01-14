using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class ChangeMapMessage : NetworkMessage
{

	public const uint Id = 221;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double MapId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Autopilot { get; set; }

	public ChangeMapMessage() {}


	public ChangeMapMessage InitChangeMapMessage(double MapId, bool Autopilot)
	{
		this.MapId = MapId;
		this.Autopilot = Autopilot;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.MapId);
		writer.WriteBoolean(this.Autopilot);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MapId = reader.ReadDouble();
		this.Autopilot = reader.ReadBoolean();
	}
}
}
