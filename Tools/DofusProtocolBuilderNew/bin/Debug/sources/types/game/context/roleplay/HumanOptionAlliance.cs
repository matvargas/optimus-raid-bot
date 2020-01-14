using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class HumanOptionAlliance : HumanOption
{

	public const uint Id = 425;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public AllianceInformations AllianceInformations { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Aggressable { get; set; }

	public HumanOptionAlliance() {}


	public HumanOptionAlliance InitHumanOptionAlliance(AllianceInformations AllianceInformations, byte Aggressable)
	{
		this.AllianceInformations = AllianceInformations;
		this.Aggressable = Aggressable;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		this.AllianceInformations.Serialize(writer);
		writer.WriteByte(this.Aggressable);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.AllianceInformations = new AllianceInformations();
		this.AllianceInformations.Deserialize(reader);
		this.Aggressable = reader.ReadByte();
	}
}
}
