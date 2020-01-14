using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HumanOptionEmote : HumanOption
{

	public const uint Id = 407;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte EmoteId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double EmoteStartTime { get; set; }

	public HumanOptionEmote() {}


	public HumanOptionEmote InitHumanOptionEmote(byte EmoteId, double EmoteStartTime)
	{
		this.EmoteId = EmoteId;
		this.EmoteStartTime = EmoteStartTime;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.EmoteId);
		writer.WriteDouble(this.EmoteStartTime);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.EmoteId = reader.ReadByte();
		this.EmoteStartTime = reader.ReadDouble();
	}
}
}
