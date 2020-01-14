using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DareListMessage : NetworkMessage
{

	public const uint Id = 6661;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DareInformations[] Dares { get; set; }

	public DareListMessage() {}


	public DareListMessage InitDareListMessage(DareInformations[] Dares)
	{
		this.Dares = Dares;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Dares.Length);
		foreach (DareInformations item in this.Dares)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int DaresLen = reader.ReadShort();
		Dares = new DareInformations[DaresLen];
		for (int i = 0; i < DaresLen; i++)
		{
			this.Dares[i] = new DareInformations();
			this.Dares[i].Deserialize(reader);
		}
	}
}
}
