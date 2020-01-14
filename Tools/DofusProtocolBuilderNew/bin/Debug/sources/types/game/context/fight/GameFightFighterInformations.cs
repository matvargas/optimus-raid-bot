using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameFightFighterInformations : GameContextActorInformations
{

	public const uint Id = 143;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte TeamId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Wave { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Alive { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameFightMinimalStats Stats { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] PreviousPositions { get; set; }

	public GameFightFighterInformations() {}


	public GameFightFighterInformations InitGameFightFighterInformations(byte TeamId, byte Wave, bool Alive, GameFightMinimalStats Stats, short[] PreviousPositions)
	{
		this.TeamId = TeamId;
		this.Wave = Wave;
		this.Alive = Alive;
		this.Stats = Stats;
		this.PreviousPositions = PreviousPositions;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.TeamId);
		writer.WriteByte(this.Wave);
		writer.WriteBoolean(this.Alive);
writer.WriteShort(Stats.MessageId);
		Stats.Serialize(writer);
		writer.WriteShort(this.PreviousPositions.Length);
		foreach (short item in this.PreviousPositions)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.TeamId = reader.ReadByte();
		this.Wave = reader.ReadByte();
		this.Alive = reader.ReadBoolean();
this.Stats = ProtocolTypeManager.GetInstance<GameFightMinimalStats>(reader.ReadShort());
		this.Stats.Deserialize(reader);
		int PreviousPositionsLen = reader.ReadShort();
		PreviousPositions = new short[PreviousPositionsLen];
		for (int i = 0; i < PreviousPositionsLen; i++)
		{
			this.PreviousPositions[i] = reader.ReadVarShort();
		}
	}
}
}
