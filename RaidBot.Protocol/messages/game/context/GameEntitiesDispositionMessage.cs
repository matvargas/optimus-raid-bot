using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameEntitiesDispositionMessage : NetworkMessage
{

	public const uint Id = 5696;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public IdentifiedEntityDispositionInformations[] Dispositions { get; set; }

	public GameEntitiesDispositionMessage() {}


	public GameEntitiesDispositionMessage InitGameEntitiesDispositionMessage(IdentifiedEntityDispositionInformations[] Dispositions)
	{
		this.Dispositions = Dispositions;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.Dispositions.Length);
		foreach (IdentifiedEntityDispositionInformations item in this.Dispositions)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int DispositionsLen = reader.ReadShort();
		Dispositions = new IdentifiedEntityDispositionInformations[DispositionsLen];
		for (int i = 0; i < DispositionsLen; i++)
		{
			this.Dispositions[i] = new IdentifiedEntityDispositionInformations();
			this.Dispositions[i].Deserialize(reader);
		}
	}
}
}
