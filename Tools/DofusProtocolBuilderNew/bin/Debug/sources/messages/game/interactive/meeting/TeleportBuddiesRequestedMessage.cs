using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TeleportBuddiesRequestedMessage : NetworkMessage
{

	public const uint Id = 6302;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DungeonId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long InviterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long[] InvalidBuddiesIds { get; set; }

	public TeleportBuddiesRequestedMessage() {}


	public TeleportBuddiesRequestedMessage InitTeleportBuddiesRequestedMessage(short DungeonId, long InviterId, long[] InvalidBuddiesIds)
	{
		this.DungeonId = DungeonId;
		this.InviterId = InviterId;
		this.InvalidBuddiesIds = InvalidBuddiesIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.DungeonId);
		writer.WriteVarLong(this.InviterId);
		writer.WriteShort(this.InvalidBuddiesIds.Length);
		foreach (long item in this.InvalidBuddiesIds)
		{
			writer.WriteVarLong(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DungeonId = reader.ReadVarShort();
		this.InviterId = reader.ReadVarLong();
		int InvalidBuddiesIdsLen = reader.ReadShort();
		InvalidBuddiesIds = new long[InvalidBuddiesIdsLen];
		for (int i = 0; i < InvalidBuddiesIdsLen; i++)
		{
			this.InvalidBuddiesIds[i] = reader.ReadVarLong();
		}
	}
}
}
