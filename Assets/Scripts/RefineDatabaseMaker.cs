using EasyButtons;
using System;
using System.Text;
using UnityEngine;
using System.IO;

public class RefineDatabaseMaker : MonoBehaviour
{
    // Constants

    public const int BASE_REFINE_RATE = 10000;
    public const int HD_REFINE_RATE_INCREASE = 500;
    public const int HD_REFINE_PRICE_MULTIPLIER = 2;

    public const int ARMOR_LEVEL_1_PRICE_BASE = 20000;
    public const int ARMOR_LEVEL_1_PRICE_INCREMENTAL = 20000;
    public const int ARMOR_LEVEL_1_BONUS_BASE = 100;
    public const int ARMOR_LEVEL_1_BONUS_INCREMENTAL = 310;
    public const int ARMOR_LEVEL_1_RANDOM_BONUS_BASE = 10;
    public const int ARMOR_LEVEL_1_RANDOM_BONUS_INCREMENTAL = 10;
    public const int ARMOR_LEVEL_1_REFINE_RATE_DECREASE = 520;
    public const string ARMOR_LEVEL_1_MATERIAL = "Elunium";
    public const string ARMOR_LEVEL_1_SPECIAL_MATERIAL = "Special_Elunium";

    public const int ARMOR_LEVEL_2_PRICE_BASE = 40000;
    public const int ARMOR_LEVEL_2_PRICE_INCREMENTAL = 40000;
    public const int ARMOR_LEVEL_2_BONUS_BASE = 120;
    public const int ARMOR_LEVEL_2_BONUS_INCREMENTAL = 370;
    public const int ARMOR_LEVEL_2_RANDOM_BONUS_BASE = 20;
    public const int ARMOR_LEVEL_2_RANDOM_BONUS_INCREMENTAL = 20;
    public const int ARMOR_LEVEL_2_REFINE_RATE_DECREASE = 525;
    public const string ARMOR_LEVEL_2_MATERIAL = "Carnium";
    public const string ARMOR_LEVEL_2_SPECIAL_MATERIAL = "Special_Carnium";

    public const int WEAPON_LEVEL_1_PRICE_BASE = 1000;
    public const int WEAPON_LEVEL_1_PRICE_INCREMENTAL = 1000;
    public const int WEAPON_LEVEL_1_BONUS_BASE = 200;
    public const int WEAPON_LEVEL_1_BONUS_INCREMENTAL = 305;
    public const int WEAPON_LEVEL_1_RANDOM_BONUS_BASE = 500;
    public const int WEAPON_LEVEL_1_RANDOM_BONUS_INCREMENTAL = 185;
    public const int WEAPON_LEVEL_1_REFINE_RATE_DECREASE = 505;
    public const string WEAPON_LEVEL_1_MATERIAL = "Phracon";
    public const string WEAPON_LEVEL_1_SPECIAL_MATERIAL = "Special_Phracon";

    public const int WEAPON_LEVEL_2_PRICE_BASE = 5000;
    public const int WEAPON_LEVEL_2_PRICE_INCREMENTAL = 5000;
    public const int WEAPON_LEVEL_2_BONUS_BASE = 300;
    public const int WEAPON_LEVEL_2_BONUS_INCREMENTAL = 510;
    public const int WEAPON_LEVEL_2_RANDOM_BONUS_BASE = 120;
    public const int WEAPON_LEVEL_2_RANDOM_BONUS_INCREMENTAL = 365;
    public const int WEAPON_LEVEL_2_REFINE_RATE_DECREASE = 510;
    public const string WEAPON_LEVEL_2_MATERIAL = "Emveretarcon";
    public const string WEAPON_LEVEL_2_SPECIAL_MATERIAL = "Special_Emveretarcon";

