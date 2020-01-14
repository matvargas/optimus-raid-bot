using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameRolePlayMutantInformations : GameRolePlayHumanoidInformations
{

	public const uint Id = 3;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MonsterId { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte PowerLevel { get; set; }

	public GameRolePlayMutantInformations() {}


	public GameRolePlayMutantInformations InitGameRolePlayMutantInformations(short MonsterId, byte PowerLevel)
	{
		this.MonsterId = MonsterId;
		this.PowerLevel = PowerLevel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteVarShort(this.MonsterId);
		writer.WriteByte(this.PowerLevel);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.MonsterId = reader.ReadVarShort();
		this.PowerLevel = reader.ReadByte();
	}
}
}
