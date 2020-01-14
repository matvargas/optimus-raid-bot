using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class CharacterHardcoreOrEpicInformations : CharacterBaseInformations
{

	public const uint Id = 474;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte DeathState { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DeathCount { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DeathMaxLevel { get; set; }

	public CharacterHardcoreOrEpicInformations() {}


	public CharacterHardcoreOrEpicInformations InitCharacterHardcoreOrEpicInformations(byte DeathState, short DeathCount, short DeathMaxLevel)
	{
		this.DeathState = DeathState;
		this.DeathCount = DeathCount;
		this.DeathMaxLevel = DeathMaxLevel;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteByte(this.DeathState);
		writer.WriteVarShort(this.DeathCount);
		writer.WriteVarShort(this.DeathMaxLevel);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.DeathState = reader.ReadByte();
		this.DeathCount = reader.ReadVarShort();
		this.DeathMaxLevel = reader.ReadVarShort();
	}
}
}
