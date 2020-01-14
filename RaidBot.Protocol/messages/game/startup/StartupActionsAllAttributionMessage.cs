using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class StartupActionsAllAttributionMessage : NetworkMessage
{

	public const uint Id = 6537;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long CharacterId { get; set; }

	public StartupActionsAllAttributionMessage() {}


	public StartupActionsAllAttributionMessage InitStartupActionsAllAttributionMessage(long CharacterId)
	{
		this.CharacterId = CharacterId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.CharacterId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CharacterId = reader.ReadVarLong();
	}
}
}
