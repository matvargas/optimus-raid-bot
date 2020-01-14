using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class TeleportToBuddyOfferMessage : NetworkMessage
{

	public const uint Id = 6287;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DungeonId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long BuddyId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int TimeLeft { get; set; }

	public TeleportToBuddyOfferMessage() {}


	public TeleportToBuddyOfferMessage InitTeleportToBuddyOfferMessage(short DungeonId, long BuddyId, int TimeLeft)
	{
		this.DungeonId = DungeonId;
		this.BuddyId = BuddyId;
		this.TimeLeft = TimeLeft;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.DungeonId);
		writer.WriteVarLong(this.BuddyId);
		writer.WriteVarInt(this.TimeLeft);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DungeonId = reader.ReadVarShort();
		this.BuddyId = reader.ReadVarLong();
		this.TimeLeft = reader.ReadVarInt();
	}
}
}
