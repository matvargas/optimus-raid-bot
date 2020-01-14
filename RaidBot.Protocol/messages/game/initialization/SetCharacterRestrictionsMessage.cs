using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class SetCharacterRestrictionsMessage : NetworkMessage
{

	public const uint Id = 170;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double ActorId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ActorRestrictionsInformations Restrictions { get; set; }

	public SetCharacterRestrictionsMessage() {}


	public SetCharacterRestrictionsMessage InitSetCharacterRestrictionsMessage(double ActorId, ActorRestrictionsInformations Restrictions)
	{
		this.ActorId = ActorId;
		this.Restrictions = Restrictions;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.ActorId);
		this.Restrictions.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ActorId = reader.ReadDouble();
		this.Restrictions = new ActorRestrictionsInformations();
		this.Restrictions.Deserialize(reader);
	}
}
}
