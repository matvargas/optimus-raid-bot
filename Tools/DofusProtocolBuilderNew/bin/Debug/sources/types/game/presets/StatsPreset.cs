using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class StatsPreset : Preset
{

	public const uint Id = 521;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public SimpleCharacterCharacteristicForPreset[] Stats { get; set; }

	public StatsPreset() {}


	public StatsPreset InitStatsPreset(SimpleCharacterCharacteristicForPreset[] Stats)
	{
		this.Stats = Stats;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.Stats.Length);
		foreach (SimpleCharacterCharacteristicForPreset item in this.Stats)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		int StatsLen = reader.ReadShort();
		Stats = new SimpleCharacterCharacteristicForPreset[StatsLen];
		for (int i = 0; i < StatsLen; i++)
		{
			this.Stats[i] = new SimpleCharacterCharacteristicForPreset();
			this.Stats[i].Deserialize(reader);
		}
	}
}
}
