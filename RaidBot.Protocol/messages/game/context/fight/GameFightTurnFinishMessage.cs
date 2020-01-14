using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightTurnFinishMessage : NetworkMessage
{

	public const uint Id = 718;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsAfk { get; set; }

	public GameFightTurnFinishMessage() {}


	public GameFightTurnFinishMessage InitGameFightTurnFinishMessage(bool IsAfk)
	{
		this.IsAfk = IsAfk;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteBoolean(this.IsAfk);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.IsAfk = reader.ReadBoolean();
	}
}
}
