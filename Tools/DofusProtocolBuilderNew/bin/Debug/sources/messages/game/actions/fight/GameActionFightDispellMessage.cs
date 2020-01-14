using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightDispellMessage : AbstractGameActionMessage
{

	public const uint Id = 5533;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double TargetId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool VerboseCast { get; set; }

	public GameActionFightDispellMessage() {}


	public GameActionFightDispellMessage InitGameActionFightDispellMessage(double TargetId, bool VerboseCast)
	{
		this.TargetId = TargetId;
		this.VerboseCast = VerboseCast;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.TargetId);
		writer.WriteBoolean(this.VerboseCast);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.TargetId = reader.ReadDouble();
		this.VerboseCast = reader.ReadBoolean();
	}
}
}