    public const int WEAPON_LEVEL_3_PRICE_BASE = 10000;
    public const int WEAPON_LEVEL_3_PRICE_INCREMENTAL = 10000;
    public const int WEAPON_LEVEL_3_BONUS_BASE = 500;
    public const int WEAPON_LEVEL_3_BONUS_INCREMENTAL = 710;
    public const int WEAPON_LEVEL_3_RANDOM_BONUS_BASE = 200;
    public const int WEAPON_LEVEL_3_RANDOM_BONUS_INCREMENTAL = 625;
    public const int WEAPON_LEVEL_3_REFINE_RATE_DECREASE = 515;
    public const string WEAPON_LEVEL_3_MATERIAL = "Oridecon";
    public const string WEAPON_LEVEL_3_SPECIAL_MATERIAL = "Special_Oridecon";

    public const int WEAPON_LEVEL_4_PRICE_BASE = 20000;
    public const int WEAPON_LEVEL_4_PRICE_INCREMENTAL = 20000;
    public const int WEAPON_LEVEL_4_BONUS_BASE = 700;
    public const int WEAPON_LEVEL_4_BONUS_INCREMENTAL = 1020;
    public const int WEAPON_LEVEL_4_RANDOM_BONUS_BASE = 300;
    public const int WEAPON_LEVEL_4_RANDOM_BONUS_INCREMENTAL = 1160;
    public const int WEAPON_LEVEL_4_REFINE_RATE_DECREASE = 520;
    public const string WEAPON_LEVEL_4_MATERIAL = "Oridecon";
    public const string WEAPON_LEVEL_4_SPECIAL_MATERIAL = "Special_Oridecon";

    public const int WEAPON_LEVEL_5_PRICE_BASE = 40000;
    public const int WEAPON_LEVEL_5_PRICE_INCREMENTAL = 40000;
    public const int WEAPON_LEVEL_5_BONUS_BASE = 800;
    public const int WEAPON_LEVEL_5_BONUS_INCREMENTAL = 800;
    public const int WEAPON_LEVEL_5_RANDOM_BONUS_BASE = 100;
    public const int WEAPON_LEVEL_5_RANDOM_BONUS_INCREMENTAL = 100;
    public const int WEAPON_LEVEL_5_REFINE_RATE_DECREASE = 525;
    public const string WEAPON_LEVEL_5_MATERIAL = "Bradium";
    public const string WEAPON_LEVEL_5_SPECIAL_MATERIAL = "Special_Bradium";

    public const int SHADOW_ARMOR_LEVEL_1_PRICE_BASE = 30000;
    public const int SHADOW_ARMOR_LEVEL_1_PRICE_INCREMENTAL = 30000;
    public const int SHADOW_ARMOR_LEVEL_1_BONUS_BASE = 1;
    public const int SHADOW_ARMOR_LEVEL_1_BONUS_INCREMENTAL = 1;
    public const int SHADOW_ARMOR_LEVEL_1_RANDOM_BONUS_BASE = 1;
    public const int SHADOW_ARMOR_LEVEL_1_RANDOM_BONUS_INCREMENTAL = 1;
    public const int SHADOW_ARMOR_LEVEL_1_REFINE_RATE_DECREASE = 500;
    public const string SHADOW_ARMOR_LEVEL_1_MATERIAL = "Elunium";
    public const string SHADOW_ARMOR_LEVEL_1_SPECIAL_MATERIAL = "Special_Elunium";

    public const int SHADOW_WEAPON_LEVEL_1_PRICE_BASE = 30000;
    public const int SHADOW_WEAPON_LEVEL_1_PRICE_INCREMENTAL = 30000;
    public const int SHADOW_WEAPON_LEVEL_1_BONUS_BASE = 1;
    public const int SHADOW_WEAPON_LEVEL_1_BONUS_INCREMENTAL = 1;
    public const int SHADOW_WEAPON_LEVEL_1_RANDOM_BONUS_BASE = 1;
    public const int SHADOW_WEAPON_LEVEL_1_RANDOM_BONUS_INCREMENTAL = 1;
    public const int SHADOW_WEAPON_LEVEL_1_REFINE_RATE_DECREASE = 500;
    public const string SHADOW_WEAPON_LEVEL_1_MATERIAL = "Elunium";
    public const string SHADOW_WEAPON_LEVEL_1_SPECIAL_MATERIAL = "Special_Elunium";

    // Local classes / structs

    [Serializable]
    public class RefineGroupData
    {
        public Group group;

