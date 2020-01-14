using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameDataPlayFarmObjectAnimationMessage : NetworkMessage
{

	public const uint Id = 6026;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short[] CellId { get; set; }

	public GameDataPlayFarmObjectAnimationMessage() {}


	public GameDataPlayFarmObjectAnimationMessage InitGameDataPlayFarmObjectAnimationMessage(short[] CellId)
	{
		this.CellId = CellId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.CellId.Length);
		foreach (short item in this.CellId)
		{
			writer.WriteVarShort(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int CellIdLen = reader.ReadShort();
		CellId = new short[CellIdLen];
		for (int i = 0; i < CellIdLen; i++)
		{
			this.CellId[i] = reader.ReadVarShort();
		}
	}
}
}
