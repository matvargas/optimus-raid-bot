using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class PartyInvitationDungeonMessage : PartyInvitationMessage
{

	public const uint Id = 6244;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DungeonId { get; set; }

	public PartyInvitationDungeonMessage() {}


	public PartyInvitationDungeonMessage InitPartyInvitationDungeonMessage(short DungeonId)
	{
		this.DungeonId = DungeonId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.DungeonId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.DungeonId = reader.ReadVarShort();
	}
}
}
