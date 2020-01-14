using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class DareVersatileInformations : NetworkType
{

	public const uint Id = 504;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double DareId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int CountEntrants { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int CountWinners { get; set; }

	public DareVersatileInformations() {}


	public DareVersatileInformations InitDareVersatileInformations(double DareId, int CountEntrants, int CountWinners)
	{
		this.DareId = DareId;
		this.CountEntrants = CountEntrants;
		this.CountWinners = CountWinners;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteDouble(this.DareId);
		writer.WriteInt(this.CountEntrants);
		writer.WriteInt(this.CountWinners);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DareId = reader.ReadDouble();
		this.CountEntrants = reader.ReadInt();
		this.CountWinners = reader.ReadInt();
	}
}
}
