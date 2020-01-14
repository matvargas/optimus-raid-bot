using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class QuestObjectiveInformationsWithCompletion : QuestObjectiveInformations
{

	public const uint Id = 386;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CurCompletion { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MaxCompletion { get; set; }

	public QuestObjectiveInformationsWithCompletion() {}


	public QuestObjectiveInformationsWithCompletion InitQuestObjectiveInformationsWithCompletion(short CurCompletion, short MaxCompletion)
	{
		this.CurCompletion = CurCompletion;
		this.MaxCompletion = MaxCompletion;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.CurCompletion);
		writer.WriteVarShort(this.MaxCompletion);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.CurCompletion = reader.ReadVarShort();
		this.MaxCompletion = reader.ReadVarShort();
	}
}
}
