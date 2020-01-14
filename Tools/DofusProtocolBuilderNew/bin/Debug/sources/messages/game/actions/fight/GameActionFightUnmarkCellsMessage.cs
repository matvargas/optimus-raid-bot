using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightUnmarkCellsMessage : AbstractGameActionMessage
{

	public const uint Id = 5570;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MarkId { get; set; }

	public GameActionFightUnmarkCellsMessage() {}


	public GameActionFightUnmarkCellsMessage InitGameActionFightUnmarkCellsMessage(short MarkId)
	{
		this.MarkId = MarkId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.MarkId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.MarkId = reader.ReadShort();
	}
}
}
