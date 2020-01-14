using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class ObjectEffectLadder : ObjectEffectCreature
{

	public const uint Id = 81;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MonsterCount { get; set; }

	public ObjectEffectLadder() {}


	public ObjectEffectLadder InitObjectEffectLadder(int MonsterCount)
	{
		this.MonsterCount = MonsterCount;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarInt(this.MonsterCount);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.MonsterCount = reader.ReadVarInt();
	}
}
}
