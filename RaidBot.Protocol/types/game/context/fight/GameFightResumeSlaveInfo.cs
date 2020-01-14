using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameFightResumeSlaveInfo : NetworkType
{

	public const uint Id = 364;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double SlaveId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GameFightSpellCooldown[] SpellCooldowns { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte SummonCount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte BombCount { get; set; }

	public GameFightResumeSlaveInfo() {}


	public GameFightResumeSlaveInfo InitGameFightResumeSlaveInfo(double SlaveId, GameFightSpellCooldown[] SpellCooldowns, byte SummonCount, byte BombCount)
	{
		this.SlaveId = SlaveId;
		this.SpellCooldowns = SpellCooldowns;
		this.SummonCount = SummonCount;
		this.BombCount = BombCount;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.SlaveId);
		writer.WriteShort(this.SpellCooldowns.Length);
		foreach (GameFightSpellCooldown item in this.SpellCooldowns)
		{
			item.Serialize(writer);
		}
		writer.WriteByte(this.SummonCount);
		writer.WriteByte(this.BombCount);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SlaveId = reader.ReadDouble();
		int SpellCooldownsLen = reader.ReadShort();
		SpellCooldowns = new GameFightSpellCooldown[SpellCooldownsLen];
		for (int i = 0; i < SpellCooldownsLen; i++)
		{
			this.SpellCooldowns[i] = new GameFightSpellCooldown();
			this.SpellCooldowns[i].Deserialize(reader);
		}
		this.SummonCount = reader.ReadByte();
		this.BombCount = reader.ReadByte();
	}
}
}
