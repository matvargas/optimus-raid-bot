using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class QuestStepInfoMessage : NetworkMessage
{

	public const uint Id = 5625;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public QuestActiveInformations Infos { get; set; }

	public QuestStepInfoMessage() {}


	public QuestStepInfoMessage InitQuestStepInfoMessage(QuestActiveInformations Infos)
	{
		this.Infos = Infos;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
writer.WriteShort(Infos.MessageId);
		Infos.Serialize(writer);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
this.Infos = ProtocolTypeManager.GetInstance<QuestActiveInformations>(reader.ReadShort());
		this.Infos.Deserialize(reader);
	}
}
}
