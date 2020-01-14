using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class AllianceFactSheetInformations : AllianceInformations
{

	public const uint Id = 421;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int CreationDate { get; set; }

	public AllianceFactSheetInformations() {}


	public AllianceFactSheetInformations InitAllianceFactSheetInformations(int CreationDate)
	{
		this.CreationDate = CreationDate;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteInt(this.CreationDate);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.CreationDate = reader.ReadInt();
	}
}
}
