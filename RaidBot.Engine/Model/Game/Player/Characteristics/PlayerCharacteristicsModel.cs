using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidBot.Engine.Dispatcher;
using RaidBot.Engine.Enums;
using RaidBot.Engine.Manager;
using RaidBot.Protocol.Enums;
using System.Reflection;
using RaidBot.Data.IO.D2I;
using RaidBot.Protocol.Messages;
using RaidBot.Protocol.Types;
namespace RaidBot.Engine.Model.Game.Player.Characteristics
{
     public class PlayerCharacteristicsModel:ModelBase
    {
        
         public PlayerCharacteristicsModel(ConnectedHost host)
        {
            host.Dispatcher.Register(this);
        }
        private ulong m_experience;

        public virtual ulong Experience
        {
            get
            {
                return m_experience;
            }
            set
            {
                m_experience = value;
                Notify();
            }
        }

        private ulong m_experienceLevelFloor;

        public virtual ulong ExperienceLevelFloor
        {
            get
            {
                return m_experienceLevelFloor;
            }
            set
            {
                m_experienceLevelFloor = value;
                Notify();
            }
        }

        private ulong m_experienceNextLevelFloor;

        public virtual ulong ExperienceNextLevelFloor
        {
            get
            {
                return m_experienceNextLevelFloor;
            }
            set
            {
                m_experienceNextLevelFloor = value;
                Notify();
            }
        }

        private int m_kamas;

        public virtual int Kamas
        {
            get
            {
                return m_kamas;

            }
            set
            {
                m_kamas = value;
                Notify();
            }
        }

        private ushort m_statsPoints;

        public virtual ushort StatsPoints
        {
            get
            {
                return m_statsPoints;
            }
            set
            {
                m_statsPoints = value;
                Notify();
            }
        }

        private ushort m_additionnalPoints;

        public virtual ushort AdditionnalPoints
        {
            get
            {
                return m_additionnalPoints;
            }
            set
            {
                m_additionnalPoints = value;
                Notify();
            }
        }

        private ushort m_spellsPoints;

        public virtual ushort SpellsPoints
        {
            get
            {
                return m_spellsPoints;
           
            }
            set
            {
                m_spellsPoints = value;
                Notify();
            }
        }

        private ActorExtendedAlignmentInformations m_alignmentInfos;

        public virtual ActorExtendedAlignmentInformations AlignmentInfos
        {
            get
            {
                return m_alignmentInfos;
            }
            set
            {
                m_alignmentInfos = value;
                Notify();
            }
        }

        private uint m_lifePoints;

        public virtual uint LifePoints
        {
            get
            {
                return m_lifePoints;
            }
            set
            {
                m_lifePoints = value;
                Notify();
            }
        }

        private uint m_maxLifePoints;

        public virtual uint MaxLifePoints
        {
            get
            {
                return m_maxLifePoints;
                
            }
            set
            {
                m_maxLifePoints = value;
                Notify();
            }
        }

        private ushort m_energyPoints;

        public virtual ushort EnergyPoints
        {
            get
            {
                return m_energyPoints;
            }
            set
            {
                m_energyPoints = value;
                Notify();
            }
        }

        private ushort m_maxEnergyPoints;

        public virtual ushort MaxEnergyPoints
        {
            get
            {
                return m_maxEnergyPoints;
            }
            set
            {
                m_maxEnergyPoints = value;
                Notify();
            }
        }

        private short m_actionPointsCurrent;

        public virtual short ActionPointsCurrent
        {
            get
            {
                return m_actionPointsCurrent;
            }
            set
            {
                m_actionPointsCurrent = value;
                Notify();
            }
        }

        private short m_movementPointsCurrent;

