using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GuestModeMessage : NetworkMessage
{

	public const uint Id = 6505;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Active { get; set; }

	public GuestModeMessage() {}


	public GuestModeMessage InitGuestModeMessage(bool Active)
	{
		this.Active = Active;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.Active);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Active = reader.ReadBoolean();
	}
}
}
