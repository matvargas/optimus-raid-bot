using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class CharacterExperienceGainMessage : NetworkMessage
{

	public const uint Id = 6321;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long ExperienceCharacter { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long ExperienceMount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long ExperienceGuild { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public long ExperienceIncarnation { get; set; }

	public CharacterExperienceGainMessage() {}


	public CharacterExperienceGainMessage InitCharacterExperienceGainMessage(long ExperienceCharacter, long ExperienceMount, long ExperienceGuild, long ExperienceIncarnation)
	{
		this.ExperienceCharacter = ExperienceCharacter;
		this.ExperienceMount = ExperienceMount;
		this.ExperienceGuild = ExperienceGuild;
		this.ExperienceIncarnation = ExperienceIncarnation;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarLong(this.ExperienceCharacter);
		writer.WriteVarLong(this.ExperienceMount);
		writer.WriteVarLong(this.ExperienceGuild);
		writer.WriteVarLong(this.ExperienceIncarnation);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.ExperienceCharacter = reader.ReadVarLong();
		this.ExperienceMount = reader.ReadVarLong();
		this.ExperienceGuild = reader.ReadVarLong();
		this.ExperienceIncarnation = reader.ReadVarLong();
	}
}
}
