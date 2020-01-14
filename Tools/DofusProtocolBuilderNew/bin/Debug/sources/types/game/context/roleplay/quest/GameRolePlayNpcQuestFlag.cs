using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameRolePlayNpcQuestFlag : NetworkType
{

	public const uint Id = 384;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] QuestsToValidId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] QuestsToStartId { get; set; }

	public GameRolePlayNpcQuestFlag() {}


	public GameRolePlayNpcQuestFlag InitGameRolePlayNpcQuestFlag(short[] QuestsToValidId, short[] QuestsToStartId)
	{
		this.QuestsToValidId = QuestsToValidId;
		this.QuestsToStartId = QuestsToStartId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.QuestsToValidId.Length);
		foreach (short item in this.QuestsToValidId)
		{
			writer.WriteVarShort(item);
		}
		writer.WriteShort(this.QuestsToStartId.Length);
		foreach (short item in this.QuestsToStartId)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int QuestsToValidIdLen = reader.ReadShort();
		QuestsToValidId = new short[QuestsToValidIdLen];
		for (int i = 0; i < QuestsToValidIdLen; i++)
		{
			this.QuestsToValidId[i] = reader.ReadVarShort();
		}
		int QuestsToStartIdLen = reader.ReadShort();
		QuestsToStartId = new short[QuestsToStartIdLen];
		for (int i = 0; i < QuestsToStartIdLen; i++)
		{
			this.QuestsToStartId[i] = reader.ReadVarShort();
		}
	}
}
}
