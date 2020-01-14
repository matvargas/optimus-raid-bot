using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class UpdateMapPlayersAgressableStatusMessage : NetworkMessage
{

	public const uint Id = 6454;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long[] PlayerIds { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte[] Enable { get; set; }

	public UpdateMapPlayersAgressableStatusMessage() {}


	public UpdateMapPlayersAgressableStatusMessage InitUpdateMapPlayersAgressableStatusMessage(long[] PlayerIds, byte[] Enable)
	{
		this.PlayerIds = PlayerIds;
		this.Enable = Enable;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.PlayerIds.Length);
		foreach (long item in this.PlayerIds)
		{
			writer.WriteVarLong(item);
		}
		writer.WriteShort(this.Enable.Length);
		foreach (byte item in this.Enable)
		{
			writer.WriteByte(item);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int PlayerIdsLen = reader.ReadShort();
		PlayerIds = new long[PlayerIdsLen];
		for (int i = 0; i < PlayerIdsLen; i++)
		{
			this.PlayerIds[i] = reader.ReadVarLong();
		}
		int EnableLen = reader.ReadShort();
		Enable = new byte[EnableLen];
		for (int i = 0; i < EnableLen; i++)
		{
			this.Enable[i] = reader.ReadByte();
		}
	}
}
}
