using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PrismsListMessage : NetworkMessage
{

	public const uint Id = 6440;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public PrismSubareaEmptyInfo[] Prisms { get; set; }

	public PrismsListMessage() {}


	public PrismsListMessage InitPrismsListMessage(PrismSubareaEmptyInfo[] Prisms)
	{
		this.Prisms = Prisms;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Prisms.Length);
		foreach (PrismSubareaEmptyInfo item in this.Prisms)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int PrismsLen = reader.ReadShort();
		Prisms = new PrismSubareaEmptyInfo[PrismsLen];
		for (int i = 0; i < PrismsLen; i++)
		{
			this.Prisms[i] = ProtocolTypeManager.GetInstance<PrismSubareaEmptyInfo>(reader.ReadShort());
			this.Prisms[i].Deserialize(reader);
		}
	}
}
}
