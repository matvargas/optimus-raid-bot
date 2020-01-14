using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class StartupActionsObjetAttributionMessage : NetworkMessage
{

	public const uint Id = 1303;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ActionId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long CharacterId { get; set; }

	public StartupActionsObjetAttributionMessage() {}


	public StartupActionsObjetAttributionMessage InitStartupActionsObjetAttributionMessage(int ActionId, long CharacterId)
	{
		this.ActionId = ActionId;
		this.CharacterId = CharacterId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.ActionId);
		writer.WriteVarLong(this.CharacterId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ActionId = reader.ReadInt();
		this.CharacterId = reader.ReadVarLong();
	}
}
}
