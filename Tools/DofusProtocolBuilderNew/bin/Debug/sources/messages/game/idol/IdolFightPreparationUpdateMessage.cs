using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class IdolFightPreparationUpdateMessage : NetworkMessage
{

	public const uint Id = 6586;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte IdolSource { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public Idol[] Idols { get; set; }

	public IdolFightPreparationUpdateMessage() {}


	public IdolFightPreparationUpdateMessage InitIdolFightPreparationUpdateMessage(byte IdolSource, Idol[] Idols)
	{
		this.IdolSource = IdolSource;
		this.Idols = Idols;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteByte(this.IdolSource);
		writer.WriteShort(this.Idols.Length);
		foreach (Idol item in this.Idols)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.IdolSource = reader.ReadByte();
		int IdolsLen = reader.ReadShort();
		Idols = new Idol[IdolsLen];
		for (int i = 0; i < IdolsLen; i++)
		{
			this.Idols[i] = ProtocolTypeManager.GetInstance<Idol>(reader.ReadShort());
			this.Idols[i].Deserialize(reader);
		}
	}
}
}
