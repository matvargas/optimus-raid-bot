using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Protocol.enums
{
    public class ActionIdENum
    {
        public const int ACTION_ENCAPSULATE_BINARY_COMMAND = 993;

        public const int ACTION_ENDS_TURN = -2;

        public const int ACTION_INTERNAL_SEND_ACTION_BUFFER = -1;

        public const int ACTION_NO_OPERATION = 0;

        public const int ACTION_SEQUENCE_START = 83;

        public const int ACTION_SEQUENCE_END = 70;

        public const int ACTION_CHARACTER_MOVEMENT = 1;

        public const int ACTION_CHARACTER_CHANGE_MAP = 2;

        public const int ACTION_CHARACTER_CHANGE_RESPAWN_MAP = 3;

        public const int ACTION_CHARACTER_TELEPORT_ON_SAME_MAP = 4;

        public const int ACTION_CHARACTER_PUSH = 5;

        public const int ACTION_CHARACTER_PULL = 6;

        public const int ACTION_CHARACTER_DIVORCE_WIFE_OR_HUSBAND = 7;

        public const int ACTION_CHARACTER_EXCHANGE_PLACES = 8;

        public const int ACTION_CHARACTER_DODGE_HIT = 9;

        public const int ACTION_CHARACTER_LEARN_EMOTICON = 10;

        public const int ACTION_CHARACTER_SET_DIRECTION = 11;

        public const int ACTION_CHARACTER_CREATE_GUILD = 12;

        public const int ACTION_USE_PUSHPULL_ELEMENT = 14;

        public const int ACTION_AREA_CHANGE_ALIGNMENT = 15;

        public const int ACTION_AREA_GIVE_KAMAS = 16;

        public const int ACTION_SCRIPT_START = 17;

        public const int ACTION_AREA_DUNGEON_ATTACKED = 20;

        public const int ACTION_GAIN_AREA_KAMAS = 21;

        public const int ACTION_AREA_DUNGEON_CITY_OPENED = 23;

        public const int ACTION_AREA_DUNGEON_HEART_OPENED = 24;

        public const int ACTION_AREA_DUNGEON_HEART_CLOSED = 25;

        public const int ACTION_AREA_CHANGE_ALIGNMENT_SUB = 26;

        public const int ACTION_QUEST_OBJECTIVE_VALIDATE = 30;

        public const int ACTION_QUEST_STEP_VALIDATE = 31;

        public const int ACTION_QUEST_QUEST_VALIDATE = 32;

        public const int ACTION_QUEST_STEP_START = 33;

        public const int ACTION_QUEST_START = 34;

        public const int ACTION_QUEST_CHECK_STARTED_OBJECTIVES = 35;

        public const int ACTION_QUEST_RESET = 36;

        public const int ACTION_START_DIALOG_WITH_NPC = 40;

        public const int ACTION_CARRY_CHARACTER = 50;

        public const int ACTION_THROW_CARRIED_CHARACTER = 51;

        public const int ACTION_NO_MORE_CARRIED = 52;

        public const int ACTION_CHARACTER_MOVEMENT_POINTS_STEAL = 77;

        public const int ACTION_CHARACTER_MOVEMENT_POINTS_WIN = 78;

        public const int ACTION_CHARACTER_MULTIPLY_RECEIVED_DAMAGE_OR_GIVE_LIFE_WITH_RATIO = 79;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_FROM_PUSH = 80;

        public const int ACTION_CHARACTER_LIFE_POINTS_WIN_WITHOUT_ELEMENT = 81;

        public const int ACTION_CHARACTER_LIFE_POINTS_STEAL_WITHOUT_BOOST = 82;

        public const int ACTION_CHARACTER_ACTION_POINTS_STEAL = 84;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_CASTER_LIFE_FROM_WATER = 85;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_CASTER_LIFE_FROM_EARTH = 86;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_CASTER_LIFE_FROM_AIR = 87;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_CASTER_LIFE_FROM_FIRE = 88;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_CASTER_LIFE = 89;

        public const int ACTION_CHARACTER_DISPATCH_LIFE_POINTS_PERCENT = 90;

        public const int ACTION_CHARACTER_LIFE_POINTS_STEAL_FROM_WATER = 91;

        public const int ACTION_CHARACTER_LIFE_POINTS_STEAL_FROM_EARTH = 92;

        public const int ACTION_CHARACTER_LIFE_POINTS_STEAL_FROM_AIR = 93;

        public const int ACTION_CHARACTER_LIFE_POINTS_STEAL_FROM_FIRE = 94;

        public const int ACTION_CHARACTER_LIFE_POINTS_STEAL = 95;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_FROM_WATER = 96;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_FROM_EARTH = 97;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_FROM_AIR = 98;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_FROM_FIRE = 99;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST = 100;

        public const int ACTION_CHARACTER_ACTION_POINTS_LOST = 101;

        public const int ACTION_CHARACTER_ACTION_POINTS_USE = 102;

        public const int ACTION_CHARACTER_DEATH = 103;

        public const int ACTION_CHARACTER_ACTION_TACKLED = 104;

        public const int ACTION_CHARACTER_LIFE_LOST_MODERATOR = 105;

        public const int ACTION_CHARACTER_SPELL_REFLECTOR = 106;

        public const int ACTION_CHARACTER_LIFE_LOST_REFLECTOR = 107;

        public const int ACTION_CHARACTER_LIFE_POINTS_WIN = 108;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_CASTER = 109;

        public const int ACTION_CHARACTER_BOOST_LIFE_POINTS = 110;

        public const int ACTION_CHARACTER_BOOST_ACTION_POINTS = 111;

        public const int ACTION_CHARACTER_BOOST_DAMAGES = 112;

        public const int ACTION_CHARACTER_MULTIPLY_DAMAGES = 114;

        public const int ACTION_CHARACTER_BOOST_CRITICAL_HIT = 115;

        public const int ACTION_CHARACTER_DEBOOST_RANGE = 116;

        public const int ACTION_CHARACTER_BOOST_RANGE = 117;

        public const int ACTION_CHARACTER_BOOST_STRENGTH = 118;

        public const int ACTION_CHARACTER_BOOST_AGILITY = 119;

        public const int ACTION_CHARACTER_ACTION_POINTS_WIN = 120;

        public const int ACTION_CHARACTER_BOOST_DAMAGES_FOR_ALL_GAME = 121;

        public const int ACTION_CHARACTER_BOOST_CRITICAL_MISS = 122;

        public const int ACTION_CHARACTER_BOOST_CHANCE = 123;

        public const int ACTION_CHARACTER_BOOST_WISDOM = 124;

        public const int ACTION_CHARACTER_BOOST_VITALITY = 125;

        public const int ACTION_CHARACTER_BOOST_INTELLIGENCE = 126;

        public const int ACTION_CHARACTER_MOVEMENT_POINTS_LOST = 127;

        public const int ACTION_CHARACTER_BOOST_MOVEMENT_POINTS = 128;

        public const int ACTION_CHARACTER_MOVEMENT_POINTS_USE = 129;

        public const int ACTION_CHARACTER_STEAL_GOLD = 130;

        public const int ACTION_CHARACTER_MANA_USE_KILL_LIFE = 131;

        public const int ACTION_CHARACTER_REMOVE_ALL_EFFECTS = 132;

        public const int ACTION_CHARACTER_ACTION_POINTS_LOST_CASTER = 133;

        public const int ACTION_CHARACTER_MOVEMEMT_POINTS_LOST_CASTER = 134;

        public const int ACTION_CHARACTER_DEBOOST_RANGE_CASTER = 135;

        public const int ACTION_CHARACTER_BOOST_RANGE_CASTER = 136;

        public const int ACTION_CHARACTER_BOOST_DAMAGES_CASTER = 137;

        public const int ACTION_CHARACTER_BOOST_DAMAGES_PERCENT = 138;

        public const int ACTION_CHARACTER_DEBOOST_DAMAGES_PERCENT = 186;

        public const int ACTION_CHARACTER_ENERGY_POINTS_WIN = 139;

        public const int ACTION_CHARACTER_PASS_NEXT_TURN = 140;

        public const int ACTION_CHARACTER_KILL = 141;

        public const int ACTION_CHARACTER_BOOST_PHYSICAL_DAMAGES = 142;

        public const int ACTION_CHARACTER_LIFE_POINTS_WIN_WITHOUT_BOOST = 143;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_WITHOUT_BOOST = 144;

        public const int ACTION_CHARACTER_DEBOOST_DAMAGES = 145;

        public const int ACTION_CHARACTER_CURSE = 146;

        public const int ACTION_CHARACTER_RESURECT_ALLY_IN_FIGHT = 147;

        public const int ACTION_CHARACTER_ADD_FOLLOWING_CHARACTER = 148;

        public const int ACTION_CHARACTER_CHANGE_LOOK = 149;

        public const int ACTION_CHARACTER_MAKE_INVISIBLE = 150;

        public const int ACTION_SPELL_INVISIBLE_OBSTACLE = 151;

        public const int ACTION_CHARACTER_DEBOOST_CHANCE = 152;

        public const int ACTION_CHARACTER_DEBOOST_VITALITY = 153;

        public const int ACTION_CHARACTER_DEBOOST_AGILITY = 154;

        public const int ACTION_CHARACTER_DEBOOST_INTELLIGENCE = 155;

        public const int ACTION_CHARACTER_DEBOOST_WISDOM = 156;

        public const int ACTION_CHARACTER_DEBOOST_STRENGTH = 157;

        public const int ACTION_CHARACTER_BOOST_MAXIMUM_WEIGHT = 158;

        public const int ACTION_CHARACTER_DEBOOST_MAXIMUM_WEIGHT = 159;

        public const int ACTION_CHARACTER_BOOST_ACTION_POINTS_LOST_DODGE = 160;

        public const int ACTION_CHARACTER_BOOST_MOVEMENT_POINTS_LOST_DODGE = 161;

        public const int ACTION_CHARACTER_DEBOOST_ACTION_POINTS_LOST_DODGE = 162;

        public const int ACTION_CHARACTER_DEBOOST_MOVEMENT_POINTS_LOST_DODGE = 163;

        public const int ACTION_CHARACTER_BOOST_WEAPON_DAMAGE_PERCENT = 165;

        public const int ACTION_CHARACTER_DEBOOST_ACTION_POINTS = 168;

        public const int ACTION_CHARACTER_DEBOOST_MOVEMENT_POINTS = 169;

        public const int ACTION_CHARACTER_LIFE_POINTS_WIN_IN_RP = 170;

        public const int ACTION_CHARACTER_DEBOOST_CRITICAL_HIT = 171;

        public const int ACTION_CHARACTER_DEBOOST_MAGICAL_REDUCTION = 172;

        public const int ACTION_CHARACTER_DEBOOST_PHYSICAL_REDUCTION = 173;

        public const int ACTION_CHARACTER_BOOST_INITIATIVE = 174;

        public const int ACTION_CHARACTER_DEBOOST_INITIATIVE = 175;

        public const int ACTION_CHARACTER_BOOST_MAGIC_FIND = 176;

        public const int ACTION_CHARACTER_DEBOOST_MAGIC_FIND = 177;

        public const int ACTION_CHARACTER_BOOST_HEAL_BONUS = 178;

        public const int ACTION_CHARACTER_DEBOOST_HEAL_BONUS = 179;

        public const int ACTION_CHARACTER_ADD_DOUBLE = 180;

        public const int ACTION_SUMMON_CREATURE = 181;

        public const int ACTION_CHARACTER_BOOST_MAXIMUM_SUMMONED_CREATURES = 182;

        public const int ACTION_CHARACTER_BOOST_MAGICAL_REDUCTION = 183;

        public const int ACTION_CHARACTER_BOOST_PHYSICAL_REDUCTION = 184;

        public const int ACTION_SUMMON_STATIC_CREATURE = 185;

        public const int ACTION_CHARACTER_ALIGNMENT_RANK_SET = 187;

        public const int ACTION_CHARACTER_ALIGNMENT_SIDE_SET = 188;

        public const int ACTION_CHARACTER_ALIGNMENT_VALUE_SET = 189;

        public const int ACTION_CHARACTER_ALIGNMENT_VALUE_MODIFICATION = 190;

        public const int ACTION_CHARACTER_INVENTORY_CLEAR = 191;

        public const int ACTION_CHARACTER_INVENTORY_REMOVE_ITEM = 192;

        public const int ACTION_CHARACTER_INVENTORY_ADD_ITEM = 193;

        public const int ACTION_CHARACTER_INVENTORY_GAIN_KAMAS = 194;

        public const int ACTION_CHARACTER_INVENTORY_LOSE_KAMAS = 195;

        public const int ACTION_CHARACTER_OPEN_MY_STORAGE = 196;

        public const int ACTION_CHARACTER_TRANSFORM = 197;

        public const int ACTION_CHARACTER_CLEAR_ALL_JOB = 198;

        public const int ACTION_CHARACTER_REPAIR_OBJECT = 199;

        public const int ACTION_DECORS_PLAY_OBJECT_ANIMATION = 200;

        public const int ACTION_DECORS_ADD_OBJECT = 201;

        public const int ACTION_DECORS_REVEAL_UNVISIBLE = 202;

        public const int ACTION_DECORS_OBSTACLE_CLOSE = 203;

        public const int ACTION_DECORS_OBSTACLE_OPEN = 204;

        public const int ACTION_CHARACTER_CHANGE_RESTRICTION = 205;

        public const int ACTION_CHARACTER_RESURRECTION = 206;

        public const int ACTION_COLLECT_RESOURCE = 207;

        public const int ACTION_DECORS_PLAY_ANIMATION = 208;

        public const int ACTION_CHARACTER_INVENTORY_ADD_ITEM_NOCHECK = 209;

        public const int ACTION_CHARACTER_BOOST_EARTH_ELEMENT_PERCENT = 210;

        public const int ACTION_CHARACTER_BOOST_WATER_ELEMENT_PERCENT = 211;

        public const int ACTION_CHARACTER_BOOST_AIR_ELEMENT_PERCENT = 212;

        public const int ACTION_CHARACTER_BOOST_FIRE_ELEMENT_PERCENT = 213;

        public const int ACTION_CHARACTER_BOOST_NEUTRAL_ELEMENT_PERCENT = 214;

        public const int ACTION_CHARACTER_DEBOOST_EARTH_ELEMENT_PERCENT = 215;

        public const int ACTION_CHARACTER_DEBOOST_WATER_ELEMENT_PERCENT = 216;

        public const int ACTION_CHARACTER_DEBOOST_AIR_ELEMENT_PERCENT = 217;

        public const int ACTION_CHARACTER_DEBOOST_FIRE_ELEMENT_PERCENT = 218;

        public const int ACTION_CHARACTER_DEBOOST_NEUTRAL_ELEMENT_PERCENT = 219;

        public const int ACTION_CHARACTER_REFLECTOR_UNBOOSTED = 220;

        public const int ACTION_CHARACTER_INVENTORY_ADD_ITEM_RANDOM_NOCHECK = 221;

        public const int ACTION_CHARACTER_INVENTORY_ADD_ITEM_FROM_RANDOM_DROP = 222;

        public const int ACTION_DECORS_OBSTACLE_CLOSE_RANDOM = 223;

        public const int ACTION_DECORS_OBSTACLE_OPEN_RANDOM = 224;

        public const int ACTION_CHARACTER_BOOST_TRAP = 225;

        public const int ACTION_CHARACTER_BOOST_TRAP_PERCENT = 226;

        public const int ACTION_DECORS_PLAY_ANIMATION_UNLIGHTED = 228;

        public const int ACTION_CHARACTER_GAIN_RIDE = 229;

        public const int ACTION_CHARACTER_ENERGY_LOSS_BOOST = 230;

        public const int ACTION_CHARACTER_ENERGY_POINTS_LOOSE = 231;

        public const int ACTION_CHARACTER_INVENTORY_ADD_ITEM_ON_RP_MAP = 232;

        public const int ACTION_CHARACTER_INVENTORY_REMOVE_ITEM_ON_RP_MAP = 233;

        public const int ACTION_CHARACTER_PLAY_SPELL_ANIMATION = 237;

        public const int ACTION_CHARACTER_BOOST_EARTH_ELEMENT_RESIST = 240;

        public const int ACTION_CHARACTER_BOOST_WATER_ELEMENT_RESIST = 241;

        public const int ACTION_CHARACTER_BOOST_AIR_ELEMENT_RESIST = 242;

        public const int ACTION_CHARACTER_BOOST_FIRE_ELEMENT_RESIST = 243;

        public const int ACTION_CHARACTER_BOOST_NEUTRAL_ELEMENT_RESIST = 244;

        public const int ACTION_CHARACTER_DEBOOST_EARTH_ELEMENT_RESIST = 245;

        public const int ACTION_CHARACTER_DEBOOST_WATER_ELEMENT_RESIST = 246;

        public const int ACTION_CHARACTER_DEBOOST_AIR_ELEMENT_RESIST = 247;

        public const int ACTION_CHARACTER_DEBOOST_FIRE_ELEMENT_RESIST = 248;

        public const int ACTION_CHARACTER_DEBOOST_NEUTRAL_ELEMENT_RESIST = 249;

        public const int ACTION_CHARACTER_BOOST_EARTH_ELEMENT_PVP_PERCENT = 250;

        public const int ACTION_CHARACTER_BOOST_WATER_ELEMENT_PVP_PERCENT = 251;

        public const int ACTION_CHARACTER_BOOST_AIR_ELEMENT_PVP_PERCENT = 252;

        public const int ACTION_CHARACTER_BOOST_FIRE_ELEMENT_PVP_PERCENT = 253;

        public const int ACTION_CHARACTER_BOOST_NEUTRAL_ELEMENT_PVP_PERCENT = 254;

        public const int ACTION_CHARACTER_DEBOOST_EARTH_ELEMENT_PVP_PERCENT = 255;

        public const int ACTION_CHARACTER_DEBOOST_WATER_ELEMENT_PVP_PERCENT = 256;

        public const int ACTION_CHARACTER_DEBOOST_AIR_ELEMENT_PVP_PERCENT = 257;

        public const int ACTION_CHARACTER_DEBOOST_FIRE_ELEMENT_PVP_PERCENT = 258;

        public const int ACTION_CHARACTER_DEBOOST_NEUTRAL_ELEMENT_PVP_PERCENT = 259;

        public const int ACTION_CHARACTER_BOOST_EARTH_ELEMENT_PVP_RESIST = 260;

        public const int ACTION_CHARACTER_BOOST_WATER_ELEMENT_PVP_RESIST = 261;

        public const int ACTION_CHARACTER_BOOST_AIR_ELEMENT_PVP_RESIST = 262;

        public const int ACTION_CHARACTER_BOOST_FIRE_ELEMENT_PVP_RESIST = 263;

        public const int ACTION_CHARACTER_BOOST_NEUTRAL_ELEMENT_PVP_RESIST = 264;

        public const int ACTION_CHARACTER_LIFE_LOST_CASTER_MODERATOR = 265;

        public const int ACTION_CHARACTER_STEAL_CHANCE = 266;

        public const int ACTION_CHARACTER_STEAL_VITALITY = 267;

        public const int ACTION_CHARACTER_STEAL_AGILITY = 268;

        public const int ACTION_CHARACTER_STEAL_INTELLIGENCE = 269;

        public const int ACTION_CHARACTER_STEAL_WISDOM = 270;

        public const int ACTION_CHARACTER_STEAL_STRENGTH = 271;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_CASTER_LIFE_MISSING_FROM_WATER = 275;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_CASTER_LIFE_MISSING_FROM_EARTH = 276;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_CASTER_LIFE_MISSING_FROM_AIR = 277;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_CASTER_LIFE_MISSING_FROM_FIRE = 278;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_CASTER_LIFE_MISSING = 279;

        public const int ACTION_BOOST_SPELL_RANGE = 281;

        public const int ACTION_BOOST_SPELL_RANGEABLE = 282;

        public const int ACTION_BOOST_SPELL_DMG = 283;

        public const int ACTION_BOOST_SPELL_HEAL = 284;

        public const int ACTION_BOOST_SPELL_AP_COST = 285;

        public const int ACTION_BOOST_SPELL_CAST_INTVL = 286;

        public const int ACTION_BOOST_SPELL_CC = 287;

        public const int ACTION_BOOST_SPELL_CASTOUTLINE = 288;

        public const int ACTION_BOOST_SPELL_NOLINEOFSIGHT = 289;

        public const int ACTION_BOOST_SPELL_MAXPERTURN = 290;

        public const int ACTION_BOOST_SPELL_MAXPERTARGET = 291;

        public const int ACTION_BOOST_SPELL_CAST_INTVL_SET = 292;

        public const int ACTION_BOOST_SPELL_BASE_DMG = 293;

        public const int ACTION_DEBOOST_SPELL_RANGE = 294;

        public const int ACTION_FIGHT_CAST_SPELL = 300;

        public const int ACTION_FIGHT_CAST_SPELL_CRITICAL_HIT = 301;

        public const int ACTION_FIGHT_CAST_SPELL_CRITICAL_MISS = 302;

        public const int ACTION_FIGHT_CLOSE_COMBAT = 303;

        public const int ACTION_FIGHT_CLOSE_COMBAT_CRITICAL_HIT = 304;

        public const int ACTION_FIGHT_CLOSE_COMBAT_CRITICAL_MISS = 305;

        public const int ACTION_FIGHT_TRIGGER_TRAP = 306;

        public const int ACTION_FIGHT_TRIGGER_GLYPH = 307;

        public const int ACTION_FIGHT_SPELL_DODGED_PA = 308;

        public const int ACTION_FIGHT_SPELL_DODGED_PM = 309;

        public const int ACTION_CHARACTER_STEAL_RANGE = 320;

        public const int ACTION_CHARACTER_CHANGE_COLOR = 333;

        public const int ACTION_CHARACTER_ADD_APPEARANCE = 335;

        public const int ACTION_FIGHT_ADD_TRAP_CASTING_SPELL = 400;

        public const int ACTION_FIGHT_ADD_GLYPH_CASTING_SPELL = 401;

        public const int ACTION_FIGHT_ADD_GLYPH_CASTING_SPELL_ENDTURN = 402;

        public const int ACTION_KILL_AND_SUMMON_CREATURE = 405;

        public const int ACTION_CHARACTER_REMOVE_ALL_SPELL_EFFECTS = 406;

        public const int ACTION_CHARACTER_BOOST_AP_ATTACK = 410;

        public const int ACTION_CHARACTER_BOOST_MP_ATTACK = 412;

        public const int ACTION_BOOST_PUSH_DMG = 414;

        public const int ACTION_DEBOOST_PUSH_DMG_REDUCTION = 417;

        public const int ACTION_INTERACTIVE_ELEMENT = 500;

        public const int ACTION_SKILL_ANIMATION = 501;

        public const int ACTION_EXCHANGE_CRAFT_OPEN = 502;

        public const int ACTION_USE_WAYPOINT = 503;

        public const int ACTION_DO_ELEMENT_PARAMETERIZED_OPERATION = 505;

        public const int ACTION_INTERACTIVE_ELEMENT_AT_HOME_INNER_DOOR = 507;

        public const int ACTION_SAVE_WAYPOINT = 508;

        public const int ACTION_CHANGE_COMPASS = 509;

        public const int ACTION_USE_SUBWAY = 510;

        public const int ACTION_EXCHANGE_JOBINDEX_OPEN = 511;

        public const int ACTION_USE_PRISM = 512;

        public const int ACTION_ADD_PRISM = 513;

        public const int ACTION_CHARACTER_BOOST_DISPELLED = 514;

        public const int ACTION_CHARACTER_UPDATE_BOOST = 515;

        public const int ACTION_GOTO_WAYPOINT = 600;

        public const int ACTION_GOTO_MAP = 601;

        public const int ACTION_CHARACTER_LEARN_SPELL = 604;

        public const int ACTION_CHARACTER_GAIN_XP = 605;

        public const int ACTION_CHARACTER_GAIN_WISDOM = 606;

        public const int ACTION_CHARACTER_GAIN_STRENGTH = 607;

        public const int ACTION_CHARACTER_GAIN_CHANCE = 608;

        public const int ACTION_CHARACTER_GAIN_AGILITY = 609;

        public const int ACTION_CHARACTER_GAIN_VITALITY = 610;

        public const int ACTION_CHARACTER_GAIN_INTELLIGENCE = 611;

        public const int ACTION_CHARACTER_GAIN_STATS_POINTS = 612;

        public const int ACTION_CHARACTER_GAIN_SPELL_POINTS = 613;

        public const int ACTION_CHARACTER_GAIN_JOB_XP = 614;

        public const int ACTION_CHARACTER_UNBOOST_SPELL = 616;

        public const int ACTION_CHARACTER_GET_MARRIED = 617;

        public const int ACTION_CHARACTER_GET_MARRIED_ACCEPT = 618;

        public const int ACTION_CHARACTER_GET_MARRIED_DECLINE = 619;

        public const int ACTION_CHARACTER_READ_BOOK = 620;

        public const int ACTION_CHARACTER_SUMMON_MONSTER = 621;

        public const int ACTION_GOTO_HOUSE = 622;

        public const int ACTION_CHARACTER_SUMMON_MONSTER_GROUP = 623;

        public const int ACTION_CHARACTER_UNLEARN_GUILDSPELL = 624;

        public const int ACTION_CHARACTER_UNBOOST_CARACS = 625;

        public const int ACTION_CHARACTER_UNBOOST_CARACS_TO_101 = 626;

        public const int ACTION_CHARACTER_SUMMON_MONSTER_GROUP_SET_MAP = 627;

        public const int ACTION_CHARACTER_SUMMON_MONSTER_GROUP_DYNAMIC = 628;

        public const int ACTION_CHARACTER_SEND_INFORMATION_TEXT = 630;

        public const int ACTION_CHARACTER_SEND_DIALOG_ACTION = 631;

        public const int ACTION_CHARACTER_GAIN_HONOUR = 640;

        public const int ACTION_CHARACTER_GAIN_DISHONOUR = 641;

        public const int ACTION_CHARACTER_LOOSE_HONOUR = 642;

        public const int ACTION_CHARACTER_LOOSE_DISHONOUR = 643;

        public const int ACTION_MAP_RESURECTION_ALLIES = 645;

        public const int ACTION_MAP_HEAL_ALLIES = 646;

        public const int ACTION_MAP_FORCE_ENNEMIES_GHOST = 647;

        public const int ACTION_FORCE_ENNEMY_GHOST = 648;

        public const int ACTION_FAKE_ALIGNMENT = 649;

        public const int ACTION_TELEPORT_NOOBY_MAP = 650;

        public const int ACTION_USE_ELEMENT_ACTIONS = 651;

        public const int ACTION_SET_STATED_ELEMENT_STATE = 652;

        public const int ACTION_RESET_STATED_ELEMENT = 653;

        public const int ACTION_HOUSE_LEAVE = 654;

        public const int ACTION_NOOP = 666;

        public const int ACTION_INCARNATION = 669;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_CASTER_LIFE_MIDLIFE = 672;

        public const int ACTION_CHARACTER_JOB_REFERENCEMENT = 699;

        public const int ACTION_ITEM_CHANGE_EFFECT = 700;

        public const int ACTION_ITEM_ADD_EFFECT = 701;

        public const int ACTION_ITEM_ADD_DURABILITY = 702;

        public const int ACTION_CAPTURE_SOUL = 705;

        public const int ACTION_CAPTURE_RIDE = 706;

        public const int ACTION_USE_PRESET = 707;

        public const int ACTION_CHARACTER_ADD_COST_TO_ACTION = 710;

        public const int ACTION_LADDER_SUPERRACE = 715;

        public const int ACTION_LADDER_RACE = 716;

        public const int ACTION_LADDER_ID = 717;

        public const int ACTION_PVP_LADDER = 720;

        public const int ACTION_VICTIM_OF = 721;

        public const int ACTION_GAIN_TEMP_SPELL = 722;

        public const int ACTION_GAIN_AURA = 723;

        public const int ACTION_GAIN_TITLE = 724;

        public const int ACTION_CHARACTER_RENAME_GUILD = 725;

        public const int ACTION_TELEPORT_NEAREST_PRISM = 730;

        public const int ACTION_AUTO_AGGRESS_ENEMY_PLAYER = 731;

        public const int ACTION_SHUSHU_STACK_RUNE = 742;

        public const int ACTION_BOOST_SOUL_CAPTURE_BONUS = 750;

        public const int ACTION_BOOST_RIDE_XP_BONUS = 751;

        public const int ACTION_CHARACTER_BOOST_DODGE = 752;

        public const int ACTION_CHARACTER_BOOST_TACKLE = 753;

        public const int ACTION_CHARACTER_DEBOOST_DODGE = 754;

        public const int ACTION_CHARACTER_DEBOOST_TACKLE = 755;

        public const int ACTION_REMOVE_ON_MOVE = 760;

        public const int ACTION_CHARACTER_SACRIFY = 765;

        public const int ACTION_HOURLY_CONFUSION_DEGREE = 770;

        public const int ACTION_HOURLY_CONFUSION_PI_2 = 771;

        public const int ACTION_HOURLY_CONFUSION_PI_4 = 772;

        public const int ACTION_UNHOURLY_CONFUSION_DEGREE = 773;

        public const int ACTION_UNHOURLY_CONFUSION_PI_2 = 774;

        public const int ACTION_UNHOURLY_CONFUSION_PI_4 = 775;

        public const int ACTION_CHARACTER_BOOST_PERMANENT_DAMAGE_PERCENT = 776;

        public const int ACTION_CHARACTER_SUMMON_DEAD_ALLY_IN_FIGHT = 780;

        public const int ACTION_CHARACTER_UNLUCKY = 781;

        public const int ACTION_CHARACTER_MAXIMIZE_ROLL = 782;

        public const int ACTION_CHARACTER_WALK_FOUR_DIR = 785;

        public const int ACTION_CHARACTER_CHATIMENT = 788;

        public const int ACTION_FIND_BOUNTY_TARGET = 790;

        public const int ACTION_MARK_TARGET_MERCENARY = 791;

        public const int ACTION_TARGET_CASTS_SPELL = 792;

        public const int ACTION_TARGET_CASTS_SPELL_WITH_ANIM = 793;

        public const int ACTION_HUNT_TOOL = 795;

        public const int ACTION_ITEM_CHANGE_PETS_LIFE = 800;

        public const int ACTION_ITEM_CHANGE_DURATION = 805;

        public const int ACTION_ITEM_PETS_SHAPE = 806;

        public const int ACTION_ITEM_PETS_EAT = 807;

        public const int ACTION_PETS_LAST_MEAL = 808;

        public const int ACTION_ITEM_CHANGE_DURABILITY = 812;

        public const int ACTION_ITEM_UPDATE_DATE = 813;

        public const int ACTION_ITEM_DUNGEON_KEY_DATE = 814;

        public const int ACTION_ITEM_CHOOSE_IN_ITEM_LIST = 820;

        public const int ACTION_CLIENT_OPEN_UI = 830;

        public const int ACTION_CLIENT_OPEN_UI_SPELL_FORGET = 831;

        public const int ACTION_FIGHT_TURN_FINISHED = 899;

        public const int ACTION_FIGHT_CHALLENGE = 900;

        public const int ACTION_FIGHT_CHALLENGE_ACCEPT = 901;

        public const int ACTION_FIGHT_CHALLENGE_DECLINE = 902;

        public const int ACTION_FIGHT_CHALLENGE_JOIN = 903;

        public const int ACTION_FIGHT_CHALLENGE_AGAINST_MONSTER = 905;

        public const int ACTION_FIGHT_AGGRESSION = 906;

        public const int ACTION_FIGHT_AGAINST_TAXCOLLECTOR = 909;

        public const int ACTION_FIGHT_CHALLENGE_AGAINST_MUTANT = 910;

        public const int ACTION_FIGHT_CHALLENGE_MIXED_VERSUS_MONSTER = 911;

        public const int ACTION_FIGHT_AGAINST_PRISM = 912;

        public const int ACTION_TOOLTIP_ACTIVATE_TIP = 915;

        public const int ACTION_PNJ_REMOVE_RIDE = 938;

        public const int ACTION_PET_SET_POWER_BOOST = 939;

        public const int ACTION_PET_POWER_BOOST = 940;

        public const int ACTION_FARM_WITHDRAW_ITEM = 947;

        public const int ACTION_FARM_PLACE_ITEM = 948;

        public const int ACTION_MOUNT_RIDE = 949;

        public const int ACTION_FIGHT_SET_STATE = 950;

        public const int ACTION_FIGHT_UNSET_STATE = 951;

        public const int ACTION_FIGHT_DISABLE_STATE = 952;

        public const int ACTION_SHOW_ALIGNMENT = 960;

        public const int ACTION_SHOW_GRADE = 961;

        public const int ACTION_SHOW_LEVEL = 962;

        public const int ACTION_CREATED_SINCE = 963;

        public const int ACTION_SHOW_PLAYERNAME = 964;

        public const int ACTION_LIVING_OBJECT_ID = 970;

        public const int ACTION_LIVING_OBJECT_MOOD = 971;

        public const int ACTION_LIVING_OBJECT_SKIN = 972;

        public const int ACTION_LIVING_OBJECT_CATEGORY = 973;

        public const int ACTION_LIVING_OBJECT_LEVEL = 974;

        public const int ACTION_LINKED_TO_CHARACTER = 981;

        public const int ACTION_LINKED_TO_ACCOUNT = 982;

        public const int ACTION_LINKED_UNTIL_DATE = 983;

        public const int ACTION_NAME_MAGE = 985;

        public const int ACTION_NAME_OWNER = 987;

        public const int ACTION_NAME_CRAFTER = 988;

        public const int ACTION_MOUNT_DESCRIPTION = 995;

        public const int ACTION_MOUNT_DESCRIPTION_OWNER = 996;

        public const int ACTION_MOUNT_DESCRIPTION_WITH_DATE = 998;

        public const int ACTION_PET_REDUCE_MAX_BONUS = 1005;

        public const int ACTION_PET_REDUCE_MIN_BONUS = 1006;

        public const int ACTION_SUMMON_BOMB = 1008;

        public const int ACTION_SUMMON_SLAVE = 1011;

        public const int ACTION_TARGET_EXECUTE_SPELL_ON_SOURCE = 1017;

        public const int ACTION_SOURCE_EXECUTE_SPELL_ON_TARGET = 1018;

        public const int ACTION_SOURCE_EXECUTE_SPELL_ON_SOURCE = 1019;

        public const int ACTION_CHARACTER_ADD_RANDOM_ILLUSION = 1024;

        public const int ACTION_CHARACTER_ADD_SPELL_COOLDOWN = 1035;

        public const int ACTION_CHARACTER_REMOVE_SPELL_COOLDOWN = 1036;

        public const int ACTION_CHARACTER_BOOST_SHIELD_BASED_ON_CASTER_LIFE = 1039;

        public const int ACTION_CHARACTER_BOOST_SHIELD = 1040;

        public const int ACTION_CHARACTER_GET_PUSHED = 1041;

        public const int ACTION_CHARACTER_GET_PULLED = 1042;

        public const int ACTION_CHARACTER_IMMUNITY_AGAINST_SPELL = 1044;

        public const int ACTION_CHARACTER_SET_SPELL_COOLDOWN = 1045;

        public const int ACTION_CHARACTER_LIFE_POINTS_MALUS = 1047;

        public const int ACTION_CHARACTER_LIFE_POINTS_MALUS_PERCENT = 1048;

        public const int ACTION_CHARACTER_GAIN_JOB_LEVEL = 1050;

        public const int ACTION_BOOST_SPELL_DAMAGES_PERCENT = 1054;

        public const int ACTION_SHARE_DAMAGES = 1061;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_TARGET_LIFE_FROM_WATER = 1068;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_TARGET_LIFE_FROM_EARTH = 1070;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_TARGET_LIFE_FROM_AIR = 1067;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_TARGET_LIFE_FROM_FIRE = 1069;

        public const int ACTION_CHARACTER_LIFE_POINTS_LOST_BASED_ON_TARGET_LIFE = 1071;

        public const int ACTION_CHARACTER_SHORTEN_ACTIVE_EFFECTS_DURATION = 1075;

        public const int ACTION_BOOST_GLOBAL_RESISTS_BONUS = 1076;

        public const int ACTION_BOOST_GLOBAL_RESISTS_MALUS = 1077;

        public const int ACTION_BOOST_ITEM_WEIGHT = 1081;

        public const int ACTION_ITEM_GIFT_CONTENT = 1084;

        public const int ACTION_PLACE_GLYPH_AURA = 1091;

        public const int ACTION_CHARACTER_ADD_MIRROR_ILLUSION = 1097;

        public const int ACTION_TELEPORT_TO_TURN_START_POSITION = 1099;

        public const int ACTION_TELEPORT_TO_PREVIOUS_POSITION = 1100;

        public const int ACTION_SPEAKING_OBJECT = 1102;

        public const int ACTION_TELEPORT_MIRROR_BY_TARGET = 1104;

        public const int ACTION_TELEPORT_MIRROR_BY_CASTER = 1105;

        public const int ACTION_TELEPORT_MIRROR_BY_AREA_CENTER = 1106;

        public const int ACTION_GAIN_LIFE_ON_TARGET_LIFE_PERCENT = 1109;

        public const int ACTION_ITEM_LOOT_COUNT = 1111;

        public const int ACTION_FIGHT_BOOST_WEAPON_DAMAGE_POWER = 1144;

        public const int ACTION_ITEM_SKIN_ITEM = 1151;

        public const int ACTION_CHARACTER_MULTIPLY_RECEIVED_HEAL = 1159;

        public const int ACTION_CASTER_EXECUTE_SPELL = 1160;

        public const int ACTION_CHARACTER_COMPANION = 1161;

        public const int ACTION_CHARACTER_MULTIPLY_RECEIVED_DAMAGE = 1163;

        public const int ACTION_CHARACTER_GIVE_LIFE_WITH_RATIO = 1164;

        public const int ACTION_BOOST_FINAL_DAMAGES_PERCENT = 1171;

        public const int ACTION_DEBOOST_FINAL_DAMAGES_PERCENT = 1172;

        public const int ACTION_CAST_SPELL_AT_FIGHT_START = 1175;

        public const int ACTION_WRAPPER_OBJECT_GID = 1176;

        public const int ACTION_WRAPPER_OBJECT_CATEGORY = 1179;

        public const int ACTION_FIGHT_DISABLE_PORTAL = 1183;

        public const int ACTION_MOUNT_HARNESS_GID = 1187;

        public const int ACTION_MOUNT_ADD_CAPACITY = 1188;

        public const int ACTION_ITEM_NUGGETS_SHARING = 2021;

        public const int ACTION_LAUNCH_FINISH_MOVE = 2029;

        public const int ACTION_TARGET_CAST_SPELL_ON_TARGETED_CELL = 2794;

        public const int ACTION_KILL_AND_SUMMON_SLAVE = 2796;

        public const int ACTION_CHARACTER_BOOST_DEALT_DAMAGE_PERCENT_MULTIPLIER_MELEE = 2800;

        public const int ACTION_CHARACTER_DEBOOST_DEALT_DAMAGE_PERCENT_MULTIPLIER_MELEE = 2801;

        public const int ACTION_CHARACTER_BOOST_RECEIVED_DAMAGE_PERCENT_MULTIPLIER_MELEE = 2802;

        public const int ACTION_CHARACTER_DEBOOST_RECEIVED_DAMAGE_PERCENT_MULTIPLIER_MELEE = 2803;

        public const int ACTION_CHARACTER_BOOST_DEALT_DAMAGE_PERCENT_MULTIPLIER_DISTANCE = 2804;

        public const int ACTION_CHARACTER_DEBOOST_DEALT_DAMAGE_PERCENT_MULTIPLIER_DISTANCE = 2805;

        public const int ACTION_CHARACTER_BOOST_RECEIVED_DAMAGE_PERCENT_MULTIPLIER_DISTANCE = 2806;

        public const int ACTION_CHARACTER_DEBOOST_RECEIVED_DAMAGE_PERCENT_MULTIPLIER_DISTANCE = 2807;

        public const int ACTION_CHARACTER_BOOST_DEALT_DAMAGE_PERCENT_MULTIPLIER_WEAPON = 2808;

        public const int ACTION_CHARACTER_DEBOOST_DEALT_DAMAGE_PERCENT_MULTIPLIER_WEAPON = 2809;

        public const int ACTION_CHARACTER_BOOST_RECEIVED_DAMAGE_PERCENT_MULTIPLIER_WEAPON = 2810;

        public const int ACTION_CHARACTER_DEBOOST_RECEIVED_DAMAGE_PERCENT_MULTIPLIER_WEAPON = 2811;

        public const int ACTION_CHARACTER_BOOST_DEALT_DAMAGE_PERCENT_MULTIPLIER_SPELLS = 2812;

        public const int ACTION_CHARACTER_DEBOOST_DEALT_DAMAGE_PERCENT_MULTIPLIER_SPELLS = 2813;

        public const int ACTION_CHARACTER_BOOST_RECEIVED_DAMAGE_PERCENT_MULTIPLIER_SPELLS = 2814;

        public const int ACTION_CHARACTER_DEBOOST_RECEIVED_DAMAGE_PERCENT_MULTIPLIER_SPELLS = 2815;

        public const int ACTION_EVOLVING_IMPROVEMENT_POINTS = 2818;

        public const int ACTION_EVOLVING_EXPERIENCE_POINTS = 2819;

        public const int ACTION_ITEM_GIVEN_EXPERIENCE_AS_SUPERFOOD = 2820;

        public const int ACTION_EVOLVING_LEVEL = 2821;

    }
}
