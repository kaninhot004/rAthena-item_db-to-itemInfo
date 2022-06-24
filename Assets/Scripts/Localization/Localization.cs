using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Localization : MonoBehaviour
{
    #region Constant
    public const string THAI = "THAI";
    public const string ENGLISH = "ENGLISH";

    public const string ERROR = "ERROR";
    public const string NOT_FOUND = "NOT_FOUND";

    public const string AND = "AND";
    public const string WITH = "WITH";
    public const string OR = "OR";
    public const string CAN = "CAN";
    public const string CANNOT = "CANNOT";

    public const string CONVERT_PROGRESSION_START = "CONVERT_PROGRESSION_START";
    public const string CONVERT_PROGRESSION_FETCHING_ITEM = "CONVERT_PROGRESSION_FETCHING_ITEM";
    public const string CONVERT_PROGRESSION_FETCHING_RESOURCE_NAME = "CONVERT_PROGRESSION_FETCHING_RESOURCE_NAME";
    public const string CONVERT_PROGRESSION_FETCHING_SKILL = "CONVERT_PROGRESSION_FETCHING_SKILL";
    public const string CONVERT_PROGRESSION_FETCHING_CLASS_NUMBER = "CONVERT_PROGRESSION_FETCHING_CLASS_NUMBER";
    public const string CONVERT_PROGRESSION_FETCHING_CLASS_MONSTER = "CONVERT_PROGRESSION_FETCHING_CLASS_MONSTER";
    public const string CONVERT_PROGRESSION_FETCHING_ITEM_COMBO = "CONVERT_PROGRESSION_FETCHING_ITEM_COMBO";
    public const string CONVERT_PROGRESSION_FETCHING_RESOURCE_NAME_WITH_TYPE = "CONVERT_PROGRESSION_FETCHING_RESOURCE_NAME_WITH_TYPE";
    public const string CONVERT_PROGRESSION_PLEASE_WAIT = "CONVERT_PROGRESSION_PLEASE_WAIT";
    public const string CONVERT_PROGRESSION_DONE = "CONVERT_PROGRESSION_DONE";

    public const string JOBS_ALL_JOB = "JOBS_ALL_JOB";
    public const string CLASSES_ALL_CLASS = "CLASSES_ALL_CLASS";
    public const string CLASSES_BABY = "CLASSES_BABY";
    public const string CLASSES_TRANS = "CLASSES_TRANS";
    public const string GENDER_FEMALE = "GENDER_FEMALE";
    public const string GENDER_MALE = "GENDER_MALE";
    public const string GENDER_ALL = "GENDER_ALL";
    public const string LOCATION_HEAD_TOP = "LOCATION_HEAD_TOP";
    public const string LOCATION_HEAD_MID = "LOCATION_HEAD_MID";
    public const string LOCATION_HEAD_LOW = "LOCATION_HEAD_LOW";
    public const string LOCATION_ARMOR = "LOCATION_ARMOR";
    public const string LOCATION_RIGHT_HAND = "LOCATION_RIGHT_HAND";
    public const string LOCATION_LEFT_HAND = "LOCATION_LEFT_HAND";
    public const string LOCATION_GARMENT = "LOCATION_GARMENT";
    public const string LOCATION_SHOES = "LOCATION_SHOES";
    public const string LOCATION_RIGHT_ACCESSORY = "LOCATION_RIGHT_ACCESSORY";
    public const string LOCATION_LEFT_ACCESSORY = "LOCATION_LEFT_ACCESSORY";
    public const string LOCATION_COSTUME_HEAD_TOP = "LOCATION_COSTUME_HEAD_TOP";
    public const string LOCATION_COSTUME_HEAD_MID = "LOCATION_COSTUME_HEAD_MID";
    public const string LOCATION_COSTUME_HEAD_LOW = "LOCATION_COSTUME_HEAD_LOW";
    public const string LOCATION_COSTUME_GARMENT = "LOCATION_COSTUME_GARMENT";
    public const string LOCATION_AMMO = "LOCATION_AMMO";
    public const string LOCATION_SHADOW_ARMOR = "LOCATION_SHADOW_ARMOR";
    public const string LOCATION_SHADOW_WEAPON = "LOCATION_SHADOW_WEAPON";
    public const string LOCATION_SHADOW_SHIELD = "LOCATION_SHADOW_SHIELD";
    public const string LOCATION_SHADOW_SHOES = "LOCATION_SHADOW_SHOES";
    public const string LOCATION_SHADOW_RIGHT_ACCESSORY = "LOCATION_SHADOW_RIGHT_ACCESSORY";
    public const string LOCATION_SHADOW_LEFT_ACCESSORY = "LOCATION_SHADOW_LEFT_ACCESSORY";
    public const string LOCATION_BOTH_HAND = "LOCATION_BOTH_HAND";
    public const string LOCATION_BOTH_ACCESSORY = "LOCATION_BOTH_ACCESSORY";

    public const string WHEN_EQUIP = "WHEN_EQUIP";
    public const string WHEN_UNEQUIP = "WHEN_UNEQUIP";
    public const string TYPE = "TYPE";
    public const string SUB_TYPE = "SUB_TYPE";
    public const string LOCATION = "LOCATION";
    public const string JOB = "JOB";
    public const string CLASS = "CLASS";
    public const string GENDER = "GENDER";
    public const string ATTACK = "ATTACK";
    public const string MAGIC_ATTACK = "MAGIC_ATTACK";
    public const string DEFENSE = "DEFENSE";
    public const string RANGE = "RANGE";
    public const string WEAPON_LEVEL = "WEAPON_LEVEL";
    public const string ARMOR_LEVEL = "ARMOR_LEVEL";
    public const string MINIMUM_EQUIP_LEVEL = "MINIMUM_EQUIP_LEVEL";
    public const string MAXIMUM_EQUIP_LEVEL = "MAXIMUM_EQUIP_LEVEL";
    public const string REFINABLE = "REFINABLE";
    public const string WEIGHT = "WEIGHT";
    public const string PRICE = "PRICE";

    public const string AUTO_BONUS_3 = "AUTO_BONUS_3";
    public const string AUTO_BONUS_2 = "AUTO_BONUS_2";
    public const string AUTO_BONUS_1 = "AUTO_BONUS_1";
    public const string BONUS_SCRIPT = "BONUS_SCRIPT";

    public const string BONUS_BASE_ATK = "BONUS_BASE_ATK";
    public const string BONUS_WEAPON_ATK_RATE = "BONUS_WEAPON_ATK_RATE";
    public const string BONUS_WEAPON_MATK_RATE = "BONUS_WEAPON_MATK_RATE";
    public const string BONUS_DEF2 = "BONUS_DEF2";
    public const string BONUS_DEF2_RATE = "BONUS_DEF2_RATE";
    public const string BONUS_MDEF2 = "BONUS_MDEF2";
    public const string BONUS_MDEF2_RATE = "BONUS_MDEF2_RATE";
    public const string BONUS_CRITICAL_LONG = "BONUS_CRITICAL_LONG";
    public const string BONUS2_CRITICAL_ADD_RACE = "BONUS2_CRITICAL_ADD_RACE";
    public const string BONUS_PERFECT_HIT_RATE = "BONUS_PERFECT_HIT_RATE";
    public const string BONUS_SPEED_RATE = "BONUS_SPEED_RATE";
    public const string BONUS_SPEED_ADD_RATE = "BONUS_SPEED_ADD_RATE";
    public const string BONUS_ATK_RANGE = "BONUS_ATK_RANGE";
    public const string BONUS_ADD_MAX_WEIGHT = "BONUS_ADD_MAX_WEIGHT";
    public const string BONUS_HP_RECOV_RATE = "BONUS_HP_RECOV_RATE";
    public const string BONUS_SP_RECOV_RATE = "BONUS_SP_RECOV_RATE";
    public const string BONUS2_HP_REGEN_RATE = "BONUS2_HP_REGEN_RATE";
    public const string BONUS2_HP_LOSS_RATE = "BONUS2_HP_LOSS_RATE";
    public const string BONUS2_SP_REGEN_RATE = "BONUS2_SP_REGEN_RATE";
    public const string BONUS2_SP_LOSS_RATE = "BONUS2_SP_LOSS_RATE";
    public const string BONUS2_REGEN_PERCENT_HP = "BONUS2_REGEN_PERCENT_HP";
    public const string BONUS2_REGEN_PERCENT_SP = "BONUS2_REGEN_PERCENT_SP";
    public const string BONUS_STOP_HP_REGEN = "BONUS_STOP_HP_REGEN";
    public const string BONUS_STOP_SP_REGEN = "BONUS_STOP_SP_REGEN";
    public const string BONUS_USE_SP_RATE = "BONUS_USE_SP_RATE";
    public const string BONUS2_SKILL_USE_SP = "BONUS2_SKILL_USE_SP";
    public const string BONUS2_SKILL_USE_SP_RATE = "BONUS2_SKILL_USE_SP_RATE";
    public const string BONUS2_SKILL_ATK = "BONUS2_SKILL_ATK";
    public const string BONUS_SHORT_ATK_RATE = "BONUS_SHORT_ATK_RATE";
    public const string BONUS_LONG_ATK_RATE = "BONUS_LONG_ATK_RATE";
    public const string BONUS_CRIT_ATK_RATE = "BONUS_CRIT_ATK_RATE";
    public const string BONUS_CRIT_DEF_RATE = "BONUS_CRIT_DEF_RATE";
    public const string BONUS_CRITICAL_DEF = "BONUS_CRITICAL_DEF";
    public const string BONUS2_WEAPON_ATK = "BONUS2_WEAPON_ATK";
    public const string BONUS2_WEAPON_DAMAGE_RATE = "BONUS2_WEAPON_DAMAGE_RATE";
    public const string BONUS_NEAR_ATK_DEF = "BONUS_NEAR_ATK_DEF";
    public const string BONUS_LONG_ATK_DEF = "BONUS_LONG_ATK_DEF";
    public const string BONUS_MAGIC_ATK_DEF = "BONUS_MAGIC_ATK_DEF";
    public const string BONUS_MISC_ATK_DEF = "BONUS_MISC_ATK_DEF";
    public const string BONUS_NO_WEAPON_DAMAGE = "BONUS_NO_WEAPON_DAMAGE";
    public const string BONUS_NO_MAGIC_DAMAGE = "BONUS_NO_MAGIC_DAMAGE";
    public const string BONUS_NO_MISC_DAMAGE = "BONUS_NO_MISC_DAMAGE";
    public const string BONUS_HEAL_POWER = "BONUS_HEAL_POWER";
    public const string BONUS_HEAL_POWER_2 = "BONUS_HEAL_POWER_2";
    public const string BONUS2_SKILL_HEAL = "BONUS2_SKILL_HEAL";
    public const string BONUS2_SKILL_HEAL_2 = "BONUS2_SKILL_HEAL_2";
    public const string BONUS_ADD_ITEM_HEAL_RATE = "BONUS_ADD_ITEM_HEAL_RATE";
    public const string BONUS2_ADD_ITEM_HEAL_RATE = "BONUS2_ADD_ITEM_HEAL_RATE";
    public const string BONUS2_ADD_ITEM_GROUP_HEAL_RATE = "BONUS2_ADD_ITEM_GROUP_HEAL_RATE";
    public const string BONUS_ADD_ITEM_SP_HEAL_RATE = "BONUS_ADD_ITEM_SP_HEAL_RATE";
    public const string BONUS2_ADD_ITEM_SP_HEAL_RATE = "BONUS2_ADD_ITEM_SP_HEAL_RATE";
    public const string BONUS2_ADD_ITEM_GROUP_SP_HEAL_RATE = "BONUS2_ADD_ITEM_GROUP_SP_HEAL_RATE";
    public const string BONUS_CAST_RATE = "BONUS_CAST_RATE";
    public const string BONUS2_CAST_RATE = "BONUS2_CAST_RATE";
    public const string BONUS_FIXED_CAST_RATE = "BONUS_FIXED_CAST_RATE";
    public const string BONUS2_FIXED_CAST_RATE = "BONUS2_FIXED_CAST_RATE";
    public const string BONUS_VARIABLE_CAST_RATE = "BONUS_VARIABLE_CAST_RATE";
    public const string BONUS2_VARIABLE_CAST_RATE = "BONUS2_VARIABLE_CAST_RATE";
    public const string BONUS_FIXED_CAST = "BONUS_FIXED_CAST";
    public const string BONUS2_SKILL_FIXED_CAST = "BONUS2_SKILL_FIXED_CAST";
    public const string BONUS_VARIABLE_CAST = "BONUS_VARIABLE_CAST";
    public const string BONUS2_SKILL_VARIABLE_CAST = "BONUS2_SKILL_VARIABLE_CAST";
    public const string BONUS_NO_CAST_CANCEL_2 = "BONUS_NO_CAST_CANCEL_2";
    public const string BONUS_NO_CAST_CANCEL = "BONUS_NO_CAST_CANCEL";
    public const string BONUS_DELAY_RATE = "BONUS_DELAY_RATE";
    public const string BONUS2_SKILL_DELAY = "BONUS2_SKILL_DELAY";
    public const string BONUS2_SKILL_COOLDOWN = "BONUS2_SKILL_COOLDOWN";
    public const string BONUS2_ADD_ELE = "BONUS2_ADD_ELE";
    public const string BONUS3_ADD_ELE = "BONUS3_ADD_ELE";
    public const string BONUS2_MAGIC_ADD_ELE = "BONUS2_MAGIC_ADD_ELE";
    public const string BONUS2_SUB_ELE = "BONUS2_SUB_ELE";
    public const string BONUS3_SUB_ELE = "BONUS3_SUB_ELE";
    public const string BONUS2_SUB_DEF_ELE = "BONUS2_SUB_DEF_ELE";
    public const string BONUS2_MAGIC_SUB_DEF_ELE = "BONUS2_MAGIC_SUB_DEF_ELE";
    public const string BONUS2_ADD_RACE = "BONUS2_ADD_RACE";
    public const string BONUS2_MAGIC_ADD_RACE = "BONUS2_MAGIC_ADD_RACE";
    public const string BONUS2_SUB_RACE = "BONUS2_SUB_RACE";
    public const string BONUS3_SUB_RACE = "BONUS3_SUB_RACE";
    public const string BONUS2_ADD_CLASS = "BONUS2_ADD_CLASS";
    public const string BONUS2_MAGIC_ADD_CLASS = "BONUS2_MAGIC_ADD_CLASS";
    public const string BONUS2_SUB_CLASS = "BONUS2_SUB_CLASS";
    public const string BONUS2_ADD_SIZE = "BONUS2_ADD_SIZE";
    public const string BONUS2_MAGIC_ADD_SIZE = "BONUS2_MAGIC_ADD_SIZE";
    public const string BONUS2_SUB_SIZE = "BONUS2_SUB_SIZE";
    public const string BONUS2_WEAPON_SUB_SIZE = "BONUS2_WEAPON_SUB_SIZE";
    public const string BONUS2_MAGIC_SUB_SIZE = "BONUS2_MAGIC_SUB_SIZE";
    public const string BONUS_NO_SIZE_FIX = "BONUS_NO_SIZE_FIX";
    public const string BONUS2_ADD_DAMAGE_CLASS = "BONUS2_ADD_DAMAGE_CLASS";
    public const string BONUS2_ADD_MAGIC_DAMAGE_CLASS = "BONUS2_ADD_MAGIC_DAMAGE_CLASS";
    public const string BONUS2_ADD_DEF_MONSTER = "BONUS2_ADD_DEF_MONSTER";
    public const string BONUS2_ADD_MDEF_MONSTER = "BONUS2_ADD_MDEF_MONSTER";
    public const string BONUS2_ADD_RACE_2 = "BONUS2_ADD_RACE_2";
    public const string BONUS2_SUB_RACE_2 = "BONUS2_SUB_RACE_2";
    public const string BONUS2_MAGIC_ADD_RACE_2 = "BONUS2_MAGIC_ADD_RACE_2";
    public const string BONUS2_SUB_SKILL = "BONUS2_SUB_SKILL";
    public const string BONUS_ABSORB_DMG_MAX_HP = "BONUS_ABSORB_DMG_MAX_HP";
    public const string BONUS_ABSORB_DMG_MAX_HP_2 = "BONUS_ABSORB_DMG_MAX_HP_2";
    public const string BONUS_ATK_ELE = "BONUS_ATK_ELE";
    public const string BONUS_DEF_ELE = "BONUS_DEF_ELE";
    public const string BONUS2_MAGIC_ATK_ELE = "BONUS2_MAGIC_ATK_ELE";
    public const string BONUS_DEF_RATIO_ATK_RACE = "BONUS_DEF_RATIO_ATK_RACE";
    public const string BONUS_DEF_RATIO_ATK_ELE = "BONUS_DEF_RATIO_ATK_ELE";
    public const string BONUS_DEF_RATIO_ATK_CLASS = "BONUS_DEF_RATIO_ATK_CLASS";
    public const string BONUS4_SET_DEF_RACE = "BONUS4_SET_DEF_RACE";
    public const string BONUS4_SET_MDEF_RACE = "BONUS4_SET_MDEF_RACE";
    public const string BONUS_IGNORE_DEF_ELE = "BONUS_IGNORE_DEF_ELE";
    public const string BONUS_IGNORE_DEF_RACE = "BONUS_IGNORE_DEF_RACE";
    public const string BONUS_IGNORE_DEF_CLASS = "BONUS_IGNORE_DEF_CLASS";
    public const string BONUS_IGNORE_MDEF_RACE = "BONUS_IGNORE_MDEF_RACE";
    public const string BONUS2_IGNORE_DEF_RACE_RATE = "BONUS2_IGNORE_DEF_RACE_RATE";
    public const string BONUS2_IGNORE_MDEF_RACE_RATE = "BONUS2_IGNORE_MDEF_RACE_RATE";
    public const string BONUS2_IGNORE_MDEF_RACE_2_RATE = "BONUS2_IGNORE_MDEF_RACE_2_RATE";
    public const string BONUS_IGNORE_MDEF_ELE = "BONUS_IGNORE_MDEF_ELE";
    public const string BONUS2_IGNORE_DEF_CLASS_RATE = "BONUS2_IGNORE_DEF_CLASS_RATE";
    public const string BONUS2_IGNORE_MDEF_CLASS_RATE = "BONUS2_IGNORE_MDEF_CLASS_RATE";
    public const string BONUS2_EXP_ADD_RACE = "BONUS2_EXP_ADD_RACE";
    public const string BONUS2_EXP_ADD_CLASS = "BONUS2_EXP_ADD_CLASS";
    public const string BONUS2_ADD_EFF = "BONUS2_ADD_EFF";
    public const string BONUS2_ADD_EFF_2 = "BONUS2_ADD_EFF_2";
    public const string BONUS2_ADD_EFF_WHEN_HIT = "BONUS2_ADD_EFF_WHEN_HIT";
    public const string BONUS2_RES_EFF = "BONUS2_RES_EFF";
    public const string BONUS3_ADD_EFF = "BONUS3_ADD_EFF";
    public const string BONUS4_ADD_EFF = "BONUS4_ADD_EFF";
    public const string BONUS3_ADD_EFF_WHEN_HIT = "BONUS3_ADD_EFF_WHEN_HIT";
    public const string BONUS4_ADD_EFF_WHEN_HIT = "BONUS4_ADD_EFF_WHEN_HIT";
    public const string BONUS3_ADD_EFF_ON_SKILL = "BONUS3_ADD_EFF_ON_SKILL";
    public const string BONUS4_ADD_EFF_ON_SKILL = "BONUS4_ADD_EFF_ON_SKILL";
    public const string BONUS5_ADD_EFF_ON_SKILL = "BONUS5_ADD_EFF_ON_SKILL";
    public const string BONUS2_COMA_CLASS = "BONUS2_COMA_CLASS";
    public const string BONUS2_COMA_RACE = "BONUS2_COMA_RACE";
    public const string BONUS2_WEAPON_COMA_ELE = "BONUS2_WEAPON_COMA_ELE";
    public const string BONUS2_WEAPON_COMA_CLASS = "BONUS2_WEAPON_COMA_CLASS";
    public const string BONUS2_WEAPON_COMA_RACE = "BONUS2_WEAPON_COMA_RACE";
    public const string BONUS3_AUTO_SPELL = "BONUS3_AUTO_SPELL";
    public const string BONUS3_AUTO_SPELL_WHEN_HIT = "BONUS3_AUTO_SPELL_WHEN_HIT";
    public const string BONUS4_AUTO_SPELL = "BONUS4_AUTO_SPELL";
    public const string BONUS5_AUTO_SPELL = "BONUS5_AUTO_SPELL";
    public const string BONUS4_AUTO_SPELL_WHEN_HIT = "BONUS4_AUTO_SPELL_WHEN_HIT";
    public const string BONUS5_AUTO_SPELL_WHEN_HIT = "BONUS5_AUTO_SPELL_WHEN_HIT";
    public const string BONUS4_AUTO_SPELL_ON_SKILL = "BONUS4_AUTO_SPELL_ON_SKILL";
    public const string BONUS5_AUTO_SPELL_ON_SKILL = "BONUS5_AUTO_SPELL_ON_SKILL";
    public const string BONUS_HP_DRAIN_VALUE = "BONUS_HP_DRAIN_VALUE";
    public const string BONUS2_HP_DRAIN_VALUE_RACE = "BONUS2_HP_DRAIN_VALUE_RACE";
    public const string BONUS2_HP_DRAIN_VALUE_CLASS = "BONUS2_HP_DRAIN_VALUE_CLASS";
    public const string BONUS_SP_DRAIN_VALUE = "BONUS_SP_DRAIN_VALUE";
    public const string BONUS2_SP_DRAIN_VALUE_RACE = "BONUS2_SP_DRAIN_VALUE_RACE";
    public const string BONUS2_SP_DRAIN_VALUE_CLASS = "BONUS2_SP_DRAIN_VALUE_CLASS";
    public const string BONUS2_HP_DRAIN_RATE = "BONUS2_HP_DRAIN_RATE";
    public const string BONUS2_SP_DRAIN_RATE = "BONUS2_SP_DRAIN_RATE";
    public const string BONUS2_HP_VANISH_RATE = "BONUS2_HP_VANISH_RATE";
    public const string BONUS3_HP_VANISH_RACE_RATE = "BONUS3_HP_VANISH_RACE_RATE";
    public const string BONUS3_HP_VANISH_RATE = "BONUS3_HP_VANISH_RATE";
    public const string BONUS2_SP_VANISH_RATE = "BONUS2_SP_VANISH_RATE";
    public const string BONUS3_SP_VANISH_RACE_RATE = "BONUS3_SP_VANISH_RACE_RATE";
    public const string BONUS3_SP_VANISH_RATE = "BONUS3_SP_VANISH_RATE";
    public const string BONUS3_STATE_NO_RECOVER_RACE = "BONUS3_STATE_NO_RECOVER_RACE";
    public const string BONUS_HP_GAIN_VALUE = "BONUS_HP_GAIN_VALUE";
    public const string BONUS_SP_GAIN_VALUE = "BONUS_SP_GAIN_VALUE";
    public const string BONUS2_SP_GAIN_RACE = "BONUS2_SP_GAIN_RACE";
    public const string BONUS_LONG_HP_GAIN_VALUE = "BONUS_LONG_HP_GAIN_VALUE";
    public const string BONUS_LONG_SP_GAIN_VALUE = "BONUS_LONG_SP_GAIN_VALUE";
    public const string BONUS_MAGIC_HP_GAIN_VALUE = "BONUS_MAGIC_HP_GAIN_VALUE";
    public const string BONUS_MAGIC_SP_GAIN_VALUE = "BONUS_MAGIC_SP_GAIN_VALUE";
    public const string BONUS_SHORT_WEAPON_DAMAGE_RETURN = "BONUS_SHORT_WEAPON_DAMAGE_RETURN";
    public const string BONUS_LONG_WEAPON_DAMAGE_RETURN = "BONUS_LONG_WEAPON_DAMAGE_RETURN";
    public const string BONUS_MAGIC_DAMAGE_RETURN = "BONUS_MAGIC_DAMAGE_RETURN";
    public const string BONUS_REDUCE_DAMAGE_RETURN = "BONUS_REDUCE_DAMAGE_RETURN";
    public const string BONUS_UNSTRIPABLE_WEAPON = "BONUS_UNSTRIPABLE_WEAPON";
    public const string BONUS_UNSTRIPABLE_ARMOR = "BONUS_UNSTRIPABLE_ARMOR";
    public const string BONUS_UNSTRIPABLE_HELM = "BONUS_UNSTRIPABLE_HELM";
    public const string BONUS_UNSTRIPABLE_SHIELD = "BONUS_UNSTRIPABLE_SHIELD";
    public const string BONUS_UNSTRIPABLE = "BONUS_UNSTRIPABLE";
    public const string BONUS_UNBREAKABLE_GARMENT = "BONUS_UNBREAKABLE_GARMENT";
    public const string BONUS_UNBREAKABLE_WEAPON = "BONUS_UNBREAKABLE_WEAPON";
    public const string BONUS_UNBREAKABLE_ARMOR = "BONUS_UNBREAKABLE_ARMOR";
    public const string BONUS_UNBREAKABLE_HELM = "BONUS_UNBREAKABLE_HELM";
    public const string BONUS_UNBREAKABLE_SHIELD = "BONUS_UNBREAKABLE_SHIELD";
    public const string BONUS_UNBREAKABLE_SHOES = "BONUS_UNBREAKABLE_SHOES";
    public const string BONUS_UNBREAKABLE = "BONUS_UNBREAKABLE";
    public const string BONUS_BREAK_WEAPON_RATE = "BONUS_BREAK_WEAPON_RATE";
    public const string BONUS_BREAK_ARMOR_RATE = "BONUS_BREAK_ARMOR_RATE";
    public const string BONUS2_DROP_ADD_RACE = "BONUS2_DROP_ADD_RACE";
    public const string BONUS2_DROP_ADD_CLASS = "BONUS2_DROP_ADD_CLASS";
    public const string BONUS3_ADD_MONSTER_ID_DROP_ITEM = "BONUS3_ADD_MONSTER_ID_DROP_ITEM";
    public const string BONUS2_ADD_MONSTER_DROP_ITEM = "BONUS2_ADD_MONSTER_DROP_ITEM";
    public const string BONUS3_ADD_MONSTER_DROP_ITEM = "BONUS3_ADD_MONSTER_DROP_ITEM";
    public const string BONUS3_ADD_CLASS_DROP_ITEM = "BONUS3_ADD_CLASS_DROP_ITEM";
    public const string BONUS2_ADD_MONSTER_DROP_ITEM_GROUP = "BONUS2_ADD_MONSTER_DROP_ITEM_GROUP";
    public const string BONUS3_ADD_MONSTER_DROP_ITEM_GROUP = "BONUS3_ADD_MONSTER_DROP_ITEM_GROUP";
    public const string BONUS3_ADD_CLASS_DROP_ITEM_GROUP = "BONUS3_ADD_CLASS_DROP_ITEM_GROUP";
    public const string BONUS2_GET_ZENY_NUM = "BONUS2_GET_ZENY_NUM";
    public const string BONUS2_ADD_GET_ZENY_NUM = "BONUS2_ADD_GET_ZENY_NUM";
    public const string BONUS_DOUBLE_RATE = "BONUS_DOUBLE_RATE";
    public const string BONUS_DOUBLE_ADD_RATE = "BONUS_DOUBLE_ADD_RATE";
    public const string BONUS_SPLASH_RANGE = "BONUS_SPLASH_RANGE";
    public const string BONUS_SPLASH_ADD_RANGE = "BONUS_SPLASH_ADD_RANGE";
    public const string BONUS2_ADD_SKILL_BLOW = "BONUS2_ADD_SKILL_BLOW";
    public const string BONUS_NO_KNOCKBACK = "BONUS_NO_KNOCKBACK";
    public const string BONUS_NO_GEM_STONE = "BONUS_NO_GEM_STONE";
    public const string BONUS_INTRAVISION = "BONUS_INTRAVISION";
    public const string BONUS_PERFECT_HIDE = "BONUS_PERFECT_HIDE";
    public const string BONUS_RESTART_FULL_RECOVER = "BONUS_RESTART_FULL_RECOVER";
    public const string BONUS_CLASS_CHANGE = "BONUS_CLASS_CHANGE";
    public const string BONUS_ADD_STEAL_RATE = "BONUS_ADD_STEAL_RATE";
    public const string BONUS_NO_MADO_FUEL = "BONUS_NO_MADO_FUEL";
    public const string BONUS_NO_WALK_DELAY = "BONUS_NO_WALK_DELAY";
    public const string SPECIAL_EFFECT_2 = "SPECIAL_EFFECT_2";
    public const string SPECIAL_EFFECT = "SPECIAL_EFFECT";
    public const string UNIT_SKILL_USE_ID = "UNIT_SKILL_USE_ID";
    public const string ITEM_SKILL = "ITEM_SKILL";
    public const string SKILL = "SKILL";
    public const string PERCENT_HEAL = "PERCENT_HEAL";
    public const string ITEM_HEAL = "ITEM_HEAL";
    public const string HEAL = "HEAL";
    public const string SC_START_4 = "SC_START_4";
    public const string SC_START_2 = "SC_START_2";
    public const string SC_START = "SC_START";
    public const string SC_END = "SC_END";
    public const string ACTIVE_TRANSFORM = "ACTIVE_TRANSFORM";
    public const string GET_ITEM = "GET_ITEM";
    public const string GET_GROUP_ITEM = "GET_GROUP_ITEM";
    public const string PET = "PET";
    public const string SC_END_CLASS = "SC_END_CLASS";
    public const string SET_MOUNTING = "SET_MOUNTING";
    public const string LAPHINE_UPGRADE = "LAPHINE_UPGRADE";
    public const string LAPHINE_SYNTHESIS = "LAPHINE_SYNTHESIS";
    public const string OPEN_STYLIST = "OPEN_STYLIST";
    public const string REFINE_UI = "REFINE_UI";

    public const string CONDITION_NOT_MET = "CONDITION_NOT_MET";
    public const string IF = "IF";
    public const string EQUIP_WITH = "EQUIP_WITH";
    public const string IF_MOUNTING = "IF_MOUNTING";
    public const string WAS = "WAS";
    public const string BASE_JOB = "BASE_JOB";
    public const string BASE_CLASS = "BASE_CLASS";
    public const string READ_PARAM = "READ_PARAM";
    public const string HI_CLASS = "HI_CLASS";
    public const string PET_INFO_INTIMATE = "PET_INFO_INTIMATE";
    public const string PET_INTIMATE_LOYAL = "PET_INTIMATE_LOYAL";
    public const string REFINE_COUNT = "REFINE_COUNT";
    public const string GRADE_COUNT = "GRADE_COUNT";
    public const string GET_WEAPON_LEVEL = "GET_WEAPON_LEVEL";
    public const string GET_EQUIPMENT_LEVEL = "GET_EQUIPMENT_LEVEL";
    public const string POW = "POW";
    public const string MIN = "MIN";
    public const string MAX = "MAX";
    public const string RAND = "RAND";
    public const string EQUAL = "EQUAL";
    public const string NOT_EQUAL = "NOT_EQUAL";
    public const string WILL_BE = "WILL_BE";
    public const string IF_NOT = "IF_NOT";
    public const string INFINITE = "INFINITE";
    public const string DIVIDE = "DIVIDE";
    public const string ALL_RACE = "ALL_RACE";
    public const string ALL_CLASS = "ALL_CLASS";
    public const string SIZE_SMALL = "SIZE_SMALL";
    public const string SIZE_MEDIUM = "SIZE_MEDIUM";
    public const string SIZE_LARGE = "SIZE_LARGE";
    public const string ALL_SIZE = "ALL_SIZE";
    public const string ALL_ELEMENT = "ALL_ELEMENT";
    public const string ATF_SELF = "ATF_SELF";
    public const string ATF_TARGET = "ATF_TARGET";
    public const string ATF_SHORT = "ATF_SHORT";
    public const string BF_SHORT = "BF_SHORT";
    public const string ATF_LONG = "ATF_LONG";
    public const string BF_LONG = "BF_LONG";
    public const string ATF_SKILL = "ATF_SKILL";
    public const string ATF_WEAPON = "ATF_WEAPON";
    public const string BF_WEAPON = "BF_WEAPON";
    public const string ATF_MAGIC = "ATF_MAGIC";
    public const string BF_MAGIC = "BF_MAGIC";
    public const string BF_SKILL = "BF_SKILL";
    public const string ATF_MISC = "ATF_MISC";
    public const string BF_MISC = "BF_MISC";
    public const string BF_NORMAL = "BF_NORMAL";
    public const string AUTO_BONUS_ATF_SELF = "AUTO_BONUS_ATF_SELF";
    public const string AUTO_BONUS_ATF_TARGET = "AUTO_BONUS_ATF_TARGET";
    public const string AUTO_BONUS_ATF_SHORT = "AUTO_BONUS_ATF_SHORT";
    public const string AUTO_BONUS_BF_SHORT = "AUTO_BONUS_BF_SHORT";
    public const string AUTO_BONUS_ATF_LONG = "AUTO_BONUS_ATF_LONG";
    public const string AUTO_BONUS_BF_LONG = "AUTO_BONUS_BF_LONG";
    public const string AUTO_BONUS_ATF_SKILL = "AUTO_BONUS_ATF_SKILL";
    public const string AUTO_BONUS_ATF_WEAPON = "AUTO_BONUS_ATF_WEAPON";
    public const string AUTO_BONUS_BF_WEAPON = "AUTO_BONUS_BF_WEAPON";
    public const string AUTO_BONUS_ATF_MAGIC = "AUTO_BONUS_ATF_MAGIC";
    public const string AUTO_BONUS_BF_MAGIC = "AUTO_BONUS_BF_MAGIC";
    public const string AUTO_BONUS_BF_SKILL = "AUTO_BONUS_BF_SKILL";
    public const string AUTO_BONUS_ATF_MISC = "AUTO_BONUS_ATF_MISC";
    public const string AUTO_BONUS_BF_MISC = "AUTO_BONUS_BF_MISC";
    public const string AUTO_BONUS_BF_NORMAL = "AUTO_BONUS_BF_NORMAL";
    public const string PHYSICAL_DAMAGE = "PHYSICAL_DAMAGE";
    public const string AUTOSPELL_I_SELF = "AUTOSPELL_I_SELF";
    public const string AUTOSPELL_I_TARGET = "AUTOSPELL_I_TARGET";
    public const string AUTOSPELL_I_SKILL = "AUTOSPELL_I_SKILL";
    public const string AUTOSPELL_I_SKILL_TO_TARGET = "AUTOSPELL_I_SKILL_TO_TARGET";
    #endregion

    [Serializable]
    public class JsonData
    {
        public List<Data> datas = new List<Data>();

        [Serializable]
        public class Data
        {
            public List<KeyData> keyDatas = new List<KeyData>();

            [Serializable]
            public class KeyData
            {
                public string key;
                public string thai;
                public string english;
            }
        }

    }

    [SerializeField] Dropdown _languageDropdown;

    string _currentLanguage;
    LocalizationDatabase _currentLocalizationDatabase = new LocalizationDatabase();

    void Start()
    {
        ParseJson();

        DropdownSetup();
    }

    void ParseJson()
    {
        var path = Application.dataPath + "/Assets/localization.json";

        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);

            if (!string.IsNullOrEmpty(json))
            {
                JsonData jsonData = null;

                try
                {
                    jsonData = JsonUtility.FromJson<JsonData>(json);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }

                if (jsonData != null)
                {
                    for (int i = 0; i < jsonData.datas.Count; i++)
                    {
                        _currentLocalizationDatabase = new LocalizationDatabase();

                        _currentLocalizationDatabase.datas = new Dictionary<string, LocalizationDatabase.Data>();

                        for (int j = 0; j < jsonData.datas[i].keyDatas.Count; j++)
                        {
                            LocalizationDatabase.Data data = new LocalizationDatabase.Data();
                            data.thai = jsonData.datas[i].keyDatas[j].thai;
                            data.english = jsonData.datas[i].keyDatas[j].english;

                            _currentLocalizationDatabase.datas.Add(jsonData.datas[i].keyDatas[j].key, data);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Get translated texts
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetTexts(string key)
    {
        if (_currentLocalizationDatabase.datas.ContainsKey(key))
        {
            if (_currentLanguage == THAI)
                return _currentLocalizationDatabase.datas[key].thai;
            else if (_currentLanguage == ENGLISH)
                return _currentLocalizationDatabase.datas[key].english;
            else
                return string.Empty;
        }
        else
            return string.Empty;
    }

    /// <summary>
    /// Setup language
    /// </summary>
    /// <param name="language"></param>
    public void SetupLanguage(string language)
    {
        _currentLanguage = language;
    }

    void DropdownSetup()
    {
        List<Dropdown.OptionData> dropdownList = new List<Dropdown.OptionData>();

        Dropdown.OptionData thaiDropdownOption = new Dropdown.OptionData();
        thaiDropdownOption.text = "Thai";
        dropdownList.Add(thaiDropdownOption);

        Dropdown.OptionData englishDropdownOption = new Dropdown.OptionData();
        englishDropdownOption.text = "English";
        dropdownList.Add(englishDropdownOption);

        _languageDropdown.AddOptions(dropdownList);

        _languageDropdown.value = 0;
        SetupLanguage(THAI);

        _languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
    }
    void OnLanguageChanged(int option)
    {
        if (option == 0)
            SetupLanguage(THAI);
        else if (option == 1)
            SetupLanguage(ENGLISH);
    }
}
