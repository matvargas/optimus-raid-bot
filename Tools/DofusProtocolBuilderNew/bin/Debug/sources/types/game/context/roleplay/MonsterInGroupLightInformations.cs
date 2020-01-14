using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class MonsterInGroupLightInformations : NetworkType
{

	public const uint Id = 395;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int CreatureGenericId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte Grade { get; set; }

	public MonsterInGroupLightInformations() {}


	public MonsterInGroupLightInformations InitMonsterInGroupLightInformations(int CreatureGenericId, byte Grade)
	{
		this.CreatureGenericId = CreatureGenericId;
		this.Grade = Grade;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteInt(this.CreatureGenericId);
		writer.WriteByte(this.Grade);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.CreatureGenericId = reader.ReadInt();
		this.Grade = reader.ReadByte();
	}
}
}
