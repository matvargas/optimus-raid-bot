using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameFightMonsterInformations : GameFightAIInformations
{

	public const uint Id = 29;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CreatureGenericId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte CreatureGrade { get; set; }

	public GameFightMonsterInformations() {}


	public GameFightMonsterInformations InitGameFightMonsterInformations(short CreatureGenericId, byte CreatureGrade)
	{
		this.CreatureGenericId = CreatureGenericId;
		this.CreatureGrade = CreatureGrade;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.CreatureGenericId);
		writer.WriteByte(this.CreatureGrade);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.CreatureGenericId = reader.ReadVarShort();
		this.CreatureGrade = reader.ReadByte();
	}
}
}
