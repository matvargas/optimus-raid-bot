using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CharacterSelectedSuccessMessage : NetworkMessage
{

	public const uint Id = 153;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterBaseInformations Infos { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool IsCollectingStats { get; set; }

	public CharacterSelectedSuccessMessage() {}


	public CharacterSelectedSuccessMessage InitCharacterSelectedSuccessMessage(CharacterBaseInformations Infos, bool IsCollectingStats)
	{
		this.Infos = Infos;
		this.IsCollectingStats = IsCollectingStats;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		this.Infos.Serialize(writer);
		writer.WriteBoolean(this.IsCollectingStats);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.Infos = new CharacterBaseInformations();
		this.Infos.Deserialize(reader);
		this.IsCollectingStats = reader.ReadBoolean();
	}
}
}
