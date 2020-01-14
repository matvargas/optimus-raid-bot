using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightChangeLookMessage : AbstractGameActionMessage
{

	public const uint Id = 5532;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double TargetId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public EntityLook EntityLook { get; set; }

	public GameActionFightChangeLookMessage() {}


	public GameActionFightChangeLookMessage InitGameActionFightChangeLookMessage(double TargetId, EntityLook EntityLook)
	{
		this.TargetId = TargetId;
		this.EntityLook = EntityLook;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteDouble(this.TargetId);
		this.EntityLook.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.TargetId = reader.ReadDouble();
		this.EntityLook = new EntityLook();
		this.EntityLook.Deserialize(reader);
	}
}
}
