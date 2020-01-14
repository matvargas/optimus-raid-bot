using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Messages
{
public class GameRefreshMonsterBoostsMessage : NetworkMessage
{

	public const uint Id = 6618;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public MonsterBoosts[] MonsterBoosts { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public MonsterBoosts[] FamilyBoosts { get; set; }

	public GameRefreshMonsterBoostsMessage() {}


	public GameRefreshMonsterBoostsMessage InitGameRefreshMonsterBoostsMessage(MonsterBoosts[] MonsterBoosts, MonsterBoosts[] FamilyBoosts)
	{
		this.MonsterBoosts = MonsterBoosts;
		this.FamilyBoosts = FamilyBoosts;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteShort(this.MonsterBoosts.Length);
		foreach (MonsterBoosts item in this.MonsterBoosts)
		{
			item.Serialize(writer);
		}
		writer.WriteShort(this.FamilyBoosts.Length);
		foreach (MonsterBoosts item in this.FamilyBoosts)
		{
			item.Serialize(writer);
		}
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		int MonsterBoostsLen = reader.ReadShort();
		MonsterBoosts = new MonsterBoosts[MonsterBoostsLen];
		for (int i = 0; i < MonsterBoostsLen; i++)
		{
			this.MonsterBoosts[i] = new MonsterBoosts();
			this.MonsterBoosts[i].Deserialize(reader);
		}
		int FamilyBoostsLen = reader.ReadShort();
		FamilyBoosts = new MonsterBoosts[FamilyBoostsLen];
		for (int i = 0; i < FamilyBoostsLen; i++)
		{
			this.FamilyBoosts[i] = new MonsterBoosts();
			this.FamilyBoosts[i].Deserialize(reader);
		}
	}
}
}
