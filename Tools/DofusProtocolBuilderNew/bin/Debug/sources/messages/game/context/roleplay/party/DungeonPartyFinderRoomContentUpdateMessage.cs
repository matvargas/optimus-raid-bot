using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DungeonPartyFinderRoomContentUpdateMessage : NetworkMessage
{

	public const uint Id = 6250;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DungeonId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public DungeonPartyFinderPlayer[] AddedPlayers { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long[] RemovedPlayersIds { get; set; }

	public DungeonPartyFinderRoomContentUpdateMessage() {}


	public DungeonPartyFinderRoomContentUpdateMessage InitDungeonPartyFinderRoomContentUpdateMessage(short DungeonId, DungeonPartyFinderPlayer[] AddedPlayers, long[] RemovedPlayersIds)
	{
		this.DungeonId = DungeonId;
		this.AddedPlayers = AddedPlayers;
		this.RemovedPlayersIds = RemovedPlayersIds;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.DungeonId);
		writer.WriteShort(this.AddedPlayers.Length);
		foreach (DungeonPartyFinderPlayer item in this.AddedPlayers)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.RemovedPlayersIds.Length);
		foreach (long item in this.RemovedPlayersIds)
		{
			writer.WriteVarLong(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DungeonId = reader.ReadVarShort();
		int AddedPlayersLen = reader.ReadShort();
		AddedPlayers = new DungeonPartyFinderPlayer[AddedPlayersLen];
		for (int i = 0; i < AddedPlayersLen; i++)
		{
			this.AddedPlayers[i] = new DungeonPartyFinderPlayer();
			this.AddedPlayers[i].Deserialize(reader);
		}
		int RemovedPlayersIdsLen = reader.ReadShort();
		RemovedPlayersIds = new long[RemovedPlayersIdsLen];
		for (int i = 0; i < RemovedPlayersIdsLen; i++)
		{
			this.RemovedPlayersIds[i] = reader.ReadVarLong();
		}
	}
}
}
