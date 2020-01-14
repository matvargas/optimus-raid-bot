using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ProtectedEntityWaitingForHelpInfo : NetworkType
{

	public const uint Id = 186;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int TimeLeftBeforeFight { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int WaitTimeForPlacement { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NbPositionForDefensors { get; set; }

	public ProtectedEntityWaitingForHelpInfo() {}


	public ProtectedEntityWaitingForHelpInfo InitProtectedEntityWaitingForHelpInfo(int TimeLeftBeforeFight, int WaitTimeForPlacement, byte NbPositionForDefensors)
	{
		this.TimeLeftBeforeFight = TimeLeftBeforeFight;
		this.WaitTimeForPlacement = WaitTimeForPlacement;
		this.NbPositionForDefensors = NbPositionForDefensors;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.TimeLeftBeforeFight);
		writer.WriteInt(this.WaitTimeForPlacement);
		writer.WriteByte(this.NbPositionForDefensors);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.TimeLeftBeforeFight = reader.ReadInt();
		this.WaitTimeForPlacement = reader.ReadInt();
		this.NbPositionForDefensors = reader.ReadByte();
	}
}
}
