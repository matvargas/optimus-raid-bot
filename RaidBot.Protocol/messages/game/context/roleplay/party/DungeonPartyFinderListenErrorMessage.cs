using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class DungeonPartyFinderListenErrorMessage : NetworkMessage
{

	public const uint Id = 6248;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DungeonId { get; set; }

	public DungeonPartyFinderListenErrorMessage() {}


	public DungeonPartyFinderListenErrorMessage InitDungeonPartyFinderListenErrorMessage(short DungeonId)
	{
		this.DungeonId = DungeonId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.DungeonId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.DungeonId = reader.ReadVarShort();
	}
}
}
