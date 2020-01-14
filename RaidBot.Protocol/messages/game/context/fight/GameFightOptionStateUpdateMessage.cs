using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightOptionStateUpdateMessage : NetworkMessage
{

	public const uint Id = 5927;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TeamId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Option { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool State { get; set; }

	public GameFightOptionStateUpdateMessage() {}


	public GameFightOptionStateUpdateMessage InitGameFightOptionStateUpdateMessage(short FightId, byte TeamId, byte Option, bool State)
	{
		this.FightId = FightId;
		this.TeamId = TeamId;
		this.Option = Option;
		this.State = State;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.FightId);
		writer.WriteByte(this.TeamId);
		writer.WriteByte(this.Option);
		writer.WriteBoolean(this.State);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FightId = reader.ReadVarShort();
		this.TeamId = reader.ReadByte();
		this.Option = reader.ReadByte();
		this.State = reader.ReadBoolean();
	}
}
}
