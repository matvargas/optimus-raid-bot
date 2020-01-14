using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DareCreatedListMessage : NetworkMessage
{

	public const uint Id = 6663;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DareInformations[] DaresFixedInfos { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DareVersatileInformations[] DaresVersatilesInfos { get; set; }

	public DareCreatedListMessage() {}


	public DareCreatedListMessage InitDareCreatedListMessage(DareInformations[] DaresFixedInfos, DareVersatileInformations[] DaresVersatilesInfos)
	{
		this.DaresFixedInfos = DaresFixedInfos;
		this.DaresVersatilesInfos = DaresVersatilesInfos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.DaresFixedInfos.Length);
		foreach (DareInformations item in this.DaresFixedInfos)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.DaresVersatilesInfos.Length);
		foreach (DareVersatileInformations item in this.DaresVersatilesInfos)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int DaresFixedInfosLen = reader.ReadShort();
		DaresFixedInfos = new DareInformations[DaresFixedInfosLen];
		for (int i = 0; i < DaresFixedInfosLen; i++)
		{
			this.DaresFixedInfos[i] = new DareInformations();
			this.DaresFixedInfos[i].Deserialize(reader);
		}
		int DaresVersatilesInfosLen = reader.ReadShort();
		DaresVersatilesInfos = new DareVersatileInformations[DaresVersatilesInfosLen];
		for (int i = 0; i < DaresVersatilesInfosLen; i++)
		{
			this.DaresVersatilesInfos[i] = new DareVersatileInformations();
			this.DaresVersatilesInfos[i].Deserialize(reader);
		}
	}
}
}
