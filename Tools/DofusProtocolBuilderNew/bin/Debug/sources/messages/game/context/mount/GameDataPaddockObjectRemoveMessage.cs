using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameDataPaddockObjectRemoveMessage : NetworkMessage
{

	public const uint Id = 5993;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CellId { get; set; }

	public GameDataPaddockObjectRemoveMessage() {}


	public GameDataPaddockObjectRemoveMessage InitGameDataPaddockObjectRemoveMessage(short CellId)
	{
		this.CellId = CellId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.CellId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CellId = reader.ReadVarShort();
	}
}
}
