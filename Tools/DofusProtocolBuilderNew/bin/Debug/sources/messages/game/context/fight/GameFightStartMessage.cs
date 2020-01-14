using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameFightStartMessage : NetworkMessage
{

	public const uint Id = 712;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public Idol[] Idols { get; set; }

	public GameFightStartMessage() {}


	public GameFightStartMessage InitGameFightStartMessage(Idol[] Idols)
	{
		this.Idols = Idols;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Idols.Length);
		foreach (Idol item in this.Idols)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int IdolsLen = reader.ReadShort();
		Idols = new Idol[IdolsLen];
		for (int i = 0; i < IdolsLen; i++)
		{
			this.Idols[i] = new Idol();
			this.Idols[i].Deserialize(reader);
		}
	}
}
}
