using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PaddockInformations : NetworkType
{

	public const uint Id = 132;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MaxOutdoorMount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MaxItems { get; set; }

	public PaddockInformations() {}


	public PaddockInformations InitPaddockInformations(short MaxOutdoorMount, short MaxItems)
	{
		this.MaxOutdoorMount = MaxOutdoorMount;
		this.MaxItems = MaxItems;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.MaxOutdoorMount);
		writer.WriteVarShort(this.MaxItems);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.MaxOutdoorMount = reader.ReadVarShort();
		this.MaxItems = reader.ReadVarShort();
	}
}
}