        public EquipmentLevelData[] equipmentLevelDatas;

        [Serializable]
        public class EquipmentLevelData
        {
            public int groupLevel;
            public RefineLevelData[] refineLevelDatas;

            [Serializable]
            public class RefineLevelData
            {
                public int refineLevel;
                public int bonus;
                public int randomBonus;
                public RefineChanceData[] refineChanceDatas;

                [Serializable]
                public class RefineChanceData
                {
                    public Type type;
                    public int rate;
                    public int price;
                }
            }
        }
    }

    // Enums

    public enum Group { Armor, Weapon, Shadow_Armor, Shadow_Weapon }
    public enum Type { Normal, HD }
    public enum Material { Phracon, Emveretarcon, Oridecon, Elunium, SpecialPhracon, SpecialEmveretarcon, SpecialOridecon, SpecialElunium }

    // Variables

    public RefineGroupData[] refineGroupDatas;

    // Methods

    [Button()]
    public void Generate()
    {
        refineGroupDatas = new RefineGroupData[4];
        for (int i = 0; i < refineGroupDatas.Length; i++)
            refineGroupDatas[i] = new RefineGroupData();

        refineGroupDatas[0].group = Group.Armor;
        refineGroupDatas[1].group = Group.Weapon;
        refineGroupDatas[2].group = Group.Shadow_Armor;
        refineGroupDatas[3].group = Group.Shadow_Weapon;
        refineGroupDatas[0].equipmentLevelDatas = new RefineGroupData.EquipmentLevelData[2];
        refineGroupDatas[1].equipmentLevelDatas = new RefineGroupData.EquipmentLevelData[5];
        refineGroupDatas[2].equipmentLevelDatas = new RefineGroupData.EquipmentLevelData[1];
        refineGroupDatas[3].equipmentLevelDatas = new RefineGroupData.EquipmentLevelData[1];

        for (int i = 0; i < refineGroupDatas.Length; i++)
        {
            var group = refineGroupDatas[i].group;

            for (int j = 0; j < refineGroupDatas[i].equipmentLevelDatas.Length; j++)
            {
                refineGroupDatas[i].equipmentLevelDatas[j] = new RefineGroupData.EquipmentLevelData();

                var groupLevel = j + 1;
                refineGroupDatas[i].equipmentLevelDatas[j].groupLevel = groupLevel;
                refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas = new RefineGroupData.EquipmentLevelData.RefineLevelData[20];

                for (int k = 0; k < refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas.Length; k++)
                {
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k] = new RefineGroupData.EquipmentLevelData.RefineLevelData();

                    var refineLevel = k + 1;
                    var refineRate = GetRate(group, groupLevel, refineLevel);
                    var refinePrice = GetPrice(group, groupLevel, refineLevel);
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineLevel = refineLevel;
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].bonus = GetBonus(group, groupLevel, refineLevel);
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].randomBonus = GetRandomBonus(group, groupLevel, refineLevel);
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas = new RefineGroupData.EquipmentLevelData.RefineLevelData.RefineChanceData[2];
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[0] = new RefineGroupData.EquipmentLevelData.RefineLevelData.RefineChanceData();
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[0].type = Type.Normal;
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[0].rate = Mathf.Clamp(refineRate, 0, 10000);
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[0].price = refinePrice;
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[1] = new RefineGroupData.EquipmentLevelData.RefineLevelData.RefineChanceData();
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[1].type = Type.HD;
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[1].rate = Mathf.Clamp(refineRate + HD_REFINE_RATE_INCREASE, 0, 10000);
                    refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[1].price = refinePrice * HD_REFINE_PRICE_MULTIPLIER;
                }
            }
        }
    }

    int GetBonus(Group group, int groupLevel, int refineLevel)
    {
        refineLevel--;

        if (group == Group.Armor)
        {
            if (groupLevel == 1)
                return ARMOR_LEVEL_1_BONUS_BASE + (ARMOR_LEVEL_1_BONUS_INCREMENTAL * refineLevel);
            else
                return ARMOR_LEVEL_2_BONUS_BASE + (ARMOR_LEVEL_2_BONUS_INCREMENTAL * refineLevel);
        }
        else if (group == Group.Weapon)
        {
            if (groupLevel == 1)
                return WEAPON_LEVEL_1_BONUS_BASE + (WEAPON_LEVEL_1_BONUS_INCREMENTAL * refineLevel);
            else if (groupLevel == 2)
                return WEAPON_LEVEL_2_BONUS_BASE + (WEAPON_LEVEL_2_BONUS_INCREMENTAL * refineLevel);
            else if (groupLevel == 3)
                return WEAPON_LEVEL_3_BONUS_BASE + (WEAPON_LEVEL_3_BONUS_INCREMENTAL * refineLevel);
            else if (groupLevel == 4)
                return WEAPON_LEVEL_4_BONUS_BASE + (WEAPON_LEVEL_4_BONUS_INCREMENTAL * refineLevel);
            else
                return WEAPON_LEVEL_5_BONUS_BASE + (WEAPON_LEVEL_5_BONUS_INCREMENTAL * refineLevel);
        }
        else if (group == Group.Shadow_Armor)
        {
            if (groupLevel == 1)
                return SHADOW_ARMOR_LEVEL_1_BONUS_BASE + (SHADOW_ARMOR_LEVEL_1_BONUS_INCREMENTAL * refineLevel);
        }
        else if (group == Group.Shadow_Weapon)
        {
            if (groupLevel == 1)
                return SHADOW_WEAPON_LEVEL_1_BONUS_BASE + (SHADOW_WEAPON_LEVEL_1_BONUS_INCREMENTAL * refineLevel);
        }

        return 0;
    }

    int GetRandomBonus(Group group, int groupLevel, int refineLevel)
    {
        refineLevel--;

        if (group == Group.Armor)
        {
            if (groupLevel == 1)
                return ARMOR_LEVEL_1_RANDOM_BONUS_BASE + (ARMOR_LEVEL_1_RANDOM_BONUS_INCREMENTAL * refineLevel);
            else
                return ARMOR_LEVEL_2_RANDOM_BONUS_BASE + (ARMOR_LEVEL_2_RANDOM_BONUS_INCREMENTAL * refineLevel);
        }
        else if (group == Group.Weapon)
        {
            if (groupLevel == 1)
                return WEAPON_LEVEL_1_RANDOM_BONUS_BASE + (WEAPON_LEVEL_1_RANDOM_BONUS_INCREMENTAL * refineLevel);
            else if (groupLevel == 2)
                return WEAPON_LEVEL_2_RANDOM_BONUS_BASE + (WEAPON_LEVEL_2_RANDOM_BONUS_INCREMENTAL * refineLevel);
            else if (groupLevel == 3)
                return WEAPON_LEVEL_3_RANDOM_BONUS_BASE + (WEAPON_LEVEL_3_RANDOM_BONUS_INCREMENTAL * refineLevel);
            else if (groupLevel == 4)
                return WEAPON_LEVEL_4_RANDOM_BONUS_BASE + (WEAPON_LEVEL_4_RANDOM_BONUS_INCREMENTAL * refineLevel);
            else
                return WEAPON_LEVEL_5_RANDOM_BONUS_BASE + (WEAPON_LEVEL_5_RANDOM_BONUS_INCREMENTAL * refineLevel);
        }
        else if (group == Group.Shadow_Armor)
        {
            if (groupLevel == 1)
                return SHADOW_ARMOR_LEVEL_1_RANDOM_BONUS_BASE + (SHADOW_ARMOR_LEVEL_1_RANDOM_BONUS_INCREMENTAL * refineLevel);
        }
        else if (group == Group.Shadow_Weapon)
        {
            if (groupLevel == 1)
                return SHADOW_WEAPON_LEVEL_1_RANDOM_BONUS_BASE + (SHADOW_WEAPON_LEVEL_1_RANDOM_BONUS_INCREMENTAL * refineLevel);
        }

        return 0;
    }

    int GetPrice(Group group, int groupLevel, int refineLevel)
    {
        refineLevel--;

        if (group == Group.Armor)
        {
            if (groupLevel == 1)
                return ARMOR_LEVEL_1_PRICE_BASE + (ARMOR_LEVEL_1_PRICE_INCREMENTAL * refineLevel);
            else
                return ARMOR_LEVEL_2_PRICE_BASE + (ARMOR_LEVEL_2_PRICE_INCREMENTAL * refineLevel);
        }
        else if (group == Group.Weapon)
        {
            if (groupLevel == 1)
                return WEAPON_LEVEL_1_PRICE_BASE + (WEAPON_LEVEL_1_PRICE_INCREMENTAL * refineLevel);
            else if (groupLevel == 2)
                return WEAPON_LEVEL_2_PRICE_BASE + (WEAPON_LEVEL_2_PRICE_INCREMENTAL * refineLevel);
            else if (groupLevel == 3)
                return WEAPON_LEVEL_3_PRICE_BASE + (WEAPON_LEVEL_3_PRICE_INCREMENTAL * refineLevel);
            else if (groupLevel == 4)
                return WEAPON_LEVEL_4_PRICE_BASE + (WEAPON_LEVEL_4_PRICE_INCREMENTAL * refineLevel);
            else
                return WEAPON_LEVEL_5_PRICE_BASE + (WEAPON_LEVEL_5_PRICE_INCREMENTAL * refineLevel);
        }
        else if (group == Group.Shadow_Armor)
        {
            if (groupLevel == 1)
                return SHADOW_ARMOR_LEVEL_1_PRICE_BASE + (SHADOW_ARMOR_LEVEL_1_PRICE_INCREMENTAL * refineLevel);
        }
        else if (group == Group.Shadow_Weapon)
        {
            if (groupLevel == 1)
                return SHADOW_WEAPON_LEVEL_1_PRICE_BASE + (SHADOW_WEAPON_LEVEL_1_PRICE_INCREMENTAL * refineLevel);
        }

        return 0;
    }

    int GetRate(Group group, int groupLevel, int refineLevel)
    {
        refineLevel--;

        if (group == Group.Armor)
        {
            if (groupLevel == 1)
                return BASE_REFINE_RATE - (ARMOR_LEVEL_1_REFINE_RATE_DECREASE * refineLevel);
            else
                return BASE_REFINE_RATE - (ARMOR_LEVEL_2_REFINE_RATE_DECREASE * refineLevel);
        }
        else if (group == Group.Weapon)
        {
            if (groupLevel == 1)
                return BASE_REFINE_RATE - (WEAPON_LEVEL_1_REFINE_RATE_DECREASE * refineLevel);
            else if (groupLevel == 2)
                return BASE_REFINE_RATE - (WEAPON_LEVEL_2_REFINE_RATE_DECREASE * refineLevel);
            else if (groupLevel == 3)
                return BASE_REFINE_RATE - (WEAPON_LEVEL_3_REFINE_RATE_DECREASE * refineLevel);
            else if (groupLevel == 4)
                return BASE_REFINE_RATE - (WEAPON_LEVEL_4_REFINE_RATE_DECREASE * refineLevel);
            else
                return BASE_REFINE_RATE - (WEAPON_LEVEL_5_REFINE_RATE_DECREASE * refineLevel);
        }
        else if (group == Group.Shadow_Armor)
        {
            if (groupLevel == 1)
                return BASE_REFINE_RATE - (SHADOW_ARMOR_LEVEL_1_REFINE_RATE_DECREASE * refineLevel);
        }
        else if (group == Group.Shadow_Weapon)
        {
            if (groupLevel == 1)
                return BASE_REFINE_RATE - (SHADOW_WEAPON_LEVEL_1_REFINE_RATE_DECREASE * refineLevel);
        }

        return 0;
    }

    string GetMaterial(Group group, int groupLevel)
    {
        if (group == Group.Armor)
        {
            if (groupLevel == 1)
                return ARMOR_LEVEL_1_MATERIAL;
            else
                return ARMOR_LEVEL_2_MATERIAL;
        }
        else if (group == Group.Weapon)
        {
            if (groupLevel == 1)
                return WEAPON_LEVEL_1_MATERIAL;
            else if (groupLevel == 2)
                return WEAPON_LEVEL_2_MATERIAL;
            else if (groupLevel == 3)
                return WEAPON_LEVEL_3_MATERIAL;
            else if (groupLevel == 4)
                return WEAPON_LEVEL_4_MATERIAL;
            else
                return WEAPON_LEVEL_5_MATERIAL;
        }
        else if (group == Group.Shadow_Armor)
        {
            if (groupLevel == 1)
                return SHADOW_ARMOR_LEVEL_1_MATERIAL;
        }
        else if (group == Group.Shadow_Weapon)
        {
            if (groupLevel == 1)
                return SHADOW_WEAPON_LEVEL_1_MATERIAL;
        }

        return string.Empty;
    }

    string GetSpecialMaterial(Group group, int groupLevel)
    {
        if (group == Group.Armor)
        {
            if (groupLevel == 1)
                return ARMOR_LEVEL_1_SPECIAL_MATERIAL;
            else
                return ARMOR_LEVEL_2_SPECIAL_MATERIAL;
        }
        else if (group == Group.Weapon)
        {
            if (groupLevel == 1)
                return WEAPON_LEVEL_1_SPECIAL_MATERIAL;
            else if (groupLevel == 2)
                return WEAPON_LEVEL_2_SPECIAL_MATERIAL;
            else if (groupLevel == 3)
                return WEAPON_LEVEL_3_SPECIAL_MATERIAL;
            else if (groupLevel == 4)
                return WEAPON_LEVEL_4_SPECIAL_MATERIAL;
            else
                return WEAPON_LEVEL_5_SPECIAL_MATERIAL;
        }
        else if (group == Group.Shadow_Armor)
        {
            if (groupLevel == 1)
                return SHADOW_ARMOR_LEVEL_1_SPECIAL_MATERIAL;
        }
        else if (group == Group.Shadow_Weapon)
        {
            if (groupLevel == 1)
                return SHADOW_WEAPON_LEVEL_1_SPECIAL_MATERIAL;
        }

        return string.Empty;
    }

    [Button()]
    public void ExportRefineDatabase()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("Header:\n");
        builder.Append("  Type: REFINE_DB\n");
        builder.Append("  Version: 2\n");
        builder.Append("\n");
        builder.Append("Body:\n");

        for (int i = 0; i < refineGroupDatas.Length; i++)
        {
            var group = refineGroupDatas[i].group;

            builder.Append("  - Group: " + group + "\n");
            builder.Append("    Levels:\n");

            for (int j = 0; j < refineGroupDatas[i].equipmentLevelDatas.Length; j++)
            {
                var groupLevel = refineGroupDatas[i].equipmentLevelDatas[j].groupLevel;

                builder.Append("      - Level: " + groupLevel + "\n");
                builder.Append("        RefineLevels:\n");

                for (int k = 0; k < refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas.Length; k++)
                {
                    builder.Append("          - Level: " + refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineLevel + "\n");
                    if (refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].bonus > 0)
                        builder.Append("            Bonus: " + refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].bonus + "\n");
                    if (refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].randomBonus > 0)
                        builder.Append("            RandomBonus: " + refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].randomBonus + "\n");
                    builder.Append("            Chances:\n");

                    for (int l = 0; l < refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas.Length; l++)
                    {
                        var type = refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[l].type;
                        builder.Append("              - Type: " + refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[l].type + "\n");
                        if (refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[l].rate > 0)
                            builder.Append("                Rate: " + refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[l].rate + "\n");
                        if (refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[l].price > 0)
                            builder.Append("                Price: " + refineGroupDatas[i].equipmentLevelDatas[j].refineLevelDatas[k].refineChanceDatas[l].price + "\n");
                        builder.Append("                Material: " + ((type == Type.Normal) ? GetMaterial(group, groupLevel) : GetSpecialMaterial(group, groupLevel)) + "\n");
                        if (type == Type.Normal)
                            builder.Append("                DowngradeAmount: 1\n");
                    }
                }
            }
        }

        File.WriteAllText("refine.yml", builder.ToString(), Encoding.UTF8);

        Debug.Log("'refine.yml' has been successfully created.");
    }
}
