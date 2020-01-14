using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightHumanReadyStateMessage : NetworkMessage
{

	public const uint Id = 740;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long CharacterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsReady { get; set; }

	public GameFightHumanReadyStateMessage() {}


	public GameFightHumanReadyStateMessage InitGameFightHumanReadyStateMessage(long CharacterId, bool IsReady)
	{
		this.CharacterId = CharacterId;
		this.IsReady = IsReady;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.CharacterId);
		writer.WriteBoolean(this.IsReady);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CharacterId = reader.ReadVarLong();
		this.IsReady = reader.ReadBoolean();
	}
}
}
