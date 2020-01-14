using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TeleportToBuddyCloseMessage : NetworkMessage
{

	public const uint Id = 6303;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DungeonId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long BuddyId { get; set; }

	public TeleportToBuddyCloseMessage() {}


	public TeleportToBuddyCloseMessage InitTeleportToBuddyCloseMessage(short DungeonId, long BuddyId)
	{
		this.DungeonId = DungeonId;
		this.BuddyId = BuddyId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.DungeonId);
		writer.WriteVarLong(this.BuddyId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DungeonId = reader.ReadVarShort();
		this.BuddyId = reader.ReadVarLong();
	}
}
}
