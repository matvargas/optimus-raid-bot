using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class PrismFightersInformation : NetworkType
{

	public const uint Id = 443;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SubAreaId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public ProtectedEntityWaitingForHelpInfo WaitingForHelpInfo { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterMinimalPlusLookInformations[] AllyCharactersInformations { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public CharacterMinimalPlusLookInformations[] EnemyCharactersInformations { get; set; }

	public PrismFightersInformation() {}


	public PrismFightersInformation InitPrismFightersInformation(short SubAreaId, ProtectedEntityWaitingForHelpInfo WaitingForHelpInfo, CharacterMinimalPlusLookInformations[] AllyCharactersInformations, CharacterMinimalPlusLookInformations[] EnemyCharactersInformations)
	{
		this.SubAreaId = SubAreaId;
		this.WaitingForHelpInfo = WaitingForHelpInfo;
		this.AllyCharactersInformations = AllyCharactersInformations;
		this.EnemyCharactersInformations = EnemyCharactersInformations;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarShort(this.SubAreaId);
		this.WaitingForHelpInfo.Serialize(writer);
		writer.WriteShort(this.AllyCharactersInformations.Length);
		foreach (CharacterMinimalPlusLookInformations item in this.AllyCharactersInformations)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
		writer.WriteShort(this.EnemyCharactersInformations.Length);
		foreach (CharacterMinimalPlusLookInformations item in this.EnemyCharactersInformations)
		{
			writer.WriteShort(item.MessageId);
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.SubAreaId = reader.ReadVarShort();
		this.WaitingForHelpInfo = new ProtectedEntityWaitingForHelpInfo();
		this.WaitingForHelpInfo.Deserialize(reader);
		int AllyCharactersInformationsLen = reader.ReadShort();
		AllyCharactersInformations = new CharacterMinimalPlusLookInformations[AllyCharactersInformationsLen];
		for (int i = 0; i < AllyCharactersInformationsLen; i++)
		{
			this.AllyCharactersInformations[i] = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>(reader.ReadShort());
			this.AllyCharactersInformations[i].Deserialize(reader);
		}
		int EnemyCharactersInformationsLen = reader.ReadShort();
		EnemyCharactersInformations = new CharacterMinimalPlusLookInformations[EnemyCharactersInformationsLen];
		for (int i = 0; i < EnemyCharactersInformationsLen; i++)
		{
			this.EnemyCharactersInformations[i] = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>(reader.ReadShort());
			this.EnemyCharactersInformations[i].Deserialize(reader);
		}
	}
}
}
