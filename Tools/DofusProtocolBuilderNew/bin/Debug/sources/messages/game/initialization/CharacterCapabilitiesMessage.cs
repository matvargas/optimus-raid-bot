using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CharacterCapabilitiesMessage : NetworkMessage
{

	public const uint Id = 6339;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int GuildEmblemSymbolCategories { get; set; }

	public CharacterCapabilitiesMessage() {}


	public CharacterCapabilitiesMessage InitCharacterCapabilitiesMessage(int GuildEmblemSymbolCategories)
	{
		this.GuildEmblemSymbolCategories = GuildEmblemSymbolCategories;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.GuildEmblemSymbolCategories);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.GuildEmblemSymbolCategories = reader.ReadVarInt();
	}
}
}
