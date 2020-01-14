using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightPauseMessage : NetworkMessage
{

	public const uint Id = 6754;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsPaused { get; set; }

	public GameFightPauseMessage() {}


	public GameFightPauseMessage InitGameFightPauseMessage(bool IsPaused)
	{
		this.IsPaused = IsPaused;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.IsPaused);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.IsPaused = reader.ReadBoolean();
	}
}
}
