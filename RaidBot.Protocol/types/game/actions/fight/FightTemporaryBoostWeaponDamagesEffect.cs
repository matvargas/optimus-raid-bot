using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class FightTemporaryBoostWeaponDamagesEffect : FightTemporaryBoostEffect
{

	public const uint Id = 211;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WeaponTypeId { get; set; }

	public FightTemporaryBoostWeaponDamagesEffect() {}


	public FightTemporaryBoostWeaponDamagesEffect InitFightTemporaryBoostWeaponDamagesEffect(short WeaponTypeId)
	{
		this.WeaponTypeId = WeaponTypeId;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		base.Serialize(writer);
		writer.WriteShort(this.WeaponTypeId);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		base.Deserialize(reader);
		this.WeaponTypeId = reader.ReadShort();
	}
}
}
