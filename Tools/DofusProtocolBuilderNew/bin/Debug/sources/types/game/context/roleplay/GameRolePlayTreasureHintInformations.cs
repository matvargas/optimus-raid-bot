using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameRolePlayTreasureHintInformations : GameRolePlayActorInformations
{

	public const uint Id = 471;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NpcId { get; set; }

	public GameRolePlayTreasureHintInformations() {}


	public GameRolePlayTreasureHintInformations InitGameRolePlayTreasureHintInformations(short NpcId)
	{
		this.NpcId = NpcId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.NpcId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.NpcId = reader.ReadVarShort();
	}
}
}
