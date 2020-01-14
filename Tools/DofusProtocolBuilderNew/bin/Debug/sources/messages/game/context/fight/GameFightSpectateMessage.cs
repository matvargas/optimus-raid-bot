using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightSpectateMessage : NetworkMessage
{

	public const uint Id = 6069;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FightDispellableEffectExtendedInformations[] Effects { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameActionMark[] Marks { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short GameTurn { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int FightStart { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public Idol[] Idols { get; set; }

	public GameFightSpectateMessage() {}


	public GameFightSpectateMessage InitGameFightSpectateMessage(FightDispellableEffectExtendedInformations[] Effects, GameActionMark[] Marks, short GameTurn, int FightStart, Idol[] Idols)
	{
		this.Effects = Effects;
		this.Marks = Marks;
		this.GameTurn = GameTurn;
		this.FightStart = FightStart;
		this.Idols = Idols;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Effects.Length);
		foreach (FightDispellableEffectExtendedInformations item in this.Effects)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.Marks.Length);
		foreach (GameActionMark item in this.Marks)
		{
			item.Serialize(writer);
		}
		writer.WriteVarShort(this.GameTurn);
		writer.WriteInt(this.FightStart);
		writer.WriteShort(this.Idols.Length);
		foreach (Idol item in this.Idols)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int EffectsLen = reader.ReadShort();
		Effects = new FightDispellableEffectExtendedInformations[EffectsLen];
		for (int i = 0; i < EffectsLen; i++)
		{
			this.Effects[i] = new FightDispellableEffectExtendedInformations();
			this.Effects[i].Deserialize(reader);
		}
		int MarksLen = reader.ReadShort();
		Marks = new GameActionMark[MarksLen];
		for (int i = 0; i < MarksLen; i++)
		{
			this.Marks[i] = new GameActionMark();
			this.Marks[i].Deserialize(reader);
		}
		this.GameTurn = reader.ReadVarShort();
		this.FightStart = reader.ReadInt();
		int IdolsLen = reader.ReadShort();
		Idols = new Idol[IdolsLen];
		for (int i = 0; i < IdolsLen; i++)
		{
			this.Idols[i] = new Idol();
			this.Idols[i].Deserialize(reader);
		}
	}
}
}
