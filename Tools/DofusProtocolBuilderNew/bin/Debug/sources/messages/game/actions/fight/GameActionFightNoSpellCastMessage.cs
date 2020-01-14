using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameActionFightNoSpellCastMessage : NetworkMessage
{

	public const uint Id = 6132;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int SpellLevelId { get; set; }

	public GameActionFightNoSpellCastMessage() {}


	public GameActionFightNoSpellCastMessage InitGameActionFightNoSpellCastMessage(int SpellLevelId)
	{
		this.SpellLevelId = SpellLevelId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.SpellLevelId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SpellLevelId = reader.ReadVarInt();
	}
}
}
