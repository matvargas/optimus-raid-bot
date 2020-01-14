using System.Collections.Generic;
using System;
using System.Linq;
using RaidBot.Protocol.Types;
using RaidBot.Protocol.Messages;
using RaidBot.Common.IO;
using System.ComponentModel;

namespace RaidBot.Protocol.Types
{
public class GameFightMinimalStats : NetworkType
{

	public const uint Id = 31;
	public override uint MessageId { get { return Id; } }

[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int LifePoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int MaxLifePoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int BaseMaxLifePoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int PermanentDamagePercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public int ShieldPoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short ActionPoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MaxActionPoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MovementPoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MaxMovementPoints { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public double Summoner { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public bool Summoned { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NeutralElementResistPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short EarthElementResistPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WaterElementResistPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short AirElementResistPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FireElementResistPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short NeutralElementReduction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short EarthElementReduction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WaterElementReduction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short AirElementReduction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FireElementReduction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short CriticalDamageFixedResist { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PushDamageFixedResist { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PvpNeutralElementResistPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PvpEarthElementResistPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PvpWaterElementResistPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PvpAirElementResistPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PvpFireElementResistPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PvpNeutralElementReduction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PvpEarthElementReduction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PvpWaterElementReduction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PvpAirElementReduction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short PvpFireElementReduction { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DodgePALostProbability { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short DodgePMLostProbability { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short TackleBlock { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short TackleEvade { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short FixedDamageReflection { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public byte InvisibilityState { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short MeleeDamageReceivedPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short RangedDamageReceivedPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short WeaponDamageReceivedPercent { get; set; }
[TypeConverter(typeof(ExpandableObjectConverter))]
[EditorBrowsable(EditorBrowsableState.Always)]
	public short SpellDamageReceivedPercent { get; set; }

	public GameFightMinimalStats() {}


	public GameFightMinimalStats InitGameFightMinimalStats(int LifePoints, int MaxLifePoints, int BaseMaxLifePoints, int PermanentDamagePercent, int ShieldPoints, short ActionPoints, short MaxActionPoints, short MovementPoints, short MaxMovementPoints, double Summoner, bool Summoned, short NeutralElementResistPercent, short EarthElementResistPercent, short WaterElementResistPercent, short AirElementResistPercent, short FireElementResistPercent, short NeutralElementReduction, short EarthElementReduction, short WaterElementReduction, short AirElementReduction, short FireElementReduction, short CriticalDamageFixedResist, short PushDamageFixedResist, short PvpNeutralElementResistPercent, short PvpEarthElementResistPercent, short PvpWaterElementResistPercent, short PvpAirElementResistPercent, short PvpFireElementResistPercent, short PvpNeutralElementReduction, short PvpEarthElementReduction, short PvpWaterElementReduction, short PvpAirElementReduction, short PvpFireElementReduction, short DodgePALostProbability, short DodgePMLostProbability, short TackleBlock, short TackleEvade, short FixedDamageReflection, byte InvisibilityState, short MeleeDamageReceivedPercent, short RangedDamageReceivedPercent, short WeaponDamageReceivedPercent, short SpellDamageReceivedPercent)
	{
		this.LifePoints = LifePoints;
		this.MaxLifePoints = MaxLifePoints;
		this.BaseMaxLifePoints = BaseMaxLifePoints;
		this.PermanentDamagePercent = PermanentDamagePercent;
		this.ShieldPoints = ShieldPoints;
		this.ActionPoints = ActionPoints;
		this.MaxActionPoints = MaxActionPoints;
		this.MovementPoints = MovementPoints;
		this.MaxMovementPoints = MaxMovementPoints;
		this.Summoner = Summoner;
		this.Summoned = Summoned;
		this.NeutralElementResistPercent = NeutralElementResistPercent;
		this.EarthElementResistPercent = EarthElementResistPercent;
		this.WaterElementResistPercent = WaterElementResistPercent;
		this.AirElementResistPercent = AirElementResistPercent;
		this.FireElementResistPercent = FireElementResistPercent;
		this.NeutralElementReduction = NeutralElementReduction;
		this.EarthElementReduction = EarthElementReduction;
		this.WaterElementReduction = WaterElementReduction;
		this.AirElementReduction = AirElementReduction;
		this.FireElementReduction = FireElementReduction;
		this.CriticalDamageFixedResist = CriticalDamageFixedResist;
		this.PushDamageFixedResist = PushDamageFixedResist;
		this.PvpNeutralElementResistPercent = PvpNeutralElementResistPercent;
		this.PvpEarthElementResistPercent = PvpEarthElementResistPercent;
		this.PvpWaterElementResistPercent = PvpWaterElementResistPercent;
		this.PvpAirElementResistPercent = PvpAirElementResistPercent;
		this.PvpFireElementResistPercent = PvpFireElementResistPercent;
		this.PvpNeutralElementReduction = PvpNeutralElementReduction;
		this.PvpEarthElementReduction = PvpEarthElementReduction;
		this.PvpWaterElementReduction = PvpWaterElementReduction;
		this.PvpAirElementReduction = PvpAirElementReduction;
		this.PvpFireElementReduction = PvpFireElementReduction;
		this.DodgePALostProbability = DodgePALostProbability;
		this.DodgePMLostProbability = DodgePMLostProbability;
		this.TackleBlock = TackleBlock;
		this.TackleEvade = TackleEvade;
		this.FixedDamageReflection = FixedDamageReflection;
		this.InvisibilityState = InvisibilityState;
		this.MeleeDamageReceivedPercent = MeleeDamageReceivedPercent;
		this.RangedDamageReceivedPercent = RangedDamageReceivedPercent;
		this.WeaponDamageReceivedPercent = WeaponDamageReceivedPercent;
		this.SpellDamageReceivedPercent = SpellDamageReceivedPercent;
		return (this);
	}

	public override void Serialize(ICustomDataWriter writer)
	{
		writer.WriteVarInt(this.LifePoints);
		writer.WriteVarInt(this.MaxLifePoints);
		writer.WriteVarInt(this.BaseMaxLifePoints);
		writer.WriteVarInt(this.PermanentDamagePercent);
		writer.WriteVarInt(this.ShieldPoints);
		writer.WriteVarShort(this.ActionPoints);
		writer.WriteVarShort(this.MaxActionPoints);
		writer.WriteVarShort(this.MovementPoints);
		writer.WriteVarShort(this.MaxMovementPoints);
		writer.WriteDouble(this.Summoner);
		writer.WriteBoolean(this.Summoned);
		writer.WriteVarShort(this.NeutralElementResistPercent);
		writer.WriteVarShort(this.EarthElementResistPercent);
		writer.WriteVarShort(this.WaterElementResistPercent);
		writer.WriteVarShort(this.AirElementResistPercent);
		writer.WriteVarShort(this.FireElementResistPercent);
		writer.WriteVarShort(this.NeutralElementReduction);
		writer.WriteVarShort(this.EarthElementReduction);
		writer.WriteVarShort(this.WaterElementReduction);
		writer.WriteVarShort(this.AirElementReduction);
		writer.WriteVarShort(this.FireElementReduction);
		writer.WriteVarShort(this.CriticalDamageFixedResist);
		writer.WriteVarShort(this.PushDamageFixedResist);
		writer.WriteVarShort(this.PvpNeutralElementResistPercent);
		writer.WriteVarShort(this.PvpEarthElementResistPercent);
		writer.WriteVarShort(this.PvpWaterElementResistPercent);
		writer.WriteVarShort(this.PvpAirElementResistPercent);
		writer.WriteVarShort(this.PvpFireElementResistPercent);
		writer.WriteVarShort(this.PvpNeutralElementReduction);
		writer.WriteVarShort(this.PvpEarthElementReduction);
		writer.WriteVarShort(this.PvpWaterElementReduction);
		writer.WriteVarShort(this.PvpAirElementReduction);
		writer.WriteVarShort(this.PvpFireElementReduction);
		writer.WriteVarShort(this.DodgePALostProbability);
		writer.WriteVarShort(this.DodgePMLostProbability);
		writer.WriteVarShort(this.TackleBlock);
		writer.WriteVarShort(this.TackleEvade);
		writer.WriteVarShort(this.FixedDamageReflection);
		writer.WriteByte(this.InvisibilityState);
		writer.WriteVarShort(this.MeleeDamageReceivedPercent);
		writer.WriteVarShort(this.RangedDamageReceivedPercent);
		writer.WriteVarShort(this.WeaponDamageReceivedPercent);
		writer.WriteVarShort(this.SpellDamageReceivedPercent);
	}

	public override void Deserialize(ICustomDataReader reader)
	{
		this.LifePoints = reader.ReadVarInt();
		this.MaxLifePoints = reader.ReadVarInt();
		this.BaseMaxLifePoints = reader.ReadVarInt();
		this.PermanentDamagePercent = reader.ReadVarInt();
		this.ShieldPoints = reader.ReadVarInt();
		this.ActionPoints = reader.ReadVarShort();
		this.MaxActionPoints = reader.ReadVarShort();
		this.MovementPoints = reader.ReadVarShort();
		this.MaxMovementPoints = reader.ReadVarShort();
		this.Summoner = reader.ReadDouble();
		this.Summoned = reader.ReadBoolean();
		this.NeutralElementResistPercent = reader.ReadVarShort();
		this.EarthElementResistPercent = reader.ReadVarShort();
		this.WaterElementResistPercent = reader.ReadVarShort();
		this.AirElementResistPercent = reader.ReadVarShort();
		this.FireElementResistPercent = reader.ReadVarShort();
		this.NeutralElementReduction = reader.ReadVarShort();
		this.EarthElementReduction = reader.ReadVarShort();
		this.WaterElementReduction = reader.ReadVarShort();
		this.AirElementReduction = reader.ReadVarShort();
		this.FireElementReduction = reader.ReadVarShort();
		this.CriticalDamageFixedResist = reader.ReadVarShort();
		this.PushDamageFixedResist = reader.ReadVarShort();
		this.PvpNeutralElementResistPercent = reader.ReadVarShort();
		this.PvpEarthElementResistPercent = reader.ReadVarShort();
		this.PvpWaterElementResistPercent = reader.ReadVarShort();
		this.PvpAirElementResistPercent = reader.ReadVarShort();
		this.PvpFireElementResistPercent = reader.ReadVarShort();
		this.PvpNeutralElementReduction = reader.ReadVarShort();
		this.PvpEarthElementReduction = reader.ReadVarShort();
		this.PvpWaterElementReduction = reader.ReadVarShort();
		this.PvpAirElementReduction = reader.ReadVarShort();
		this.PvpFireElementReduction = reader.ReadVarShort();
		this.DodgePALostProbability = reader.ReadVarShort();
		this.DodgePMLostProbability = reader.ReadVarShort();
		this.TackleBlock = reader.ReadVarShort();
		this.TackleEvade = reader.ReadVarShort();
		this.FixedDamageReflection = reader.ReadVarShort();
		this.InvisibilityState = reader.ReadByte();
		this.MeleeDamageReceivedPercent = reader.ReadVarShort();
		this.RangedDamageReceivedPercent = reader.ReadVarShort();
		this.WeaponDamageReceivedPercent = reader.ReadVarShort();
		this.SpellDamageReceivedPercent = reader.ReadVarShort();
	}
}
}