        public virtual short MovementPointsCurrent
        {
            get
            {
                return m_movementPointsCurrent;
            }
            set
            {
                m_movementPointsCurrent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_initiative;

        public virtual CharacterBaseCharacteristic Initiative
        {
            get
            {
                return m_initiative;
            }
            set
            {
                m_initiative = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_prospecting;

        public virtual CharacterBaseCharacteristic Prospecting
        {
            get
            {
                return m_prospecting;
            }
            set
            {
                m_prospecting = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_actionPoints;

        public virtual CharacterBaseCharacteristic ActionPoints
        {
            get
            {
                return m_actionPoints;
            }
            set
            {
                m_actionPoints = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_movementPoints;

        public virtual CharacterBaseCharacteristic MovementPoints
        {
            get
            {
                return m_movementPoints;
            }
            set
            {
                m_movementPoints = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_strength;

        public virtual CharacterBaseCharacteristic Strength
        {
            get
            {
                return m_strength;
            }
            set
            {
                m_strength = value;

                Notify();
            }
        }

        private CharacterBaseCharacteristic m_vitality;

        public virtual CharacterBaseCharacteristic Vitality
        {
            get
            {
                return m_vitality;
            }
            set
            {
                m_vitality = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_wisdom;

        public virtual CharacterBaseCharacteristic Wisdom
        {
            get
            {
                return m_wisdom;
            }
            set
            {
                m_wisdom = value;
                Notify();
               

            }
        }

        private CharacterBaseCharacteristic m_chance;

        public virtual CharacterBaseCharacteristic Chance
        {
            get
            {
                return m_chance;
            }
            set
            {
                m_chance = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_agility;

        public virtual CharacterBaseCharacteristic Agility
        {
            get
            {
                return m_agility;
            }
            set
            {
                m_agility = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_intelligence;

        public virtual CharacterBaseCharacteristic Intelligence
        {
            get
            {
                return m_intelligence;
            }
            set
            {
                m_intelligence = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_range;

        public virtual CharacterBaseCharacteristic Range
        {
            get
            {
                return m_range;
            }
            set
            {
                m_range = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_summonableCreaturesBoost;

        public virtual CharacterBaseCharacteristic SummonableCreaturesBoost
        {
            get
            {
                return m_summonableCreaturesBoost;
            }
            set
            {
                m_summonableCreaturesBoost = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_reflect;

        public virtual CharacterBaseCharacteristic Reflect
        {
            get
            {
                return m_reflect;
            }
            set
            {
                m_reflect = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_criticalHit;

        public virtual CharacterBaseCharacteristic CriticalHit
        {
            get
            {
                return m_criticalHit;
            }
            set
            {
                m_criticalHit = value;
                Notify();
            }
        }

        private ushort m_criticalHitWeapon;

        public virtual ushort CriticalHitWeapon
        {
            get
            {
                return m_criticalHitWeapon;
            }
            set
            {
                m_criticalHitWeapon = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_criticalMiss;

        public virtual CharacterBaseCharacteristic CriticalMiss
        {
            get
            {
                return m_criticalMiss;
            }
            set
            {
                m_criticalMiss = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_healBonus;

        public virtual CharacterBaseCharacteristic HealBonus
        {
            get
            {
                return m_healBonus;
            }
            set
            {
                m_healBonus = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_allDamagesBonus;

        public virtual CharacterBaseCharacteristic AllDamagesBonus
        {
            get
            {
                return m_allDamagesBonus;
               
            }
            set
            {
                m_allDamagesBonus = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_weaponDamagesBonusPercent;

        public virtual CharacterBaseCharacteristic WeaponDamagesBonusPercent
        {
            get
            {
                return m_weaponDamagesBonusPercent;
            }
            set
            {
                m_weaponDamagesBonusPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_damagesBonusPercent;

        public virtual CharacterBaseCharacteristic DamagesBonusPercent
        {
            get
            {
                return m_damagesBonusPercent;
            }
            set
            {
                m_damagesBonusPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_trapBonus;

        public virtual CharacterBaseCharacteristic TrapBonus
        {
            get
            {
                return m_trapBonus;
            }
            set
            {
                m_trapBonus = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_trapBonusPercent;

        public virtual CharacterBaseCharacteristic TrapBonusPercent
        {
            get
            {
                return m_trapBonusPercent;
            }
            set
            {
                m_trapBonusPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_glyphBonusPercent;

        public virtual CharacterBaseCharacteristic GlyphBonusPercent
        {
            get
            {
                return m_glyphBonusPercent;
            }
            set
            {
                m_glyphBonusPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_permanentDamagePercent;

        public virtual CharacterBaseCharacteristic PermanentDamagePercent
        {
            get
            {
                return m_permanentDamagePercent;
            }
            set
            {
                m_permanentDamagePercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_tackleBlock;

        public virtual CharacterBaseCharacteristic TackleBlock
        {
            get
            {
                return m_tackleBlock;
            }
            set
            {
                m_tackleBlock = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_tackleEvade;

        public virtual CharacterBaseCharacteristic TackleEvade
        {
            get
            {
                return m_tackleEvade;
            }
            set
            {
                m_tackleEvade = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_PAAttack;

        public virtual CharacterBaseCharacteristic PAAttack
        {
            get
            {
                return m_PAAttack;
            }
            set
            {
                m_PAAttack = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_PMAttack;

        public virtual CharacterBaseCharacteristic PMAttack
        {
            get
            {
                return m_PMAttack;
               
            }
            set
            {
                m_PMAttack = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pushDamageBonus;

        public virtual CharacterBaseCharacteristic PushDamageBonus
        {
            get
            {
                return m_pushDamageBonus;
               
            }
            set
            {
                m_pushDamageBonus = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_criticalDamageBonus;

        public virtual CharacterBaseCharacteristic CriticalDamageBonus
        {
            get
            {
                return m_criticalDamageBonus;
            }
            set
            {
                m_criticalDamageBonus = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_neutralDamageBonus;

        public virtual CharacterBaseCharacteristic NeutralDamageBonus
        {
            get
            {
                return m_neutralDamageBonus;
            }
            set
            {
                m_neutralDamageBonus = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_earthDamageBonus;

        public virtual CharacterBaseCharacteristic EarthDamageBonus
        {
            get
            {
                return m_earthDamageBonus;
            }
            set
            {
                m_earthDamageBonus = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_waterDamageBonus;

        public virtual CharacterBaseCharacteristic WaterDamageBonus
        {
            get
            {
                return m_waterDamageBonus;
            }
            set
            {
                m_waterDamageBonus = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_airDamageBonus;

        public virtual CharacterBaseCharacteristic AirDamageBonus
        {
            get
            {
                return m_airDamageBonus;
            }
            set
            {
                m_airDamageBonus = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_fireDamageBonus;

        public virtual CharacterBaseCharacteristic FireDamageBonus
        {
            get
            {
                return m_fireDamageBonus;
            }
            set
            {
                m_fireDamageBonus = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_dodgePALostProbability;

        public virtual CharacterBaseCharacteristic DodgePALostProbability
        {
            get
            {
                return m_dodgePALostProbability;
            }
            set
            {
                m_dodgePALostProbability = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_dodgePMLostProbability;

        public virtual CharacterBaseCharacteristic DodgePMLostProbability
        {
            get
            {
                return m_dodgePMLostProbability;
            }
            set
            {
                m_dodgePMLostProbability = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_neutralElementResistPercent;

        public virtual CharacterBaseCharacteristic NeutralElementResistPercent
        {
            get
            {
                return m_neutralElementResistPercent;
            }
            set
            {
                m_neutralElementResistPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_earthElementResistPercent;

        public virtual CharacterBaseCharacteristic EarthElementResistPercent
        {
            get
            {
                return m_earthElementResistPercent;
            }
            set
            {
                m_earthElementResistPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_waterElementResistPercent;

        public virtual CharacterBaseCharacteristic WaterElementResistPercent
        {
            get
            {
                return m_waterElementResistPercent;
            }
            set
            {
                m_waterElementResistPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_airElementResistPercent;

        public virtual CharacterBaseCharacteristic AirElementResistPercent
        {
            get
            {
                return m_airElementResistPercent;
            }
            set
            {
                m_airElementResistPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_fireElementResistPercent;

        public virtual CharacterBaseCharacteristic FireElementResistPercent
        {
            get
            {
                return m_fireElementResistPercent;
            }
            set
            {
                m_fireElementResistPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_neutralElementReduction;

        public virtual CharacterBaseCharacteristic NeutralElementReduction
        {
            get
            {
                return m_neutralElementReduction;
            }
            set
            {
                m_neutralElementReduction = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_earthElementReduction;

        public virtual CharacterBaseCharacteristic EarthElementReduction
        {
            get
            {
                return m_earthElementReduction;
            }
            set
            {
                m_earthElementReduction = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_waterElementReduction;

        public virtual CharacterBaseCharacteristic WaterElementReduction
        {
            get
            {
                return m_waterElementReduction;
            }
            set
            {
                m_waterElementReduction = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_airElementReduction;

        public virtual CharacterBaseCharacteristic AirElementReduction
        {
            get
            {
                return m_airElementReduction;
            }
            set
            {
                m_airElementReduction = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_fireElementReduction;

        public virtual CharacterBaseCharacteristic FireElementReduction
        {
            get
            {
                return m_fireElementReduction;
            }
            set
            {
                m_fireElementReduction = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pushDamageReduction;

        public virtual CharacterBaseCharacteristic PushDamageReduction
        {
            get
            {
                return m_pushDamageReduction;
            }
            set
            {
                m_pushDamageReduction = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_criticalDamageReduction;

        public virtual CharacterBaseCharacteristic CriticalDamageReduction
        {
            get
            {
                return m_criticalDamageReduction;
            }
            set
            {
                m_criticalDamageReduction = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pvpNeutralElementResistPercent;

        public virtual CharacterBaseCharacteristic PvpNeutralElementResistPercent
        {
            get
            {
                return m_pvpNeutralElementResistPercent;
            }
            set
            {
                m_pvpNeutralElementResistPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pvpEarthElementResistPercent;

        public virtual CharacterBaseCharacteristic PvpEarthElementResistPercent
        {
            get
            {
                return m_pvpEarthElementResistPercent;
            }
            set
            {
                m_pvpEarthElementResistPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pvpWaterElementResistPercent;

        public virtual CharacterBaseCharacteristic PvpWaterElementResistPercent
        {
            get
            {
                return m_pvpWaterElementResistPercent;
            }
            set
            {
                m_pvpWaterElementResistPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pvpAirElementResistPercent;

        public virtual CharacterBaseCharacteristic PvpAirElementResistPercent
        {
            get
            {
                return m_pvpAirElementResistPercent;
            }
            set
            {
                m_pvpAirElementResistPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pvpFireElementResistPercent;

        public virtual CharacterBaseCharacteristic PvpFireElementResistPercent
        {
            get
            {
                return m_pvpFireElementResistPercent;
            }
            set
            {
                m_pvpFireElementResistPercent = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pvpNeutralElementReduction;

        public virtual CharacterBaseCharacteristic PvpNeutralElementReduction
        {
            get
            {
                return m_pvpNeutralElementReduction;
            }
            set
            {
                m_pvpNeutralElementReduction = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pvpEarthElementReduction;

        public virtual CharacterBaseCharacteristic PvpEarthElementReduction
        {
            get
            {
                return m_pvpEarthElementReduction;
            }
            set
            {
                m_pvpEarthElementReduction = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pvpWaterElementReduction;

        public virtual CharacterBaseCharacteristic PvpWaterElementReduction
        {
            get
            {
                return m_pvpWaterElementReduction;
            }
            set
            {
                m_pvpWaterElementReduction = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pvpAirElementReduction;

        public virtual CharacterBaseCharacteristic PvpAirElementReduction
        {
            get
            {
                return m_pvpAirElementReduction;
            }
            set
            {
                m_pvpAirElementReduction = value;
                Notify();
            }
        }

        private CharacterBaseCharacteristic m_pvpFireElementReduction;

        public virtual CharacterBaseCharacteristic PvpFireElementReduction
        {
            get
            {
                return m_pvpFireElementReduction;
            }
            set
            {
                m_pvpFireElementReduction = value;
                Notify();
            }
        }
      

        private CharacterSpellModification[] m_spellModifications;

        public virtual CharacterSpellModification[] SpellModifications
        {
            get
            {
                return m_spellModifications;
            }
            set
            {
                m_spellModifications = value;
                Notify();
            }
        }

        private int m_probationTime;

        public virtual int ProbationTime
        {
            get
            {
                return m_probationTime;
            }
            set
            {
                m_probationTime = value;
                Notify();
            }
        }
        [MessageHandlerAttribut(typeof(CharacterStatsListMessage))]
        private void HandleCharacterCharacteristicsInformations(CharacterStatsListMessage _message, ConnectedHost source)
        {
            CharacterCharacteristicsInformations message = _message.stats;
            m_experience = message.experience;
            m_experienceLevelFloor = message.experienceLevelFloor;
            m_experienceNextLevelFloor = message.experienceNextLevelFloor;
            m_kamas = message.kamas;
            m_statsPoints = message.statsPoints;
            m_additionnalPoints = message.additionnalPoints;
            m_spellsPoints = message.spellsPoints;
            m_alignmentInfos = message.alignmentInfos;
            m_lifePoints = message.lifePoints;
            m_maxLifePoints = message.maxLifePoints;
            m_energyPoints = message.energyPoints;
            m_maxEnergyPoints = message.maxEnergyPoints;
            m_actionPointsCurrent = message.actionPointsCurrent;
            m_movementPointsCurrent = message.movementPointsCurrent;
            m_initiative = message.initiative;
            m_prospecting = message.prospecting;
            m_actionPoints = message.actionPoints;
            m_movementPoints = message.movementPoints;
            m_strength = message.strength;
            m_vitality = message.vitality;
            m_wisdom = message.wisdom;
            m_chance = message.chance;
            m_agility = message.agility;
            m_intelligence = message.intelligence;
            m_range = message.range;
            m_summonableCreaturesBoost = message.summonableCreaturesBoost;
            m_reflect = message.reflect;
            m_criticalHit = message.criticalHit;
            m_criticalHitWeapon = message.criticalHitWeapon;
            m_criticalMiss = message.criticalMiss;
            m_healBonus = message.healBonus;
            m_allDamagesBonus = message.allDamagesBonus;
            m_weaponDamagesBonusPercent = message.weaponDamagesBonusPercent;
            m_damagesBonusPercent = message.damagesBonusPercent;
            m_trapBonus = message.trapBonus;
            m_trapBonusPercent = message.trapBonusPercent;
            m_glyphBonusPercent = message.glyphBonusPercent;
            m_permanentDamagePercent = message.permanentDamagePercent;
            m_tackleBlock = message.tackleBlock;
            m_tackleEvade = message.tackleEvade;
            m_PAAttack = message.PAAttack;
            m_PMAttack = message.PMAttack;
            m_pushDamageBonus = message.pushDamageBonus;
            m_criticalDamageBonus = message.criticalDamageBonus;
            m_neutralDamageBonus = message.neutralDamageBonus;
            m_earthDamageBonus = message.earthDamageBonus;
            m_waterDamageBonus = message.waterDamageBonus;
            m_airDamageBonus = message.airDamageBonus;
            m_fireDamageBonus = message.fireDamageBonus;
            m_dodgePALostProbability = message.dodgePALostProbability;
            m_dodgePMLostProbability = message.dodgePMLostProbability;
            m_neutralElementResistPercent = message.neutralElementResistPercent;
            m_earthElementResistPercent = message.earthElementResistPercent;
            m_waterElementResistPercent = message.waterElementResistPercent;
            m_airElementResistPercent = message.airElementResistPercent;
            m_fireElementResistPercent = message.fireElementResistPercent;
            m_neutralElementReduction = message.neutralElementReduction;
            m_earthElementReduction = message.earthElementReduction;
            m_waterElementReduction = message.waterElementReduction;
            m_airElementReduction = message.airElementReduction;
            m_fireElementReduction = message.fireElementReduction;
            m_pushDamageReduction = message.pushDamageReduction;
            m_criticalDamageReduction = message.criticalDamageReduction;
            m_pvpNeutralElementResistPercent = message.pvpNeutralElementResistPercent;
            m_pvpEarthElementResistPercent = message.pvpEarthElementResistPercent;
            m_pvpWaterElementResistPercent = message.pvpWaterElementResistPercent;
            m_pvpAirElementResistPercent = message.pvpAirElementResistPercent;
            m_pvpFireElementResistPercent = message.pvpFireElementResistPercent;
            m_pvpNeutralElementReduction = message.pvpNeutralElementReduction;
            m_pvpEarthElementReduction = message.pvpEarthElementReduction;
            m_pvpWaterElementReduction = message.pvpWaterElementReduction;
            m_pvpAirElementReduction = message.pvpAirElementReduction;
            m_pvpFireElementReduction = message.pvpFireElementReduction;
            m_spellModifications = message.spellModifications;
            m_probationTime = message.probationTime;
            OnUpdated();
            
        }
        [MessageHandlerAttribut(typeof(CharacterExperienceGainMessage))]
        private void HandleCharacterCharacteristicsInformations(CharacterExperienceGainMessage message, ConnectedHost source)
        {
            this.Experience = message.experienceCharacter;
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(UpdateLifePointsMessage))]
        private void HandleCharacterCharacteristicsInformations(UpdateLifePointsMessage message, ConnectedHost source)
        {
            this.LifePoints = message.lifePoints;
            this.MaxLifePoints = message.maxLifePoints;
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(InventoryContentMessage))]
        private void HandleCharacterCharacteristicsInformations(InventoryContentMessage message, ConnectedHost source)
        {
            this.Kamas = (int)message.kamas;
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(KamasUpdateMessage))]
        private void HandleCharacterCharacteristicsInformations(KamasUpdateMessage message, ConnectedHost source)
        {
            this.Kamas = message.kamasTotal;
            OnUpdated();
        }

        [MessageHandlerAttribut(typeof(LifePointsRegenEndMessage))]
        private void HandleCharacterCharacteristicsInformations(LifePointsRegenEndMessage message, ConnectedHost source)
        {
            this.LifePoints = message.lifePoints;
            this.MaxLifePoints = message.maxLifePoints;
            OnUpdated();
        }
        [MessageHandlerAttribut(typeof(StatsUpgradeResultMessage))]
        private void HandleCharacterCharacteristicsInformations(StatsUpgradeResultMessage message, ConnectedHost source)
        {
            switch (message.result)
            {
                default:
                    TextDataAdapter.GetText("ui.popup.statboostFailed.text");
                    break;
                case 0:
                    source.logger.Log("Caractéristique augmentée avec succés." , Common.Default.Loging.LogLevelEnum.Succes);
                    break;
                case 1:
                    source.logger.Log(TextDataAdapter.GetText("ui.charSel.deletionErrorUnsecureMode"), Common.Default.Loging.LogLevelEnum.Error);
                    break;
                case 2:
                    source.logger.Log(TextDataAdapter.GetText("ui.fight.guestAccount"), Common.Default.Loging.LogLevelEnum.Error);
                    break;
                case 3:
                    source.logger.Log(TextDataAdapter.GetText("ui.error.cantDoInFight"), Common.Default.Loging.LogLevelEnum.Error);
                    break;
                case 4:
                    source.logger.Log(TextDataAdapter.GetText("ui.popup.statboostFailed.notEnoughPoint"), Common.Default.Loging.LogLevelEnum.Error);
                    break;
            }
            OnUpdated();
        }


       
    }
}
