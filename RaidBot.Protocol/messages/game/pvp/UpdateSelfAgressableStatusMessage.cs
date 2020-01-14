using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class UpdateSelfAgressableStatusMessage : NetworkMessage
{

	public const uint Id = 6456;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Status { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ProbationTime { get; set; }

	public UpdateSelfAgressableStatusMessage() {}


	public UpdateSelfAgressableStatusMessage InitUpdateSelfAgressableStatusMessage(byte Status, int ProbationTime)
	{
		this.Status = Status;
		this.ProbationTime = ProbationTime;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.Status);
		writer.WriteInt(this.ProbationTime);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Status = reader.ReadByte();
		this.ProbationTime = reader.ReadInt();
	}
}
}
