using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightRemoveTeamMemberMessage : NetworkMessage
{

	public const uint Id = 711;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TeamId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double CharId { get; set; }

	public GameFightRemoveTeamMemberMessage() {}


	public GameFightRemoveTeamMemberMessage InitGameFightRemoveTeamMemberMessage(short FightId, byte TeamId, double CharId)
	{
		this.FightId = FightId;
		this.TeamId = TeamId;
		this.CharId = CharId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.FightId);
		writer.WriteByte(this.TeamId);
		writer.WriteDouble(this.CharId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FightId = reader.ReadVarShort();
		this.TeamId = reader.ReadByte();
		this.CharId = reader.ReadDouble();
	}
}
}
