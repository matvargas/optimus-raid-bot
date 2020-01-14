using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class QuestObjectiveInformations : NetworkType
{

	public const uint Id = 385;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ObjectiveId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool ObjectiveStatus { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public String[] DialogParams { get; set; }

	public QuestObjectiveInformations() {}


	public QuestObjectiveInformations InitQuestObjectiveInformations(short ObjectiveId, bool ObjectiveStatus, String[] DialogParams)
	{
		this.ObjectiveId = ObjectiveId;
		this.ObjectiveStatus = ObjectiveStatus;
		this.DialogParams = DialogParams;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.ObjectiveId);
		writer.WriteBoolean(this.ObjectiveStatus);
		writer.WriteShort(this.DialogParams.Length);
		foreach (String item in this.DialogParams)
		{
			writer.WriteUTF(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ObjectiveId = reader.ReadVarShort();
		this.ObjectiveStatus = reader.ReadBoolean();
		int DialogParamsLen = reader.ReadShort();
		DialogParams = new String[DialogParamsLen];
		for (int i = 0; i < DialogParamsLen; i++)
		{
			this.DialogParams[i] = reader.ReadUTF();
		}
	}
}
}
