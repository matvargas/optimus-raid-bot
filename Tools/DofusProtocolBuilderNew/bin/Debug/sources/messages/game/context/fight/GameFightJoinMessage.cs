using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightJoinMessage : NetworkMessage
{

	public const uint Id = 702;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsTeamPhase { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CanBeCancelled { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool CanSayReady { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsFightStarted { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short TimeMaxBeforeFightStart { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte FightType { get; set; }

	public GameFightJoinMessage() {}


	public GameFightJoinMessage InitGameFightJoinMessage(bool IsTeamPhase, bool CanBeCancelled, bool CanSayReady, bool IsFightStarted, short TimeMaxBeforeFightStart, byte FightType)
	{
		this.IsTeamPhase = IsTeamPhase;
		this.CanBeCancelled = CanBeCancelled;
		this.CanSayReady = CanSayReady;
		this.IsFightStarted = IsFightStarted;
		this.TimeMaxBeforeFightStart = TimeMaxBeforeFightStart;
		this.FightType = FightType;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		byte box = 0;
		box = BooleanByteWrapper.SetFlag(box, 0, IsTeamPhase);
		box = BooleanByteWrapper.SetFlag(box, 1, CanBeCancelled);
		box = BooleanByteWrapper.SetFlag(box, 2, CanSayReady);
		box = BooleanByteWrapper.SetFlag(box, 3, IsFightStarted);
		writer.WriteByte(box);
		writer.WriteShort(this.TimeMaxBeforeFightStart);
		writer.WriteByte(this.FightType);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		byte box = reader.ReadByte();
		this.IsTeamPhase = BooleanByteWrapper.GetFlag(box, 0);
		this.CanBeCancelled = BooleanByteWrapper.GetFlag(box, 1);
		this.CanSayReady = BooleanByteWrapper.GetFlag(box, 2);
		this.IsFightStarted = BooleanByteWrapper.GetFlag(box, 3);
		this.TimeMaxBeforeFightStart = reader.ReadShort();
		this.FightType = reader.ReadByte();
	}
}
}
