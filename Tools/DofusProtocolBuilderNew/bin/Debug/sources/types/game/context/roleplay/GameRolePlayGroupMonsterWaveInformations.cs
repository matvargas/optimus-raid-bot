using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameRolePlayGroupMonsterWaveInformations : GameRolePlayGroupMonsterInformations
{

	public const uint Id = 464;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte NbWaves { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public GroupMonsterStaticInformations[] Alternatives { get; set; }

	public GameRolePlayGroupMonsterWaveInformations() {}


	public GameRolePlayGroupMonsterWaveInformations InitGameRolePlayGroupMonsterWaveInformations(byte NbWaves, GroupMonsterStaticInformations[] Alternatives)
	{
		this.NbWaves = NbWaves;
		this.Alternatives = Alternatives;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.NbWaves);
		writer.WriteShort(this.Alternatives.Length);
		foreach (GroupMonsterStaticInformations item in this.Alternatives)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.NbWaves = reader.ReadByte();
		int AlternativesLen = reader.ReadShort();
		Alternatives = new GroupMonsterStaticInformations[AlternativesLen];
		for (int i = 0; i < AlternativesLen; i++)
		{
			this.Alternatives[i] = ProtocolTypeManager.GetInstance<GroupMonsterStaticInformations>(reader.ReadShort());
			this.Alternatives[i].Deserialize(reader);
		}
	}
}
}
