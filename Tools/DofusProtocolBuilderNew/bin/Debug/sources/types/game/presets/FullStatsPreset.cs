using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FullStatsPreset : Preset
{

	public const uint Id = 532;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterCharacteristicForPreset[] Stats { get; set; }

	public FullStatsPreset() {}


	public FullStatsPreset InitFullStatsPreset(CharacterCharacteristicForPreset[] Stats)
	{
		this.Stats = Stats;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Stats.Length);
		foreach (CharacterCharacteristicForPreset item in this.Stats)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int StatsLen = reader.ReadShort();
		Stats = new CharacterCharacteristicForPreset[StatsLen];
		for (int i = 0; i < StatsLen; i++)
		{
			this.Stats[i] = new CharacterCharacteristicForPreset();
			this.Stats[i].Deserialize(reader);
		}
	}
}
}
