using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DareInformationsMessage : NetworkMessage
{

	public const uint Id = 6656;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DareInformations DareFixedInfos { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DareVersatileInformations DareVersatilesInfos { get; set; }

	public DareInformationsMessage() {}


	public DareInformationsMessage InitDareInformationsMessage(DareInformations DareFixedInfos, DareVersatileInformations DareVersatilesInfos)
	{
		this.DareFixedInfos = DareFixedInfos;
		this.DareVersatilesInfos = DareVersatilesInfos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.DareFixedInfos.Serialize(writer);
		this.DareVersatilesInfos.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DareFixedInfos = new DareInformations();
		this.DareFixedInfos.Deserialize(reader);
		this.DareVersatilesInfos = new DareVersatileInformations();
		this.DareVersatilesInfos.Deserialize(reader);
	}
}
}
