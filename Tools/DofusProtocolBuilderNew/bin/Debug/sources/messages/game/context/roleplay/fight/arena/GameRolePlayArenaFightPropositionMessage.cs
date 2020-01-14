using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRolePlayArenaFightPropositionMessage : NetworkMessage
{

	public const uint Id = 6276;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FightId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double[] AlliesId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short Duration { get; set; }

	public GameRolePlayArenaFightPropositionMessage() {}


	public GameRolePlayArenaFightPropositionMessage InitGameRolePlayArenaFightPropositionMessage(short FightId, double[] AlliesId, short Duration)
	{
		this.FightId = FightId;
		this.AlliesId = AlliesId;
		this.Duration = Duration;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.FightId);
		writer.WriteShort(this.AlliesId.Length);
		foreach (double item in this.AlliesId)
		{
			writer.WriteDouble(item);
		}
		writer.WriteVarShort(this.Duration);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.FightId = reader.ReadVarShort();
		int AlliesIdLen = reader.ReadShort();
		AlliesId = new double[AlliesIdLen];
		for (int i = 0; i < AlliesIdLen; i++)
		{
			this.AlliesId[i] = reader.ReadDouble();
		}
		this.Duration = reader.ReadVarShort();
	}
}
}
