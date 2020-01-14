using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class FinishMoveListMessage : NetworkMessage
{

	public const uint Id = 6704;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public FinishMoveInformations[] FinishMoves { get; set; }

	public FinishMoveListMessage() {}


	public FinishMoveListMessage InitFinishMoveListMessage(FinishMoveInformations[] FinishMoves)
	{
		this.FinishMoves = FinishMoves;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.FinishMoves.Length);
		foreach (FinishMoveInformations item in this.FinishMoves)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int FinishMovesLen = reader.ReadShort();
		FinishMoves = new FinishMoveInformations[FinishMovesLen];
		for (int i = 0; i < FinishMovesLen; i++)
		{
			this.FinishMoves[i] = new FinishMoveInformations();
			this.FinishMoves[i].Deserialize(reader);
		}
	}
}
}
