using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayArenaFighterStatusMessage : NetworkMessage
{

	public const uint Id = 6281;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double PlayerId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Accepted { get; set; }

	public GameRolePlayArenaFighterStatusMessage() {}


	public GameRolePlayArenaFighterStatusMessage InitGameRolePlayArenaFighterStatusMessage(short FightId, double PlayerId, bool Accepted)
	{
		this.FightId = FightId;
		this.PlayerId = PlayerId;
		this.Accepted = Accepted;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.FightId);
		writer.WriteDouble(this.PlayerId);
		writer.WriteBoolean(this.Accepted);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FightId = reader.ReadVarShort();
		this.PlayerId = reader.ReadDouble();
		this.Accepted = reader.ReadBoolean();
	}
}
}
