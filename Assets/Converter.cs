using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using EasyButtons;
using System;
using System.Text;

public class Converter : MonoBehaviour
{
    bool isConvertError;

    public HardcodeItemScripts hardcodeItemScripts;

    public int testItemComboId;

    public bool isUseTestTextAsset;
    public bool isOnlyUseCustomTextAsset;

    string id;
    string _name;
    string type;
    string subType;
    string buy;
    string weight;
    string atk;
    string mAtk;
    string def;
    string atkRange;
    string slots;
    bool isJob;
    string jobs;
    bool isClass;
    string classes;
    string gender;
    string location;
    string weaponLv;
    string armorLv;
    string equipLevelMin;
    string equipLevelMax;
    string refineable;
    string view;
    bool isScript;
    string script;
    bool isEquipScript;
    string equipScript;
    bool isUnEquipScript;
    string unEquipScript;

    void Clean()
    {
        id = string.Empty;
        _name = string.Empty;
        type = string.Empty;
        subType = string.Empty;
        buy = string.Empty;
        weight = string.Empty;
        atk = string.Empty;
        mAtk = string.Empty;
        def = string.Empty;
        atkRange = string.Empty;
        slots = string.Empty;
        isJob = false;
        jobs = string.Empty;
        isClass = false;
        classes = string.Empty;
        gender = string.Empty;
        location = string.Empty;
        weaponLv = string.Empty;
        armorLv = string.Empty;
        equipLevelMin = string.Empty;
        equipLevelMax = string.Empty;
        refineable = string.Empty;
        view = string.Empty;
        isScript = false;
        script = string.Empty;
        isEquipScript = false;
        equipScript = string.Empty;
        isUnEquipScript = false;
        unEquipScript = string.Empty;
    }

    [Button]
    public void PrintAllItemTypeToArray()
    {
        string builder = string.Empty;
        builder += "setarray $weaponIds[0],";
        foreach (var item in weaponIds)
            builder += item + ",";
        builder = builder.Substring(0, builder.Length - 1);
        builder += ";\n";
        builder += "setarray $equipmentIds[0],";
        foreach (var item in equipmentIds)
            builder += item + ",";
        builder = builder.Substring(0, builder.Length - 1);
        builder += ";\n";
        builder += "setarray $costumeIds[0],";
        foreach (var item in costumeIds)
            builder += item + ",";
        builder = builder.Substring(0, builder.Length - 1);
        builder += ";\n";
        builder += "setarray $cardIds[0],";
        foreach (var item in cardIds)
            builder += item + ",";
        builder = builder.Substring(0, builder.Length - 1);
        builder += ";\n";
        builder += "setarray $enchantIds[0],";
        foreach (var item in enchantIds)
            builder += item + ",";
        builder = builder.Substring(0, builder.Length - 1);
        builder += ";\n";
        File.WriteAllText("global_item_ids.txt", builder, Encoding.UTF8);
        Debug.Log("Printed all item type.");
    }
    [Button]
    public void PrintAllItemIdToItemMall()
    {
        int shopNumber = 0;

        string builder = string.Empty;

        for (int i = 0; i < allItemIds.Count; i++)
        {
            if ((i == 0)
                || (i % 100 == 0))
            {
                if (!string.IsNullOrEmpty(builder))
                    builder = builder.Substring(0, builder.Length - 1);

                shopNumber++;

                builder += "\n-	shop	ItemMall" + shopNumber + "	-1,no,";
            }

            builder += allItemIds[i] + ":1000000000,";
        }

        builder = builder.Substring(0, builder.Length - 1);

        File.WriteAllText("item_mall.txt", builder, Encoding.UTF8);

        Debug.Log("Printed item mall.");
    }
    [Button]
    public void CheckAdditionalRequirement()
    {
        Debug.Log("resourceNameDatas.Count:" + resourceNameDatas.Count);
        Debug.Log("comboDatas.Count:" + comboDatas.Count);
        Debug.Log("idNameDatas.Count:" + idNameDatas.Count);
        Debug.Log("skillDatas.Count:" + skillDatas.Count);
        Debug.Log("classNumDatas.Count:" + classNumDatas.Count);
        Debug.Log("weaponIds.Count:" + weaponIds.Count);
        Debug.Log("equipmentIds.Count:" + equipmentIds.Count);
        Debug.Log("costumeIds.Count:" + costumeIds.Count);
        Debug.Log("cardIds.Count:" + cardIds.Count);
        Debug.Log("enchantIds.Count:" + enchantIds.Count);
        Debug.Log("allItemIds.Count:" + allItemIds.Count);
        Debug.Log(GetCombo(GetIdNameData(testItemComboId).aegisName));
    }

    List<string> weaponIds = new List<string>();
    List<string> equipmentIds = new List<string>();
    List<string> costumeIds = new List<string>();
    List<string> cardIds = new List<string>();
    List<string> enchantIds = new List<string>();
    List<string> allItemIds = new List<string>();

    [Button]
    public void FetchResourceNameWithType()
    {
        var allTextAsset = File.ReadAllText(Application.dataPath + "/Assets/item_db_equip.yml");
        var lines = allTextAsset.Split('\n');
        resNameDagger = new List<string>();
        resName1hSword = new List<string>();
        resName2hSword = new List<string>();
        resName1hSpear = new List<string>();
        resName2hSpear = new List<string>();
        resName1hAxe = new List<string>();
        resName2hAxe = new List<string>();
        resNameMace = new List<string>();
        resNameStaff = new List<string>();
        resNameBow = new List<string>();
        resNameKnuckle = new List<string>();
        resNameMusical = new List<string>();
        resNameWhip = new List<string>();
        resNameBook = new List<string>();
        resNameKatar = new List<string>();
        resNameRevolver = new List<string>();
        resNameRifle = new List<string>();
        resNameGatling = new List<string>();
        resNameShotgun = new List<string>();
        resNameGrenade = new List<string>();
        resNameHuuma = new List<string>();
        //resName2hStaff = new List<string>();
        resNameHead_Top = new List<string>();
        resNameHead_Mid = new List<string>();
        resNameHead_Low = new List<string>();
        resNameArmor = new List<string>();
        resNameGarment = new List<string>();
        resNameShoes = new List<string>();
        resNameAccessory = new List<string>();

        string id = string.Empty;
        bool isArmor = false;
        for (int i = 0; i < lines.Length; i++)
        {
            var text = lines[i];

            text = RemoveCommentAndUnwantedWord(text);

            // Skip these
            if (text.Contains("    Buy:")
                || text.Contains("    Sell:")
                || text.Contains("    Jobs:")
                || text.Contains("    Classes:")
                || text.Contains("    AliasName:")
                || text.Contains("    Flags:")
                || text.Contains("    BuyingStore:")
                || text.Contains("    DeadBranch:")
                || text.Contains("    Container:")
                || text.Contains("    UniqueId:")
                || text.Contains("    BindOnEquip:")
                || text.Contains("    DropAnnounce:")
                || text.Contains("    NoConsume:")
                || text.Contains("    DropEffect:")
                || text.Contains("    Delay:")
                || text.Contains("    Duration:")
                || text.Contains("    Status:")
                || text.Contains("    Stack:")
                || text.Contains("    Amount:")
                || text.Contains("    Inventory:")
                || text.Contains("    Cart:")
                || text.Contains("    Storage:")
                || text.Contains("    GuildStorage:")
                || text.Contains("    NoUse:")
                || text.Contains("    Override:")
                || text.Contains("    Sitting:")
                || text.Contains("    Trade:")
                || text.Contains("    NoDrop:")
                || text.Contains("    NoTrade:")
                || text.Contains("    TradePartner:")
                || text.Contains("    NoSell:")
                || text.Contains("    NoCart:")
                || text.Contains("    NoStorage:")
                || text.Contains("    NoGuildStorage:")
                || text.Contains("    NoMail:")
                || text.Contains("    NoAuction:")
                || text.Contains("    Script:")
                || text.Contains("    OnEquip_Script:")
                || text.Contains("    OnUnequip_Script:")
                )
                text = string.Empty;

            // Id
            if (text.Contains("  - Id:"))
            {
                text = RemoveSpace(text);
                id = text.Replace("-Id:", string.Empty);
            }
            // Type
            else if (text.Contains("    Type:"))
            {
                text = text.Replace("    Type: ", string.Empty);
                text = RemoveQuote(text);
                text = RemoveSpace(text);
                if (text.ToLower() == "armor" || text.ToLower() == "shadowgear")
                    isArmor = true;
                else
                    isArmor = false;
            }
            // Locations
            else if (isArmor)
            {
                text = RemoveQuote(text);
                text = RemoveSpace(text);
                if (text.ToLower().Contains("costume_head_top") || text.ToLower().Contains("head_top"))
                    resNameHead_Top.Add(id);
                else if (text.ToLower().Contains("costume_head_mid") || text.ToLower().Contains("head_mid"))
                    resNameHead_Mid.Add(id);
                else if (text.ToLower().Contains("costume_head_low") || text.ToLower().Contains("head_low"))
                    resNameHead_Low.Add(id);
                else if (text.ToLower().Contains("costume_garment") || text.ToLower().Contains("garment"))
                    resNameGarment.Add(id);
                else if (text.ToLower().Contains("shadow_armor") || text.ToLower().Contains("armor"))
                    resNameArmor.Add(id);
                else if (text.ToLower().Contains("shadow_weapon") || text.ToLower().Contains("shadow_shield"))
                    resNameShield.Add(id);
                else if (text.ToLower().Contains("shadow_shoes") || text.ToLower().Contains("shoes"))
                    resNameShoes.Add(id);
                else if (text.ToLower().Contains("shadow_right_accessory") || text.ToLower().Contains("shadow_left_accessory") || text.ToLower().Contains("right_accessory") || text.ToLower().Contains("left_accessory") || text.ToLower().Contains("both_accessory"))
                    resNameAccessory.Add(id);
            }
            else
            {
                if (text.ToLower().Contains("dagger"))
                    resNameDagger.Add(id);
                else if (text.ToLower().Contains("1hsword"))
                    resName1hSword.Add(id);
                else if (text.ToLower().Contains("2hsword"))
                    resName2hSword.Add(id);
                else if (text.ToLower().Contains("1hspear"))
                    resName1hSpear.Add(id);
                else if (text.ToLower().Contains("2hspear"))
                    resName2hSpear.Add(id);
                else if (text.ToLower().Contains("1haxe"))
                    resName1hAxe.Add(id);
                else if (text.ToLower().Contains("2haxe"))
                    resName2hAxe.Add(id);
                else if (text.ToLower().Contains("mace"))
                    resNameMace.Add(id);
                else if (text.ToLower().Contains("staff"))
                    resNameStaff.Add(id);
                else if (text.ToLower().Contains("bow"))
                    resNameBow.Add(id);
                else if (text.ToLower().Contains("knuckle"))
                    resNameKnuckle.Add(id);
                else if (text.ToLower().Contains("musical"))
                    resNameMusical.Add(id);
                else if (text.ToLower().Contains("whip"))
                    resNameWhip.Add(id);
                else if (text.ToLower().Contains("book"))
                    resNameBook.Add(id);
                else if (text.ToLower().Contains("katar"))
                    resNameKatar.Add(id);
                else if (text.ToLower().Contains("revolver"))
                    resNameRevolver.Add(id);
                else if (text.ToLower().Contains("rifle"))
                    resNameRifle.Add(id);
                else if (text.ToLower().Contains("gatling"))
                    resNameGatling.Add(id);
                else if (text.ToLower().Contains("shotgun"))
                    resNameShotgun.Add(id);
                else if (text.ToLower().Contains("grenade"))
                    resNameGrenade.Add(id);
                else if (text.ToLower().Contains("huuma"))
                    resNameHuuma.Add(id);
                //else if (text.ToLower().Contains("2hstaff"))
                //    resName2hStaff.Add(id);
            }
        }
        Debug.Log(resNameDagger.Count);
        Debug.Log(resName1hSword.Count);
        Debug.Log(resName2hSword.Count);
        Debug.Log(resName1hSpear.Count);
        Debug.Log(resName2hSpear.Count);
        Debug.Log(resName1hAxe.Count);
        Debug.Log(resName2hAxe.Count);
        Debug.Log(resNameMace.Count);
        Debug.Log(resNameStaff.Count);
        Debug.Log(resNameBow.Count);
        Debug.Log(resNameKnuckle.Count);
        Debug.Log(resNameMusical.Count);
        Debug.Log(resNameWhip.Count);
        Debug.Log(resNameBook.Count);
        Debug.Log(resNameKatar.Count);
        Debug.Log(resNameRevolver.Count);
        Debug.Log(resNameRifle.Count);
        Debug.Log(resNameGatling.Count);
        Debug.Log(resNameShotgun.Count);
        Debug.Log(resNameGrenade.Count);
        Debug.Log(resNameHuuma.Count);
        //Debug.Log(resName2hStaff.Count);
        Debug.Log(resNameHead_Top.Count);
        Debug.Log(resNameHead_Mid.Count);
        Debug.Log(resNameHead_Low.Count);
        Debug.Log(resNameArmor.Count);
        Debug.Log(resNameShield.Count);
        Debug.Log(resNameGarment.Count);
        Debug.Log(resNameShoes.Count);
        Debug.Log(resNameAccessory.Count);
    }

    List<string> resNameDagger = new List<string>();
    List<string> resName1hSword = new List<string>();
    List<string> resName2hSword = new List<string>();
    List<string> resName1hSpear = new List<string>();
    List<string> resName2hSpear = new List<string>();
    List<string> resName1hAxe = new List<string>();
    List<string> resName2hAxe = new List<string>();
    List<string> resNameMace = new List<string>();
    List<string> resNameStaff = new List<string>();
    List<string> resNameBow = new List<string>();
    List<string> resNameKnuckle = new List<string>();
    List<string> resNameMusical = new List<string>();
    List<string> resNameWhip = new List<string>();
    List<string> resNameBook = new List<string>();
    List<string> resNameKatar = new List<string>();
    List<string> resNameRevolver = new List<string>();
    List<string> resNameRifle = new List<string>();
    List<string> resNameGatling = new List<string>();
    List<string> resNameShotgun = new List<string>();
    List<string> resNameGrenade = new List<string>();
    List<string> resNameHuuma = new List<string>();
    //List<string> resName2hStaff = new List<string>();
    List<string> resNameHead_Top = new List<string>();
    List<string> resNameHead_Mid = new List<string>();
    List<string> resNameHead_Low = new List<string>();
    List<string> resNameArmor = new List<string>();
    List<string> resNameShield = new List<string>();
    List<string> resNameGarment = new List<string>();
    List<string> resNameShoes = new List<string>();
    List<string> resNameAccessory = new List<string>();

    [Button]
    public void FetchMonsterName()
    {
        if (!File.Exists(Application.dataPath + "/Assets/mob_db.yml"))
        {
            isConvertError = true;
            return;
        }
        var mobDb = File.ReadAllText(Application.dataPath + "/Assets/mob_db.yml");
        var lines = mobDb.Split('\n');
        monsterNameDatas = new List<MonsterNameData>();
        MonsterNameData monsterNameData = new MonsterNameData();
        for (int i = 0; i < lines.Length; i++)
        {
            var text = lines[i];

            // Null
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                continue;

            text = RemoveCommentAndUnwantedWord(text);

            // Id
            if (text.Contains("  - Id:"))
                monsterNameData.id = int.Parse(RemoveSpace(text).Replace("-Id:", string.Empty));
            // Name
            else if (text.Contains("    Name:"))
            {
                monsterNameData.monsterName = RemoveQuote(text.Replace("    Name: ", string.Empty));
                monsterNameDatas.Add(monsterNameData);
                monsterNameData = new MonsterNameData();
            }
        }
        Debug.Log("monsterNameDatas.Count:" + monsterNameDatas.Count);
    }

    List<MonsterNameData> monsterNameDatas = new List<MonsterNameData>();

    [Serializable]
    public class MonsterNameData
    {
        public int id;
        public string monsterName;
    }

    [Button]
    public void FetchClassNum()
    {
        if (!File.Exists(Application.dataPath + "/Assets/classNum.txt"))
        {
            isConvertError = true;
            return;
        }
        var classNum = File.ReadAllText(Application.dataPath + "/Assets/classNum.txt");
        var lines = classNum.Split('\n');
        classNumDatas = new List<ClassNumData>();
        ClassNumData classNumData = new ClassNumData();
        for (int i = 0; i < lines.Length; i++)
        {
            var text = lines[i];

            // Null
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                continue;

            // Get Ids first
            var texts = text.Split('=');
            classNumData.id = int.Parse(texts[0]);
            classNumData.classNum = texts[1].Replace("\r", string.Empty).Replace("/n", string.Empty);
            classNumDatas.Add(classNumData);
            classNumData = new ClassNumData();
        }
        Debug.Log("classNumDatas.Count:" + classNumDatas.Count);

        if (!File.Exists(Application.dataPath + "/Assets/item_db_custom.txt"))
            return;

        classNum = File.ReadAllText(Application.dataPath + "/Assets/item_db_custom.txt");
        lines = classNum.Split('\n');
        classNumData = new ClassNumData();
        for (int i = 0; i < lines.Length; i++)
        {
            var text = lines[i];

            // Null
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                continue;

            // Id
            if (text.Contains("- Id:"))
                classNumData.id = int.Parse(RemoveSpace(text).Replace("-Id:", string.Empty));
            // Name
            else if (text.Contains("    View:"))
            {
                classNumData.classNum = RemoveSpace(text).Replace("View:", string.Empty);
                classNumDatas.Add(classNumData);
                classNumData = new ClassNumData();
            }
        }
        Debug.Log("classNumDatas.Count:" + classNumDatas.Count);
    }

    List<ClassNumData> classNumDatas = new List<ClassNumData>();

    [Serializable]
    public class ClassNumData
    {
        public int id;
        public string classNum;
    }

    [Button]
    public void FetchSkill()
    {
        if (!File.Exists(Application.dataPath + "/Assets/skill_db.yml"))
        {
            isConvertError = true;
            return;
        }
        var skillDb = File.ReadAllText(Application.dataPath + "/Assets/skill_db.yml");
        var lines = skillDb.Split('\n');
        skillDatas = new List<SkillData>();
        SkillData skillData = new SkillData();
        for (int i = 0; i < lines.Length; i++)
        {
            var text = lines[i];

            // Null
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                continue;

            text = RemoveCommentAndUnwantedWord(text);

            // Id
            if (text.Contains("  - Id:"))
                skillData.id = int.Parse(RemoveSpace(text).Replace("-Id:", string.Empty));
            // Name
            else if (text.Contains("    Name:"))
                skillData.name = RemoveQuote(text.Replace("    Name: ", string.Empty));
            // Description
            else if (text.Contains("    Description:"))
            {
                skillData.description = RemoveQuote(text.Replace("    Description: ", string.Empty));
                skillDatas.Add(skillData);
                skillData = new SkillData();
            }
        }
        Debug.Log("skillDatas.Count:" + skillDatas.Count);
    }

    List<SkillData> skillDatas = new List<SkillData>();

    [Serializable]
    public class SkillData
    {
        public int id;
        public string name;
        public string description;
    }

    [Button]
    public void FetchResourceName()
    {
        if (!File.Exists(Application.dataPath + "/Assets/resourceName.txt"))
        {
            isConvertError = true;
            return;
        }
        var resourceName = File.ReadAllText(Application.dataPath + "/Assets/resourceName.txt");
        var lines = resourceName.Split('\n');
        resourceNameDatas = new List<ResourceNameData>();
        ResourceNameData resourceNameData = new ResourceNameData();
        for (int i = 0; i < lines.Length; i++)
        {
            var text = lines[i];

            // Null
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                continue;

            // Get Ids first
            //Debug.Log(text);
            var texts = text.Split('=');
            resourceNameData.id = int.Parse(texts[0]);
            resourceNameData.resourceName = texts[1].Replace("\r", string.Empty).Replace("/n", string.Empty);
            resourceNameDatas.Add(resourceNameData);
            resourceNameData = new ResourceNameData();
        }
        Debug.Log("resourceNameDatas.Count:" + resourceNameDatas.Count);
    }

    List<ResourceNameData> resourceNameDatas = new List<ResourceNameData>();

    [Serializable]
    public class ResourceNameData
    {
        public int id;
        public string resourceName;
    }

    [Button]
    public void FetchCombo()
    {
        // Not found
        if (!File.Exists(Application.dataPath + "/Assets/item_combos.yml"))
        {
            isConvertError = true;
            return;
        }

        // Found
        var itemComboDb = File.ReadAllText(Application.dataPath + "/Assets/item_combos.yml");

        // Custom text asset check
        if (isOnlyUseCustomTextAsset)
            itemComboDb = string.Empty;

        // Split
        var lines = itemComboDb.Split('\n');

        // New combo data list
        comboDatas = new List<ComboData>();

        // New combo data
        ComboData comboData = new ComboData();

        bool isScript = false;

        string script = string.Empty;

        for (int i = 0; i < lines.Length; i++)
        {
            var text = lines[i];
            //Debug.Log("Line:" + i + " >> text:" + text);

            // Comment remover
            if (text.Contains("//"))
                text = string.Empty;

            int retry = 30;

            // Unexpected error
            if (lines[i].Contains("/*") && !lines[i].Contains("*/"))
            {
                int retryUnexpected = 30;
                while (retryUnexpected > 0)
                {
                    retryUnexpected--;

                    int incrementCommentCheck = 1;
                    if ((i + incrementCommentCheck < lines.Length)
                        && lines[i + incrementCommentCheck].Contains("*/"))
                    {
                        while (incrementCommentCheck > 0)
                        {
                            lines[i + incrementCommentCheck] = string.Empty;
                            incrementCommentCheck--;
                        }
                    }
                    else
                        incrementCommentCheck++;
                }
            }

            while (text.Contains("/*"))
            {
                var copier = text;
                if (!copier.Contains("*/"))
                    text = copier.Substring(0, copier.IndexOf("/*"));
                else
                    text = copier.Substring(0, copier.IndexOf("/*")) + copier.Substring(copier.IndexOf("*/") + 2);
                retry--;
                if (retry <= 0)
                    break;
            }

            retry = 30;
            while (text.Contains("*/"))
            {
                text = text.Replace("*/", string.Empty);
                retry--;
                if (retry <= 0)
                    break;
            }

            text = text.Replace("\\", string.Empty);
            //Debug.Log(text);

            text = RemoveCommentAndUnwantedWord(text);

            // New combo data
            if (text.Contains("- Combos:"))
            {
                comboData = new ComboData();
                comboDatas.Add(comboData);
                isScript = false;
                script = string.Empty;
            }
            // New same combo data
            else if (text.Contains("- Combo:"))
                comboData.sameComboDatas.Add(new ComboData.SameComboData());
            // Name
            else if (text.Contains("          - "))
                comboData.sameComboDatas[comboData.sameComboDatas.Count - 1].aegis_names.Add(RemoveQuote(text.Replace("          - ", string.Empty)));
            // Description
            else if (text.Contains("Script: |"))
                isScript = true;
            else if (isScript)
            {
                var sum = ConvertItemBonus(text);
                if (!string.IsNullOrEmpty(sum))
                    script += "			\"" + sum + "\",\n";
                // Check if next line is new combo data or last line
                if ((i + 1) >= lines.Length || lines[i + 1].Contains("- Combos:"))
                    comboData.descriptions.Add(script);
            }
        }
        Debug.Log("comboDatas.Count:" + comboDatas.Count);
    }

    List<ComboData> comboDatas = new List<ComboData>();

    [Serializable]
    public class ComboData
    {
        [Serializable]
        public class SameComboData
        {
            public List<string> aegis_names = new List<string>();
        }

        public List<SameComboData> sameComboDatas = new List<SameComboData>();

        public List<string> descriptions = new List<string>();

        public bool IsAegisNameContain(string aegis_name)
        {
            for (int i = 0; i < sameComboDatas.Count; i++)
            {
                if (sameComboDatas[i].aegis_names.Contains(aegis_name))
                    return true;
            }
            return false;
        }
    }

    [Button]
    public void FetchIdName()
    {
        if (!File.Exists(Application.dataPath + "/Assets/item_db_equip.yml")
            || !File.Exists(Application.dataPath + "/Assets/item_db_usable.yml")
            || !File.Exists(Application.dataPath + "/Assets/item_db_etc.yml"))
        {
            isConvertError = true;
            return;
        }

        var allTextAsset = File.ReadAllText(Application.dataPath + "/Assets/item_db_equip.yml") + "\n"
            + File.ReadAllText(Application.dataPath + "/Assets/item_db_usable.yml") + "\n"
            + File.ReadAllText(Application.dataPath + "/Assets/item_db_etc.yml") + "\n"
            + (File.Exists(Application.dataPath + "/Assets/item_db_custom.txt") ? File.ReadAllText(Application.dataPath + "/Assets/item_db_custom.txt") : string.Empty);
        var lines = allTextAsset.Split('\n');
        idNameDatas = new List<IdNameData>();
        weaponIds = new List<string>();
        equipmentIds = new List<string>();
        costumeIds = new List<string>();
        cardIds = new List<string>();
        enchantIds = new List<string>();
        allItemIds = new List<string>();
        IdNameData idNameData = new IdNameData();
        bool isArmor = false;
        for (int i = 0; i < lines.Length; i++)
        {
            var text = lines[i];

            text = RemoveCommentAndUnwantedWord(text);

            // Skip these
            if (text.Contains("    Buy:")
                || text.Contains("    Sell:")
                || text.Contains("    Jobs:")
                || text.Contains("    Classes:")
                || text.Contains("    AliasName:")
                || text.Contains("    Flags:")
                || text.Contains("    BuyingStore:")
                || text.Contains("    DeadBranch:")
                || text.Contains("    Container:")
                || text.Contains("    UniqueId:")
                || text.Contains("    BindOnEquip:")
                || text.Contains("    DropAnnounce:")
                || text.Contains("    NoConsume:")
                || text.Contains("    DropEffect:")
                || text.Contains("    Delay:")
                || text.Contains("    Duration:")
                || text.Contains("    Status:")
                || text.Contains("    Stack:")
                || text.Contains("    Amount:")
                || text.Contains("    Inventory:")
                || text.Contains("    Cart:")
                || text.Contains("    Storage:")
                || text.Contains("    GuildStorage:")
                || text.Contains("    NoUse:")
                || text.Contains("    Override:")
                || text.Contains("    Sitting:")
                || text.Contains("    Trade:")
                || text.Contains("    NoDrop:")
                || text.Contains("    NoTrade:")
                || text.Contains("    TradePartner:")
                || text.Contains("    NoSell:")
                || text.Contains("    NoCart:")
                || text.Contains("    NoStorage:")
                || text.Contains("    NoGuildStorage:")
                || text.Contains("    NoMail:")
                || text.Contains("    NoAuction:")
                || text.Contains("    Script:")
                || text.Contains("    OnEquip_Script:")
                || text.Contains("    OnUnequip_Script:")
                )
                text = string.Empty;

            // Id
            if (text.Contains("  - Id:"))
            {
                text = RemoveSpace(text);
                id = text.Replace("-Id:", string.Empty);
                idNameData.id = int.Parse(id);

                allItemIds.Add(id);
            }
            // Aegis Name
            else if (text.Contains("    AegisName:"))
            {
                _name = text.Replace("    AegisName: ", string.Empty);
                _name = RemoveQuote(_name);
                idNameData.aegisName = _name;
            }
            // Name
            else if (text.Contains("    Name:"))
            {
                _name = text.Replace("    Name: ", string.Empty);
                _name = RemoveQuote(_name);
                idNameData._name = _name;
                idNameDatas.Add(idNameData);
                idNameData = new IdNameData();
            }
            // Type
            else if (text.Contains("    Type:"))
            {
                text = text.Replace("    Type: ", string.Empty);
                text = RemoveQuote(text);
                text = RemoveSpace(text);
                isArmor = false;
                if (text.ToLower() == "weapon")
                    weaponIds.Add(id);
                else if (text.ToLower() == "armor")
                    isArmor = true;
                else if (text.ToLower() == "card" && _name.ToLower().Contains(" card"))
                    cardIds.Add(id);
                else if (text.ToLower() == "card" && !_name.ToLower().Contains(" card"))
                    enchantIds.Add(id);
            }
            // Locations
            else if (isArmor)
            {
                text = RemoveQuote(text);
                text = RemoveSpace(text);
                if (text.ToLower().Contains("costume_head_top")
                   || text.ToLower().Contains("costume_head_mid")
                   || text.ToLower().Contains("costume_head_low")
                   || text.ToLower().Contains("costume_garment")
                   || text.ToLower().Contains("shadow_armor")
                   || text.ToLower().Contains("shadow_weapon")
                   || text.ToLower().Contains("shadow_shield")
                   || text.ToLower().Contains("shadow_shoes")
                   || text.ToLower().Contains("shadow_right_accessory")
                   || text.ToLower().Contains("shadow_left_accessory")
                   )
                {
                    costumeIds.Add(id);

                    // Always clear isArmor
                    isArmor = false;
                }
                else if (text.ToLower().Contains("head_top")
                   || text.ToLower().Contains("head_mid")
                   || text.ToLower().Contains("head_low")
                   || text.ToLower().Contains("armor")
                   || text.ToLower().Contains("left_hand")
                   || text.ToLower().Contains("garment")
                   || text.ToLower().Contains("shoes")
                   || text.ToLower().Contains("right_accessory")
                   || text.ToLower().Contains("left_accessory")
                   || text.ToLower().Contains("both_accessory")
                   )
                {
                    equipmentIds.Add(id);

                    // Always clear isArmor
                    isArmor = false;
                }
            }
            // Locations
            else if (!isArmor)
            {
                text = RemoveQuote(text);
                text = RemoveSpace(text);
                if (text.ToLower().Contains("costume_head_top")
                    || text.ToLower().Contains("costume_head_mid")
                    || text.ToLower().Contains("costume_head_low")
                    || text.ToLower().Contains("costume_garment")
                    || text.ToLower().Contains("shadow_armor")
                    || text.ToLower().Contains("shadow_weapon")
                    || text.ToLower().Contains("shadow_shield")
                    || text.ToLower().Contains("shadow_shoes")
                    || text.ToLower().Contains("shadow_right_accessory")
                    || text.ToLower().Contains("shadow_left_accessory")
                    )
                {
                    costumeIds.Add(id);

                    // Always clear isArmor
                    isArmor = false;
                }
            }
        }

        Debug.Log("idNameDatas.Count:" + idNameDatas.Count);
        Debug.Log("weaponIds.Count:" + weaponIds.Count);
        Debug.Log("equipmentIds.Count:" + equipmentIds.Count);
        Debug.Log("costumeIds.Count:" + costumeIds.Count);
        Debug.Log("cardIds.Count:" + cardIds.Count);
        Debug.Log("enchantIds.Count:" + enchantIds.Count);
        Debug.Log("allItemIds.Count:" + allItemIds.Count);
    }

    List<IdNameData> idNameDatas = new List<IdNameData>();

    [Serializable]
    public class IdNameData
    {
        public int id;
        public string aegisName;
        public string _name;
    }

    [SerializeField] Text txtProgression;
    [SerializeField] Button btnConvert;
    [SerializeField] Button btnCredit;
    void Start()
    {
        btnConvert.onClick.AddListener(OnConvertButtonTap);
        btnCredit.onClick.AddListener(OnCreditButtonTap);
    }
    void OnCreditButtonTap()
    {
        Application.OpenURL("https://kanintemsrisukgames.wordpress.com/2019/04/05/support-kt-games/");
    }
    [SerializeField] GameObject objConvertBtn;
    [SerializeField] GameObject objConvertProgression;
    [SerializeField] Text txtConvertProgression;
    void OnConvertButtonTap()
    {
        objConvertBtn.SetActive(false);

        StartCoroutine(StandAloneConvert());
    }
    public IEnumerator StandAloneConvert()
    {
        objConvertProgression.SetActive(true);
        txtConvertProgression.text = "Starting now...";

        isConvertError = false;

        yield return new WaitForSeconds(1);

        txtConvertProgression.text = "Fetching id and name data...";
        yield return null;
        FetchIdName();
        if (isConvertError)
        {
            txtConvertProgression.text = "<color=red>Error</color> - Fetching id and name data...";
            yield break;
        }

        yield return new WaitForSeconds(1);

        txtConvertProgression.text = "Fetching resource name data...";
        yield return null;
        FetchResourceName();
        if (isConvertError)
        {
            txtConvertProgression.text = "<color=red>Error</color> - Fetching resource name data...";
            yield break;
        }

        yield return new WaitForSeconds(1);

        txtConvertProgression.text = "Fetching skill data...";
        yield return null;
        FetchSkill();
        if (isConvertError)
        {
            txtConvertProgression.text = "<color=red>Error</color> - Fetching skill data...";
            yield break;
        }

        yield return new WaitForSeconds(1);

        txtConvertProgression.text = "Fetching class number data...";
        yield return null;
        FetchClassNum();
        if (isConvertError)
        {
            txtConvertProgression.text = "<color=red>Error</color> - Fetching class number data...";
            yield break;
        }

        yield return new WaitForSeconds(1);

        txtConvertProgression.text = "Fetching monster name data...";
        yield return null;
        FetchMonsterName();
        if (isConvertError)
        {
            txtConvertProgression.text = "<color=red>Error</color> - Fetching monster name data...";
            yield break;
        }

        yield return new WaitForSeconds(1);

        txtConvertProgression.text = "Fetching combo data...";
        yield return null;
        FetchCombo();
        if (isConvertError)
        {
            txtConvertProgression.text = "<color=red>Error</color> - Fetching combo data...";
            yield break;
        }

        yield return new WaitForSeconds(1);

        txtConvertProgression.text = "Please wait around 1 minutes.";

        yield return new WaitForSeconds(1);

        Invoke("Convert", 1);
    }

    [SerializeField] bool isRandomizeResourceName;
    [SerializeField] bool isRandomizeResourceNameCustomItemOnly;

    [Button]
    public void Convert()
    {
        Debug.Log(DateTime.UtcNow);

        Clean();

        StringBuilder builder = new StringBuilder();
        var allTextAsset = File.ReadAllText(Application.dataPath + "/Assets/item_db_equip.yml") + "\n"
            + File.ReadAllText(Application.dataPath + "/Assets/item_db_usable.yml") + "\n"
            + File.ReadAllText(Application.dataPath + "/Assets/item_db_etc.yml") + "\n"
            + (File.Exists(Application.dataPath + "/Assets/item_db_custom.txt") ? File.ReadAllText(Application.dataPath + "/Assets/item_db_custom.txt") : string.Empty);
        if (isOnlyUseCustomTextAsset)
            allTextAsset = File.ReadAllText(Application.dataPath + "/Assets/item_db_custom.txt");
        var lines = allTextAsset.Split('\n');
        if (isUseTestTextAsset)
            lines = File.ReadAllText(Application.dataPath + "/Assets/item_db_test.txt").Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            // Unexpected error
            if (lines[i].Contains("/*") && !lines[i].Contains("*/"))
            {
                int retryUnexpected = 30;
                while (retryUnexpected > 0)
                {
                    retryUnexpected--;

                    int incrementCommentCheck = 1;
                    if ((i + incrementCommentCheck < lines.Length)
                        && lines[i + incrementCommentCheck].Contains("*/"))
                    {
                        while (incrementCommentCheck > 0)
                        {
                            lines[i + incrementCommentCheck] = string.Empty;
                            incrementCommentCheck--;
                        }
                    }
                    else
                        incrementCommentCheck++;
                }
            }

            var text = lines[i];

            text = RemoveCommentAndUnwantedWord(text);

            var nextText = i + 1 < lines.Length ? lines[i + 1] : string.Empty;
            var nextNextText = i + 2 < lines.Length ? lines[i + 2] : string.Empty;

            #region Description
            // Skip these
            if (text.Contains("    AegisName:")
                //|| text.Contains("    Buy:")
                || text.Contains("    Sell:")
                || text.Contains("    Jobs:")
                || text.Contains("    Classes:")
                || text.Contains("    Locations:")
                || text.Contains("    AliasName:")
                || text.Contains("    Flags:")
                || text.Contains("    BuyingStore:")
                || text.Contains("    DeadBranch:")
                || text.Contains("    Container:")
                || text.Contains("    UniqueId:")
                || text.Contains("    BindOnEquip:")
                || text.Contains("    DropAnnounce:")
                || text.Contains("    NoConsume:")
                || text.Contains("    DropEffect:")
                || text.Contains("    Delay:")
                || text.Contains("    Duration:")
                || text.Contains("    Status:")
                || text.Contains("    Stack:")
                || text.Contains("    Amount:")
                || text.Contains("    Inventory:")
                || text.Contains("    Cart:")
                || text.Contains("    Storage:")
                || text.Contains("    GuildStorage:")
                || text.Contains("    NoUse:")
                || text.Contains("    Override:")
                || text.Contains("    Sitting:")
                || text.Contains("    Trade:")
                || text.Contains("    NoDrop:")
                || text.Contains("    NoTrade:")
                || text.Contains("    TradePartner:")
                || text.Contains("    NoSell:")
                || text.Contains("    NoCart:")
                || text.Contains("    NoStorage:")
                || text.Contains("    NoGuildStorage:")
                || text.Contains("    NoMail:")
                || text.Contains("    NoAuction:")
                || text.Contains("    Script:")
                || text.Contains("    OnEquip_Script:")
                || text.Contains("    OnUnequip_Script:")
                )
            {
                if (text.Contains("    Jobs:"))
                {
                    isJob = true;
                    isClass = false;
                    isScript = false;
                    isEquipScript = false;
                    isUnEquipScript = false;
                }
                if (text.Contains("    Classes:"))
                {
                    isJob = false;
                    isClass = true;
                    isScript = false;
                    isEquipScript = false;
                    isUnEquipScript = false;
                }
                if (text.Contains("    Script:"))
                {
                    isJob = false;
                    isClass = false;
                    isScript = true;
                    isEquipScript = false;
                    isUnEquipScript = false;
                }
                if (text.Contains("    OnEquip_Script:"))
                {
                    isJob = false;
                    isClass = false;
                    isScript = false;
                    isEquipScript = true;
                    isUnEquipScript = false;
                }
                if (text.Contains("    OnUnequip_Script:"))
                {
                    isJob = false;
                    isClass = false;
                    isScript = false;
                    isEquipScript = false;
                    isUnEquipScript = true;
                }

                text = string.Empty;
            }

            // Id
            if (text.Contains("  - Id:"))
            {
                text = RemoveSpace(text);
                id = text.Replace("-Id:", string.Empty);
            }
            // Name
            else if (text.Contains("    Name:"))
            {
                _name = text.Replace("    Name: ", string.Empty);
                _name = RemoveQuote(_name);
            }
            // Type
            else if (text.Contains("    Type:"))
                type = text.Replace("    Type: ", string.Empty);
            // SubType
            else if (text.Contains("    SubType:"))
                subType = text.Replace("    SubType: ", string.Empty);
            // Buy
            else if (text.Contains("    Buy:"))
            {
                text = text.Replace("    Buy: ", string.Empty);
                buy = TryParseInt(text);
            }
            // Weight
            else if (text.Contains("    Weight:"))
            {
                text = text.Replace("    Weight: ", string.Empty);
                weight = TryParseInt(text, 10);
            }
            // Attack
            else if (text.Contains("    Attack:"))
                atk = text.Replace("    Attack: ", string.Empty);
            // Magic Attack
            else if (text.Contains("    MagicAttack:"))
                mAtk = text.Replace("    MagicAttack: ", string.Empty);
            // Defense
            else if (text.Contains("    Defense:"))
                def = text.Replace("    Defense: ", string.Empty);
            // Range
            else if (text.Contains("    Range:"))
                atkRange = text.Replace("    Range: ", string.Empty);
            // Slots
            else if (text.Contains("    Slots:"))
                slots = text.Replace("    Slots: ", string.Empty);
            // Jobs
            #region Jobs
            else if (isJob && text.Contains("      All: true"))
                jobs += "ทุกอาชีพ, ";
            else if (isJob && text.Contains("      All: false"))
                jobs += "ทุกอาชีพ [x], ";
            else if (isJob && text.Contains("      Acolyte: true"))
                jobs += "Acolyte, ";
            else if (isJob && text.Contains("      Acolyte: false"))
                jobs += "Acolyte [x], ";
            else if (isJob && text.Contains("      Alchemist: true"))
                jobs += "Alchemist, ";
            else if (isJob && text.Contains("      Alchemist: false"))
                jobs += "Alchemist [x], ";
            else if (isJob && text.Contains("      Archer: true"))
                jobs += "Archer, ";
            else if (isJob && text.Contains("      Archer: false"))
                jobs += "Archer [x], ";
            else if (isJob && text.Contains("      Assassin: true"))
                jobs += "Assassin, ";
            else if (isJob && text.Contains("      Assassin: false"))
                jobs += "Assassin [x], ";
            else if (isJob && text.Contains("      BardDancer: true"))
                jobs += "Bard & Dancer, ";
            else if (isJob && text.Contains("      BardDancer: false"))
                jobs += "Bard & Dancer [x], ";
            else if (isJob && text.Contains("      Blacksmith: true"))
                jobs += "Blacksmith, ";
            else if (isJob && text.Contains("      Blacksmith: false"))
                jobs += "Blacksmith [x], ";
            else if (isJob && text.Contains("      Crusader: true"))
                jobs += "Crusader, ";
            else if (isJob && text.Contains("      Crusader: false"))
                jobs += "Crusader [x], ";
            else if (isJob && text.Contains("      Gunslinger: true"))
                jobs += "Gunslinger, ";
            else if (isJob && text.Contains("      Gunslinger: false"))
                jobs += "Gunslinger [x], ";
            else if (isJob && text.Contains("      Hunter: true"))
                jobs += "Hunter, ";
            else if (isJob && text.Contains("      Hunter: false"))
                jobs += "Hunter [x], ";
            else if (isJob && text.Contains("      KagerouOboro: true"))
                jobs += "Kagerou & Oboro, ";
            else if (isJob && text.Contains("      KagerouOboro: false"))
                jobs += "Kagerou & Oboro [x], ";
            else if (isJob && text.Contains("      Knight: true"))
                jobs += "Knight, ";
            else if (isJob && text.Contains("      Knight: false"))
                jobs += "Knight [x], ";
            else if (isJob && text.Contains("      Mage: true"))
                jobs += "Mage, ";
            else if (isJob && text.Contains("      Mage: false"))
                jobs += "Mage [x], ";
            else if (isJob && text.Contains("      Merchant: true"))
                jobs += "Merchant, ";
            else if (isJob && text.Contains("      Merchant: false"))
                jobs += "Merchant [x], ";
            else if (isJob && text.Contains("      Monk: true"))
                jobs += "Monk, ";
            else if (isJob && text.Contains("      Monk: false"))
                jobs += "Monk [x], ";
            else if (isJob && text.Contains("      Ninja: true"))
                jobs += "Ninja, ";
            else if (isJob && text.Contains("      Ninja: false"))
                jobs += "Ninja [x], ";
            else if (isJob && text.Contains("      Novice: true"))
                jobs += "Novice, ";
            else if (isJob && text.Contains("      Novice: false"))
                jobs += "Novice [x], ";
            else if (isJob && text.Contains("      Priest: true"))
                jobs += "Priest, ";
            else if (isJob && text.Contains("      Priest: false"))
                jobs += "Priest [x], ";
            else if (isJob && text.Contains("      Rebellion: true"))
                jobs += "Rebellion, ";
            else if (isJob && text.Contains("      Rebellion: false"))
                jobs += "Rebellion [x], ";
            else if (isJob && text.Contains("      Rogue: true"))
                jobs += "Rogue, ";
            else if (isJob && text.Contains("      Sage: false"))
                jobs += "Sage [x], ";
            else if (isJob && text.Contains("      Sage: true"))
                jobs += "Sage, ";
            else if (isJob && text.Contains("      Rogue: false"))
                jobs += "Rogue [x], ";
            else if (isJob && text.Contains("      SoulLinker: true"))
                jobs += "Soul Linker, ";
            else if (isJob && text.Contains("      SoulLinker: false"))
                jobs += "Soul Linker [x], ";
            else if (isJob && text.Contains("      StarGladiator: true"))
                jobs += "Star Gladiator, ";
            else if (isJob && text.Contains("      StarGladiator: false"))
                jobs += "Star Gladiator [x], ";
            else if (isJob && text.Contains("      Summoner: true"))
                jobs += "Summoner, ";
            else if (isJob && text.Contains("      Summoner: false"))
                jobs += "Summoner [x], ";
            else if (isJob && text.Contains("      SuperNovice: true"))
                jobs += "Super Novice, ";
            else if (isJob && text.Contains("      SuperNovice: false"))
                jobs += "Super Novice [x], ";
            else if (isJob && text.Contains("      Swordman: true"))
                jobs += "Swordman, ";
            else if (isJob && text.Contains("      Swordman: false"))
                jobs += "Swordman [x], ";
            else if (isJob && text.Contains("      Taekwon: true"))
                jobs += "Taekwon, ";
            else if (isJob && text.Contains("      Taekwon: false"))
                jobs += "Taekwon [x], ";
            else if (isJob && text.Contains("      Thief: true"))
                jobs += "Thief, ";
            else if (isJob && text.Contains("      Thief: false"))
                jobs += "Thief [x], ";
            else if (isJob && text.Contains("      Wizard: true"))
                jobs += "Wizard, ";
            else if (isJob && text.Contains("      Wizard: false"))
                jobs += "Wizard [x], ";
            #endregion
            // Classes
            #region Classes
            else if (isClass && text.Contains("      All: true"))
                classes += "ทุกคลาส, ";
            else if (isClass && text.Contains("      All: false"))
                classes += "ทุกคลาส [x], ";
            else if (isClass && text.Contains("      Normal: true"))
                classes += "คลาส 1, ";
            else if (isClass && text.Contains("      Normal: false"))
                classes += "คลาส 1 [x], ";
            else if (isClass && text.Contains("      Upper: true"))
                classes += "คลาส 2, ";
            else if (isClass && text.Contains("      Upper: false"))
                classes += "คลาส 2 [x], ";
            else if (isClass && text.Contains("      Baby: true"))
                classes += "คลาส 1 หรือ 2 Baby, ";
            else if (isClass && text.Contains("      Baby: false"))
                classes += "คลาส 1 หรือ 2 Baby [x], ";
            else if (isClass && text.Contains("      Third: true"))
                classes += "คลาส 3, ";
            else if (isClass && text.Contains("      Third: false"))
                classes += "คลาส 3 [x], ";
            else if (isClass && text.Contains("      Third_Upper: true"))
                classes += "คลาส 3 Trans, ";
            else if (isClass && text.Contains("      Third_Upper: false"))
                classes += "คลาส 3 Trans [x], ";
            else if (isClass && text.Contains("      Third_Baby: true"))
                classes += "คลาส 3 Baby, ";
            else if (isClass && text.Contains("      Third_Baby: false"))
                classes += "คลาส 3 Baby [x], ";
            else if (isClass && text.Contains("      All_Upper: true"))
                classes += "คลาส 2 หรือคลาส 3 Trans, ";
            else if (isClass && text.Contains("      All_Upper: false"))
                classes += "คลาส 2 หรือคลาส 3 Trans [x], ";
            else if (isClass && text.Contains("      All_Baby: true"))
                classes += "คลาส Baby, ";
            else if (isClass && text.Contains("      All_Baby: false"))
                classes += "คลาส Baby [x], ";
            else if (isClass && text.Contains("      All_Third: true"))
                classes += "คลาส 3, ";
            else if (isClass && text.Contains("      All_Third: false"))
                classes += "คลาส 3 [x], ";
            else if (isClass && text.Contains("      Fourth: true"))
                classes += "คลาส 4, ";
            else if (isClass && text.Contains("      Fourth: false"))
                classes += "คลาส 4 [x], ";
            else if (isClass && text.Contains("      Fourth_Baby: true"))
                classes += "คลาส 4 Baby, ";
            else if (isClass && text.Contains("      Fourth_Baby: false"))
                classes += "คลาส 4 Baby [x], ";
            else if (isClass && text.Contains("      All_Fourth: true"))
                classes += "คลาส 4, ";
            else if (isClass && text.Contains("      All_Fourth: false"))
                classes += "คลาส 4 [x], ";
            #endregion
            // Gender
            #region Gender
            else if (text.Contains("      Female: true"))
                gender += "หญิง, ";
            else if (text.Contains("      Female: false"))
                gender += "หญิง [x], ";
            else if (text.Contains("      Male: true"))
                gender += "ชาย, ";
            else if (text.Contains("      Male: false"))
                gender += "ชาย [x], ";
            else if (text.Contains("      Both: true"))
                gender += "ทุกเพศ, ";
            else if (text.Contains("      Both: false"))
                gender += "ทุกเพศ [x], ";
            #endregion
            // Location
            #region Location
            else if (text.Contains("      Head_Top: true"))
                location += "หมวกส่วนบน, ";
            else if (text.Contains("      Head_Top: false"))
                location += "หมวกส่วนบน [x], ";
            else if (text.Contains("      Head_Mid: true"))
                location += "หมวกส่วนกลาง, ";
            else if (text.Contains("      Head_Mid: false"))
                location += "หมวกส่วนกลาง [x], ";
            else if (text.Contains("      Head_Low: true"))
                location += "หมวกส่วนล่าง, ";
            else if (text.Contains("      Head_Low: false"))
                location += "หมวกส่วนล่าง [x], ";
            else if (text.Contains("      Armor: true"))
                location += "ชุดเกราะ, ";
            else if (text.Contains("      Armor: false"))
                location += "ชุดเกราะ [x], ";
            else if (text.Contains("      Right_Hand: true"))
                location += "มือขวา, ";
            else if (text.Contains("      Right_Hand: false"))
                location += "มือขวา [x], ";
            else if (text.Contains("      Left_Hand: true"))
                location += "มือซ้าย, ";
            else if (text.Contains("      Left_Hand: false"))
                location += "มือซ้าย [x], ";
            else if (text.Contains("      Garment: true"))
                location += "ผ้าคลุม, ";
            else if (text.Contains("      Garment: false"))
                location += "ผ้าคลุม [x], ";
            else if (text.Contains("      Shoes: true"))
                location += "รองเท้า, ";
            else if (text.Contains("      Shoes: false"))
                location += "รองเท้า [x], ";
            else if (text.Contains("      Right_Accessory: true"))
                location += "ประดับข้างขวา, ";
            else if (text.Contains("      Right_Accessory: false"))
                location += "ประดับข้างขวา [x], ";
            else if (text.Contains("      Left_Accessory: true"))
                location += "ประดับข้างซ้าย, ";
            else if (text.Contains("      Left_Accessory: false"))
                location += "ประดับข้างซ้าย [x], ";
            else if (text.Contains("      Costume_Head_Top: true"))
                location += "หมวกส่วนบน Costume, ";
            else if (text.Contains("      Costume_Head_Top: false"))
                location += "หมวกส่วนบน Costume [x], ";
            else if (text.Contains("      Costume_Head_Mid: true"))
                location += "หมวกส่วนกลาง Costume, ";
            else if (text.Contains("      Costume_Head_Mid: false"))
                location += "หมวกส่วนกลาง Costume [x], ";
            else if (text.Contains("      Costume_Head_Low: true"))
                location += "หมวกส่วนล่าง Costume, ";
            else if (text.Contains("      Costume_Head_Low: false"))
                location += "หมวกส่วนล่าง Costume [x], ";
            else if (text.Contains("      Costume_Garment: true"))
                location += "ผ้าคลุม Costume, ";
            else if (text.Contains("      Costume_Garment: false"))
                location += "ผ้าคลุม Costume [x], ";
            else if (text.Contains("      Ammo: true"))
                location += "กระสุน, ";
            else if (text.Contains("      Ammo: false"))
                location += "กระสุน [x], ";
            else if (text.Contains("      Shadow_Armor: true"))
                location += "ชุดเกราะ Shadow, ";
            else if (text.Contains("      Shadow_Armor: false"))
                location += "ชุดเกราะ Shadow [x], ";
            else if (text.Contains("      Shadow_Weapon: true"))
                location += "อาวุธ Shadow, ";
            else if (text.Contains("      Shadow_Weapon: false"))
                location += "อาวุธ Shadow [x], ";
            else if (text.Contains("      Shadow_Shield: true"))
                location += "โล่ Shadow, ";
            else if (text.Contains("      Shadow_Shield: false"))
                location += "โล่ Shadow [x], ";
            else if (text.Contains("      Shadow_Shoes: true"))
                location += "รองเท้า Shadow, ";
            else if (text.Contains("      Shadow_Shoes: false"))
                location += "รองเท้า Shadow [x], ";
            else if (text.Contains("      Shadow_Right_Accessory: true"))
                location += "ประดับ Shadow ข้างขวา, ";
            else if (text.Contains("      Shadow_Right_Accessory: false"))
                location += "ประดับ Shadow ข้างขวา [x], ";
            else if (text.Contains("      Shadow_Left_Accessory: true"))
                location += "ประดับ Shadow ข้างซ้าย, ";
            else if (text.Contains("      Shadow_Left_Accessory: false"))
                location += "ประดับ Shadow ข้างซ้าย [x], ";
            else if (text.Contains("      Both_Hand: true"))
                location += "สองมือ, ";
            else if (text.Contains("      Both_Hand: false"))
                location += "สองมือ [x], ";
            else if (text.Contains("      Both_Accessory: true"))
                location += "ประดับสองข้าง, ";
            else if (text.Contains("      Both_Accessory: false"))
                location += "ประดับสองข้าง [x], ";
            #endregion
            // Weapon Level
            else if (text.Contains("    WeaponLevel:"))
                weaponLv = text.Replace("    WeaponLevel: ", string.Empty);
            // Armor Level
            else if (text.Contains("    ArmorLevel:"))
                armorLv = text.Replace("    ArmorLevel: ", string.Empty);
            // Equip Level Min
            else if (text.Contains("    EquipLevelMin:"))
                equipLevelMin = text.Replace("    EquipLevelMin: ", string.Empty);
            // Equip Level Max
            else if (text.Contains("    EquipLevelMax:"))
                equipLevelMax = text.Replace("    EquipLevelMax: ", string.Empty);
            // Refineable
            else if (text.Contains("    Refineable: true"))
                refineable = "ได้";
            else if (text.Contains("    Refineable: false"))
                refineable = "ไม่ได้";
            // View
            else if (text.Contains("    View:"))
                view = text.Replace("    View: ", string.Empty);
            #endregion
            // Script
            else if (isScript)
            {
                var sum = ConvertItemBonus(text);
                if (!string.IsNullOrEmpty(sum))
                    script += "			\"" + sum + "\",\n";
            }
            // Equip Script
            else if (isEquipScript)
            {
                var sum = ConvertItemBonus(text);
                if (!string.IsNullOrEmpty(sum))
                    equipScript += "			\"" + sum + "\",\n";
            }
            // Unequip Script
            else if (isUnEquipScript)
            {
                var sum = ConvertItemBonus(text);
                if (!string.IsNullOrEmpty(sum))
                    unEquipScript += "			\"" + sum + "\",\n";
            }

            // Write builder now
            if (nextText.Contains("- Id:") && !string.IsNullOrEmpty(id) && !string.IsNullOrWhiteSpace(id) || (i + 1) >= lines.Length)
            {
                var resName = GetResourceNameFromId(int.Parse(id), type, subType, !string.IsNullOrEmpty(location) ? location.Substring(0, location.Length - 2) : string.Empty);
                // Id
                builder.Append("	[" + id + "] = {\n");
                // Unidentified display name
                builder.Append("		unidentifiedDisplayName = \"" + _name + "\",\n");
                // Unidentified resource name
                builder.Append("		unidentifiedResourceName = " + resName + ",\n");
                // Unidentified description
                builder.Append("		unidentifiedDescriptionName = {\n");
                builder.Append("			\"\"\n");
                builder.Append("		},\n");
                // Identified display name
                builder.Append("		identifiedDisplayName = \"" + _name + "\",\n");
                // Identified resource name
                builder.Append("		identifiedResourceName = " + resName + ",\n");
                // Identified description
                builder.Append("		identifiedDescriptionName = {\n");
                // Description here
                var sumCombo = GetCombo(GetIdNameData(int.Parse(id)).aegisName);
                //var sumCombo = string.Empty;
                string hardcodeBonus = hardcodeItemScripts.GetHardcodeItemScript(int.Parse(id));
                var sumBonus = !string.IsNullOrEmpty(hardcodeBonus) ? hardcodeBonus : !string.IsNullOrEmpty(script) ? script : string.Empty;
                var sumEquipBonus = !string.IsNullOrEmpty(equipScript) ? "			\"^666478[เมื่อสวมใส่]^000000\",\n" + equipScript : string.Empty;
                var sumUnEquipBonus = !string.IsNullOrEmpty(unEquipScript) ? "			\"^666478[เมื่อถอด]^000000\",\n" + unEquipScript : string.Empty;
                var sumDesc = "			\"^3F28FFID:^000000 " + id + "\",\n"
                    + "			\"^3F28FFประเภท:^000000 " + type + "\",\n";
                if (!string.IsNullOrEmpty(subType))
                    sumDesc += "			\"^3F28FFประเภทรอง:^000000 " + subType + "\",\n";
                if (!string.IsNullOrEmpty(location))
                    sumDesc += "			\"^3F28FFตำแหน่ง:^000000 " + location.Substring(0, location.Length - 2) + "\",\n";
                if (!string.IsNullOrEmpty(jobs))
                    sumDesc += "			\"^3F28FFอาชีพ:^000000 " + jobs.Substring(0, jobs.Length - 2) + "\",\n";
                if (!string.IsNullOrEmpty(classes))
                    sumDesc += "			\"^3F28FFคลาส:^000000 " + classes.Substring(0, classes.Length - 2) + "\",\n";
                if (!string.IsNullOrEmpty(gender))
                    sumDesc += "			\"^3F28FFเพศ:^000000 " + gender.Substring(0, gender.Length - 2) + "\",\n";
                if (!string.IsNullOrEmpty(atk))
                    sumDesc += "			\"^3F28FFโจมตี:^000000 " + atk + "\",\n";
                if (!string.IsNullOrEmpty(mAtk))
                    sumDesc += "			\"^3F28FFโจมตีเวทย์:^000000 " + mAtk + "\",\n";
                if (!string.IsNullOrEmpty(def))
                    sumDesc += "			\"^3F28FFป้องกัน:^000000 " + def + "\",\n";
                if (!string.IsNullOrEmpty(atkRange))
                    sumDesc += "			\"^3F28FFระยะตี:^000000 " + atkRange + "\",\n";
                if (!string.IsNullOrEmpty(weaponLv))
                    sumDesc += "			\"^3F28FFเลเวลอาวุธ:^000000 " + weaponLv + "\",\n";
                if (!string.IsNullOrEmpty(armorLv))
                    sumDesc += "			\"^3F28FFเลเวลชุดเกราะ:^000000 " + armorLv + "\",\n";
                if (!string.IsNullOrEmpty(equipLevelMin))
                    sumDesc += "			\"^3F28FFเลเวลขั้นต่ำ:^000000 " + equipLevelMin + "\",\n";
                if (!string.IsNullOrEmpty(equipLevelMax))
                    sumDesc += "			\"^3F28FFเลเวลสูงสุด:^000000 " + equipLevelMax + "\",\n";
                if (!string.IsNullOrEmpty(refineable))
                    sumDesc += "			\"^3F28FFตีบวก:^000000 " + refineable + "\",\n";
                if (!string.IsNullOrEmpty(weight))
                    sumDesc += "			\"^3F28FFน้ำหนัก:^000000 " + weight + "\",\n";
                if (!string.IsNullOrEmpty(buy))
                    sumDesc += "			\"^3F28FFราคา:^000000 " + buy + "\",\n";
                builder.Append(sumBonus);
                if (!string.IsNullOrEmpty(sumBonus) && !string.IsNullOrWhiteSpace(sumBonus))
                    builder.Append("			\"^58990F[สิ้นสุด Bonus]^000000" + "\",\n");
                builder.Append(sumCombo);
                builder.Append(sumEquipBonus);
                builder.Append(sumUnEquipBonus);
                builder.Append(sumDesc);
                builder.Append("			\"\"\n");
                builder.Append("		},\n");
                // Slot Count
                if (!string.IsNullOrEmpty(slots))
                    builder.Append("		slotCount = " + slots + ",\n");
                else
                    builder.Append("		slotCount = 0,\n");
                // View / Class Number
                builder.Append("		ClassNum = " + GetClassNumFromId(int.Parse(id)) + ",\n");
                // Costume
                builder.Append("		costume = false\n");
                if (string.IsNullOrEmpty(nextNextText) || string.IsNullOrWhiteSpace(nextNextText))
                    builder.Append("	}\n");
                else
                    builder.Append("	},\n");

                Clean();
            }
        }
        #region prefix postfix
        var prefix = "tbl = {\n";
        var postfix = "}\n\n" +
            "-- Function #0\n" +
            "main = function()\n" +
            "	for ItemID, DESC in pairs(tbl) do\n" +
            "		result, msg = AddItem(ItemID, DESC.unidentifiedDisplayName, DESC.unidentifiedResourceName, DESC.identifiedDisplayName, DESC.identifiedResourceName, DESC.slotCount, DESC.ClassNum)\n" +
            "		if not result == true then\n" +
            "			return false, msg\n" +
            "		end\n" +
            "		for k, v in pairs(DESC.unidentifiedDescriptionName) do\n" +
            "			result, msg = AddItemUnidentifiedDesc(ItemID, v)\n" +
            "			if not result == true then\n" +
            "				return false, msg\n" +
            "			end\n" +
            "		end\n" +
            "		for k, v in pairs(DESC.identifiedDescriptionName) do\n" +
            "			result, msg = AddItemIdentifiedDesc(ItemID, v)\n" +
            "			if not result == true then\n" +
            "				return false, msg\n" +
            "			end\n" +
            "		end\n" +
            "		if nil ~= DESC.EffectID then\n" +
            "			result, msg = AddItemEffectInfo(ItemID, DESC.EffectID)\n" +
            "			if not result == true then\n" +
            "				return false, msg\n" +
            "			end\n" +
            "		end\n" +
            "		if nil ~= DESC.costume then\n" +
            "			result, msg = AddItemIsCostume(ItemID, DESC.costume)\n" +
            "			if not result == true then\n" +
            "				return false, msg\n" +
            "			end\n" +
            "		end\n" +
            "		k = DESC.unidentifiedResourceName\n" +
            "		v = DESC.identifiedDisplayName\n" +
            "	end\n" +
            "	return true, \"good\"\n" +
            "end\n" +
            "\n" +
            "-- Function #1\n" +
            "main_server = function()\n" +
            "	for ItemID, DESC in pairs(tbl) do\n" +
            "		result, msg = AddItem(ItemID, DESC.identifiedDisplayName, DESC.slotCount)\n" +
            "		if not result == true then\n" +
            "			return false, msg\n" +
            "		end\n" +
            "	end\n" +
            "	return true, \"good\"\n" +
            "end\n";
        #endregion

        string finalTexts = prefix + builder.ToString() + postfix;
        finalTexts = finalTexts.Replace("[NEW_LINE]", "\",\n			\"");
        finalTexts = finalTexts.Replace("กับ 11)", "ประเภท)");
        finalTexts = finalTexts.Replace("กับ 11 )", "ประเภท)");
        finalTexts = finalTexts.Replace("กับ II_VIEW)", "ประเภท)");
        finalTexts = finalTexts.Replace("กับ II_VIEW )", "ประเภท)");
        finalTexts = finalTexts.Replace("กับ ITEMINFO_VIEW)", "ประเภท)");
        finalTexts = finalTexts.Replace("กับ ITEMINFO_VIEW )", "ประเภท)");
        finalTexts = finalTexts.Replace("     ๐", "๐");
        finalTexts = finalTexts.Replace("    ๐", "๐");
        finalTexts = finalTexts.Replace("   ๐", "๐");
        finalTexts = finalTexts.Replace("  ๐", "๐");
        finalTexts = finalTexts.Replace(" ๐", "๐");

        File.WriteAllText("itemInfo_Sak.lub", finalTexts, Encoding.UTF8);

        Debug.Log(DateTime.UtcNow);

        txtConvertProgression.text = "Done!! File name 'itemInfo_Sak.lub'";
    }

    string ConvertItemBonus(string text)
    {
        // Comment fix
        int commentFixRetry = 300;
        while (text.Contains("/*"))
        {
            var copier = text;
            if (!copier.Contains("*/"))
                text = copier.Substring(0, copier.IndexOf("/*"));
            else
                text = copier.Substring(0, copier.IndexOf("/*")) + copier.Substring(copier.IndexOf("*/") + 2);

            commentFixRetry--;

            if (commentFixRetry <= 0)
                break;
        }

        commentFixRetry = 30;
        while (text.Contains("*/"))
        {
            text = text.Replace("*/", string.Empty);

            commentFixRetry--;

            if (commentFixRetry <= 0)
                break;
        }

        // Wrong wording fix
        text = text.Replace("bPAtk", "bPatk");
        text = text.Replace("Ele_dark", "Ele_Dark");
        text = text.Replace("bonus2 bIgnoreMDefRaceRate", "bonus2 bIgnoreMdefRaceRate");
        text = text.Replace("bVariableCastRate", "bVariableCastrate");
        text = text.Replace("bMaxHPRate", "bMaxHPrate");
        text = text.Replace("bMaxSPRate", "bMaxSPrate");
        text = text.Replace("Baselevel", "BaseLevel");
        // End wrong wording fix

        // Comma fix
        int commaFixRetry = 300;
        while (text.Contains("(") && text.Contains(")") && commaFixRetry > 0)
        {
            commaFixRetry--;

            int commaOpenCount = 0;
            int commaCloseCount = 0;
            int currentCommaFixerStartIndex = text.IndexOf('(');

            //Debug.Log("text#1:" + text);

            for (int i = currentCommaFixerStartIndex; i < text.Length; i++)
            {
                if (text[i] == '(')
                    commaOpenCount++;
                else if (text[i] == ')')
                {
                    commaCloseCount++;

                    if (commaCloseCount == commaOpenCount)
                    {
                        var testCommaFixer = text.Substring(currentCommaFixerStartIndex, i - currentCommaFixerStartIndex + 1);
                        var testCommaFixer2 = text.Substring(currentCommaFixerStartIndex, i - currentCommaFixerStartIndex + 1);
                        testCommaFixer2 = testCommaFixer2.Replace('(', '†');
                        testCommaFixer2 = testCommaFixer2.Replace(')', '‡');
                        testCommaFixer2 = testCommaFixer2.Replace(",", " กับ ");
                        text = text.Replace(testCommaFixer, testCommaFixer2);

                        break;
                    }
                }
            }

            //Debug.Log("text#2:" + text);
        }

        text = text.Replace('†', '(');
        text = text.Replace('‡', ')');

        //Debug.Log("text#3:" + text);

        // End comma fix

        text = text.Replace("      ", string.Empty);

        // autobonus3
        if (text.Contains("autobonus3 \"{"))
        {
            var temp = text.Replace("autobonus3 \"{", string.Empty);
            string duplicate = string.Empty;
            if (temp.IndexOf("}\"") > 0)
            {
                duplicate = temp.Substring(temp.IndexOf("}\""));
                temp = temp.Substring(0, temp.IndexOf("}\""));
            }
            var duplicates = duplicate.Split(',');
            var bonuses = string.Empty;
            int retry = 30;
            while (temp.Contains("bonus"))
            {
                var sumBonus = temp.Substring(0, temp.IndexOf(';'));
                bonuses += ConvertItemBonus(sumBonus);
                if (temp.Length > temp.IndexOf(';') + 1)
                    temp = temp.Substring(temp.IndexOf(';') + 1);
                else
                    temp = string.Empty;
                retry--;
                if (retry <= 0)
                    break;
            }
            var skillName = RemoveQuote(duplicates.Length >= 4 ? duplicates[3] : string.Empty);
            if (!string.IsNullOrEmpty(bonuses) || !string.IsNullOrWhiteSpace(bonuses))
            {
                bonuses = bonuses.Replace("๐", "[NEW_LINE]๐");
                text = string.Format("เมื่อใช้ {1} มีโอกาสเล็กน้อย ที่จะ {0} ชั่วคราว", bonuses, GetSkillName(skillName));
            }
            else
                text = string.Empty;
        }
        // autobonus2
        if (text.Contains("autobonus2 \"{"))
        {
            var temp = text.Replace("autobonus2 \"{", string.Empty);
            if (temp.IndexOf("}\"") > 0)
                temp = temp.Substring(0, temp.IndexOf("}\""));
            var bonuses = string.Empty;
            int retry = 30;
            while (temp.Contains("bonus"))
            {
                var sumBonus = temp.Substring(0, temp.IndexOf(';'));
                bonuses += ConvertItemBonus(sumBonus);
                if (temp.Length > temp.IndexOf(';') + 1)
                    temp = temp.Substring(temp.IndexOf(';') + 1);
                else
                    temp = string.Empty;
                retry--;
                if (retry <= 0)
                    break;
            }
            if (!string.IsNullOrEmpty(bonuses) || !string.IsNullOrWhiteSpace(bonuses))
            {
                bonuses = bonuses.Replace("๐", "[NEW_LINE]๐");
                text = string.Format("เมื่อโดนตีกายภาพ มีโอกาสเล็กน้อย ที่จะ {0} ชั่วคราว", bonuses);
            }
            else
                text = string.Empty;
        }
        // autobonus
        if (text.Contains("autobonus \"{"))
        {
            var temp = text.Replace("autobonus \"{", string.Empty);
            if (temp.IndexOf("}\"") > 0)
                temp = temp.Substring(0, temp.IndexOf("}\""));
            var bonuses = string.Empty;
            int retry = 30;
            while (temp.Contains("bonus"))
            {
                var sumBonus = temp.Substring(0, temp.IndexOf(';'));
                bonuses += ConvertItemBonus(sumBonus);
                if (temp.Length > temp.IndexOf(';') + 1)
                    temp = temp.Substring(temp.IndexOf(';') + 1);
                else
                    temp = string.Empty;
                retry--;
                if (retry <= 0)
                    break;
            }
            if (!string.IsNullOrEmpty(bonuses) || !string.IsNullOrWhiteSpace(bonuses))
            {
                bonuses = bonuses.Replace("๐", "[NEW_LINE]๐");
                text = string.Format("เมื่อตีกายภาพ มีโอกาสเล็กน้อย ที่จะ {0} ชั่วคราว", bonuses);
            }
            else
                text = string.Empty;
        }

        text = text.Replace(";", string.Empty);

        text = text.Replace("UnEquipScript: |", "^666478[เมื่อถอด]^000000");
        text = text.Replace("EquipScript: |", "^666478[เมื่อสวมใส่]^000000");

        text = text.Replace("bonus bStr,", "๐ Str +");
        text = text.Replace("bonus bAgi,", "๐ Agi +");
        text = text.Replace("bonus bVit,", "๐ Vit +");
        text = text.Replace("bonus bInt,", "๐ Int +");
        text = text.Replace("bonus bDex,", "๐ Dex +");
        text = text.Replace("bonus bLuk,", "๐ Luk +");
        text = text.Replace("bonus bAllStats,", "๐ All Status +");
        text = text.Replace("bonus bAgiVit,", "๐ Agi, Vit +");
        text = text.Replace("bonus bAgiDexStr,", "๐ Agi, Dex, Str +");

        text = text.Replace("bonus bPow,", "๐ Pow +");
        text = text.Replace("bonus bSta,", "๐ Sta +");
        text = text.Replace("bonus bWis,", "๐ Wis +");
        text = text.Replace("bonus bSpl,", "๐ Spl +");
        text = text.Replace("bonus bCon,", "๐ Con +");
        text = text.Replace("bonus bCrt,", "๐ Crt +");
        text = text.Replace("bonus bAllTraitStats,", "๐ All Trait +");

        text = text.Replace("bonus bMaxHP,", "๐ MaxHP +");
        if (text.Contains("bonus bMaxHPrate,"))
        {
            var temp = text.Replace("bonus bMaxHPrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ MaxHP +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bMaxSP,", "๐ MaxSP +");
        if (text.Contains("bonus bMaxSPrate,"))
        {
            var temp = text.Replace("bonus bMaxSPrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ MaxSP +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bMaxAP,", "๐ MaxAP +");
        if (text.Contains("bonus bMaxAPrate,"))
        {
            var temp = text.Replace("bonus bMaxAPrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ MaxAP +{0}%", TryParseInt(temps[0]));
        }

        text = text.Replace("bonus bBaseAtk,", "๐ ฐาน Atk +");
        text = text.Replace("bonus bAtk,", "๐ Atk +");
        text = text.Replace("bonus bAtk2,", "๐ Atk +");
        if (text.Contains("bonus bAtkRate,"))
        {
            var temp = text.Replace("bonus bAtkRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Atk +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bWeaponAtkRate,"))
        {
            var temp = text.Replace("bonus bWeaponAtkRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Atk อาวุธ +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bMatk,", "๐ MAtk +");
        if (text.Contains("bonus bMatkRate,"))
        {
            var temp = text.Replace("bonus bMatkRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ MAtk +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bWeaponMatkRate,"))
        {
            var temp = text.Replace("bonus bWeaponMatkRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ MAtk อาวุธ +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bDef,", "๐ Def +");
        if (text.Contains("bonus bDefRate,"))
        {
            var temp = text.Replace("bonus bDefRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Def +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bDef2,", "๐ ฐาน Def +");
        if (text.Contains("bonus bDef2Rate,"))
        {
            var temp = text.Replace("bonus bDef2Rate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ฐาน Def +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bMdef,", "๐ MDef +");
        if (text.Contains("bonus bMdefRate,"))
        {
            var temp = text.Replace("bonus bMdefRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ MDef +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bMdef2,", "๐ ฐาน MDef +");
        if (text.Contains("bonus bMdef2Rate,"))
        {
            var temp = text.Replace("bonus bMdef2Rate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ฐาน MDef +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bHit,", "๐ Hit +");
        if (text.Contains("bonus bHitRate,"))
        {
            var temp = text.Replace("bonus bHitRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Hit +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bCritical,", "๐ Critical +");
        text = text.Replace("bonus bCriticalLong,", "๐ Critical ตีไกล +");
        if (text.Contains("bonus2 bCriticalAddRace,"))
        {
            var temp = text.Replace("bonus2 bCriticalAddRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Critical กับ {0} +{1}", ParseRace(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bCriticalRate,"))
        {
            var temp = text.Replace("bonus bCriticalRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Critical +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bFlee,", "๐ Flee +");
        if (text.Contains("bonus bFleeRate,"))
        {
            var temp = text.Replace("bonus bFleeRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Flee +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bFlee2,", "๐ Perfect Dodge +");
        if (text.Contains("bonus bFlee2Rate,"))
        {
            var temp = text.Replace("bonus bFlee2Rate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Perfect Dodge +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bPerfectHitRate,"))
        {
            var temp = text.Replace("bonus bPerfectHitRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Perfect Hit +{0}% (ทับไม่ได้)", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bPerfectHitAddRate,"))
        {
            var temp = text.Replace("bonus bPerfectHitAddRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Perfect Hit +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bSpeedRate,"))
        {
            var temp = text.Replace("bonus bSpeedRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เดินเร็ว +{0}% (ทับไม่ได้)", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bSpeedAddRate,"))
        {
            var temp = text.Replace("bonus bSpeedAddRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เดินเร็ว +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bAspd,", "๐ ASPD +");
        if (text.Contains("bonus bAspdRate,"))
        {
            var temp = text.Replace("bonus bAspdRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ASPD +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bAtkRange,", "๐ ระยะตี +");
        if (text.Contains("bonus bAddMaxWeight,"))
        {
            var temp = text.Replace("bonus bAddMaxWeight,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ แบกของได้หนักขึ้น +{0}", TryParseInt(temps[0], 10));
        }

        text = text.Replace("bonus bPatk,", "๐ P.Atk +");
        if (text.Contains("bonus bPAtkRate,"))
        {
            var temp = text.Replace("bonus bPAtkRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ P.Atk +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bSmatk,", "๐ S.MAtk +");
        if (text.Contains("bonus bSMatkRate,"))
        {
            var temp = text.Replace("bonus bSMatkRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ S.MAtk +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bRes,", "๐ Res +");
        if (text.Contains("bonus bResRate,"))
        {
            var temp = text.Replace("bonus bResRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Res +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bMres,", "๐ M.Res +");
        if (text.Contains("bonus bMResRate,"))
        {
            var temp = text.Replace("bonus bMResRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ M.Res +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bHplus,", "๐ H.Plus +");
        if (text.Contains("bonus bHPlusRate,"))
        {
            var temp = text.Replace("bonus bHPlusRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ H.Plus +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bCrate,", "๐ C.Rate +");
        if (text.Contains("bonus bCRateRate,"))
        {
            var temp = text.Replace("bonus bCRateRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ C.Rate +{0}%", TryParseInt(temps[0]));
        }

        if (text.Contains("bonus bHPrecovRate,"))
        {
            var temp = text.Replace("bonus bHPrecovRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ การฟื้นฟู HP ปกติ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bSPrecovRate,"))
        {
            var temp = text.Replace("bonus bSPrecovRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ การฟื้นฟู SP ปกติ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus2 bHPRegenRate,"))
        {
            var temp = text.Replace("bonus2 bHPRegenRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ฟื้นฟู HP +{0} ทุก ๆ {1} วินาที", TryParseInt(temps[0]), TryParseInt(temps[1], 1000));
        }
        if (text.Contains("bonus2 bHPLossRate,"))
        {
            var temp = text.Replace("bonus2 bHPLossRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เสีย HP +{0} ทุก ๆ {1} วินาที", TryParseInt(temps[0]), TryParseInt(temps[1], 1000));
        }
        if (text.Contains("bonus2 bSPRegenRate,"))
        {
            var temp = text.Replace("bonus2 bSPRegenRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ฟื้นฟู SP +{0} ทุก ๆ {1} วินาที", TryParseInt(temps[0]), TryParseInt(temps[1], 1000));
        }
        if (text.Contains("bonus2 bSPLossRate,"))
        {
            var temp = text.Replace("bonus2 bSPLossRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เสีย SP +{0} ทุก ๆ {1} วินาที", TryParseInt(temps[0]), TryParseInt(temps[1], 1000));
        }
        if (text.Contains("bonus2 bRegenPercentHP,"))
        {
            var temp = text.Replace("bonus2 bRegenPercentHP,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ฟื้นฟู HP +{0}% ทุก ๆ {1} วินาที", TryParseInt(temps[0]), TryParseInt(temps[1], 1000));
        }
        if (text.Contains("bonus2 bRegenPercentSP,"))
        {
            var temp = text.Replace("bonus2 bRegenPercentSP,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ฟื้นฟู SP +{0}% ทุก ๆ {1} วินาที", TryParseInt(temps[0]), TryParseInt(temps[1], 1000));
        }
        text = text.Replace("bonus bNoRegen,1", "๐ หยุดการฟื้นฟู HP ปกติ");
        text = text.Replace("bonus bNoRegen,2", "๐ หยุดการฟื้นฟู SP ปกติ");
        if (text.Contains("bonus bUseSPrate,"))
        {
            var temp = text.Replace("bonus bUseSPrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ SP ที่ต้องใช้ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus2 bSkillUseSP,"))
        {
            var temp = text.Replace("bonus2 bSkillUseSP,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ SP ที่ต้องใช้กับ {0} +{1}", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bSkillUseSPrate,"))
        {
            var temp = text.Replace("bonus2 bSkillUseSPrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ SP ที่ต้องใช้กับ {0} +{1}%", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bSkillAtk,"))
        {
            var temp = text.Replace("bonus2 bSkillAtk,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ {0} แรงขึ้น +{1}%", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bShortAtkRate,"))
        {
            var temp = text.Replace("bonus bShortAtkRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีกายภาพใกล้ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bLongAtkRate,"))
        {
            var temp = text.Replace("bonus bLongAtkRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีกายภาพไกล +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bCritAtkRate,"))
        {
            var temp = text.Replace("bonus bCritAtkRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Critical แรงขึ้น +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bCritDefRate,"))
        {
            var temp = text.Replace("bonus bCritDefRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กัน Critical +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bCriticalDef,"))
        {
            var temp = text.Replace("bonus bCriticalDef,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ หลบ Critical +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus2 bWeaponAtk,"))
        {
            var temp = text.Replace("bonus2 bWeaponAtk,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ถ้าสวมใส่ {0} Atk อาวุธ +{1}", RemoveQuote(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bWeaponDamageRate,"))
        {
            var temp = text.Replace("bonus2 bWeaponDamageRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ถ้าสวมใส่ {0} ตีกายภาพ +{1}%", RemoveQuote(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bNearAtkDef,"))
        {
            var temp = text.Replace("bonus bNearAtkDef,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันกายภาพใกล้ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bLongAtkDef,"))
        {
            var temp = text.Replace("bonus bLongAtkDef,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันกายภาพไกล +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bMagicAtkDef,"))
        {
            var temp = text.Replace("bonus bMagicAtkDef,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันเวทย์ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bMiscAtkDef,"))
        {
            var temp = text.Replace("bonus bMiscAtkDef,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันอื่น ๆ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bNoWeaponDamage,"))
        {
            var temp = text.Replace("bonus bNoWeaponDamage,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันกายภาพ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bNoMagicDamage,"))
        {
            var temp = text.Replace("bonus bNoMagicDamage,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันเวทย์ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bNoMiscDamage,"))
        {
            var temp = text.Replace("bonus bNoMiscDamage,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันอื่น ๆ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bHealPower,"))
        {
            var temp = text.Replace("bonus bHealPower,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Heal แรงขึ้น +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bHealPower2,"))
        {
            var temp = text.Replace("bonus bHealPower2,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ โดน Heal แรงขึ้น +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus2 bSkillHeal,"))
        {
            var temp = text.Replace("bonus2 bSkillHeal,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ {0} Heal แรงขึ้น +{1}%", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bSkillHeal2,"))
        {
            var temp = text.Replace("bonus2 bSkillHeal2,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ โดน {0} Heal แรงขึ้น +{1}%", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bAddItemHealRate,"))
        {
            var temp = text.Replace("bonus bAddItemHealRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Item Heal HP แรงขึ้น +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus2 bAddItemHealRate,"))
        {
            var temp = text.Replace("bonus2 bAddItemHealRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ {0} Heal HP แรงขึ้น +{1}%", GetItemName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bAddItemGroupHealRate,"))
        {
            var temp = text.Replace("bonus2 bAddItemGroupHealRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Item กลุ่ม {0} Heal HP แรงขึ้น +{1}%", RemoveQuote(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bAddItemSPHealRate,"))
        {
            var temp = text.Replace("bonus bAddItemSPHealRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Item Heal SP แรงขึ้น +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus2 bAddItemSPHealRate,"))
        {
            var temp = text.Replace("bonus2 bAddItemSPHealRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ {0} Heal SP แรงขึ้น +{1}%", GetItemName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bAddItemGroupSPHealRate,"))
        {
            var temp = text.Replace("bonus2 bAddItemGroupSPHealRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Item กลุ่ม {0} Heal SP แรงขึ้น +{1}%", RemoveQuote(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bCastrate,"))
        {
            var temp = text.Replace("bonus bCastrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ร่าย V. +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus2 bCastrate,"))
        {
            var temp = text.Replace("bonus2 bCastrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ร่าย V. {0} +{1}%", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bFixedCastrate,"))
        {
            var temp = text.Replace("bonus bFixedCastrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ร่าย F. +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus2 bFixedCastrate,"))
        {
            var temp = text.Replace("bonus2 bFixedCastrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ร่าย F. {0} +{1}%", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bVariableCastrate,"))
        {
            var temp = text.Replace("bonus bVariableCastrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ร่าย V. +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus2 bVariableCastrate,"))
        {
            var temp = text.Replace("bonus2 bVariableCastrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ร่าย V. {0} +{1}%", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bFixedCast,"))
        {
            var temp = text.Replace("bonus bFixedCast,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ร่าย F. +{0} วินาที", TryParseInt(temps[0], 1000));
        }
        if (text.Contains("bonus2 bSkillFixedCast,"))
        {
            var temp = text.Replace("bonus2 bSkillFixedCast,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ร่าย F. {0} +{1} วินาที", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1], 1000));
        }
        if (text.Contains("bonus bVariableCast,"))
        {
            var temp = text.Replace("bonus bVariableCast,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ร่าย V. +{0} วินาที", TryParseInt(temps[0], 1000));
        }
        if (text.Contains("bonus2 bSkillVariableCast,"))
        {
            var temp = text.Replace("bonus2 bSkillVariableCast,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ร่าย V. {0} +{1} วินาที", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1], 1000));
        }
        text = text.Replace("bonus bNoCastCancel2", "๐ การร่ายไม่ถูกหยุด");
        text = text.Replace("bonus bNoCastCancel", "๐ การร่ายไม่ถูกหยุด (ใช้ไม่ได้ใน GvG)");
        if (text.Contains("bonus bDelayrate,"))
        {
            var temp = text.Replace("bonus bDelayrate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Delay หลังร่าย +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bDelayRate,"))
        {
            var temp = text.Replace("bonus bDelayRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Delay หลังร่าย +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus2 bSkillDelay,"))
        {
            var temp = text.Replace("bonus2 bSkillDelay,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Delay หลังร่าย {0} +{1} วินาที", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1], 1000));
        }
        if (text.Contains("bonus2 bSkillCooldown,"))
        {
            var temp = text.Replace("bonus2 bSkillCooldown,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Cooldown {0} +{1} วินาที", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1], 1000));
        }
        if (text.Contains("bonus2 bAddEle,"))
        {
            var temp = text.Replace("bonus2 bAddEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีกายภาพ {0} +{1}%", ParseElement(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus3 bAddEle,"))
        {
            var temp = text.Replace("bonus3 bAddEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีกายภาพ {0} โดย {2} +{1}%", ParseElement(temps[0]), TryParseInt(temps[1]), ParseAtf(temps[2]));
        }
        if (text.Contains("bonus2 bMagicAddEle,"))
        {
            var temp = text.Replace("bonus2 bMagicAddEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีเวทย์ {0} +{1}%", ParseElement(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bSubEle,"))
        {
            var temp = text.Replace("bonus2 bSubEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันกายภาพ {0} +{1}%", ParseElement(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus3 bSubEle,"))
        {
            var temp = text.Replace("bonus3 bSubEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันกายภาพ {0} โดย {2} +{1}%", ParseElement(temps[0]), TryParseInt(temps[1]), ParseAtf(temps[2]));
        }
        if (text.Contains("bonus2 bSubDefEle,"))
        {
            var temp = text.Replace("bonus2 bSubDefEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันกายภาพ {0} +{1}%", ParseElement(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bMagicSubDefEle,"))
        {
            var temp = text.Replace("bonus2 bMagicSubDefEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันเวทย์ {0} +{1}%", ParseElement(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bAddRace,"))
        {
            var temp = text.Replace("bonus2 bAddRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีกายภาพ {0} +{1}%", ParseRace(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bMagicAddRace,"))
        {
            var temp = text.Replace("bonus2 bMagicAddRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีเวทย์ {0} +{1}%", ParseRace(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bSubRace,"))
        {
            var temp = text.Replace("bonus2 bSubRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กัน {0} +{1}%", ParseRace(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus3 bSubRace,"))
        {
            var temp = text.Replace("bonus3 bSubRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กัน {0} โดย {2} +{1}%", ParseRace(temps[0]), TryParseInt(temps[1]), ParseAtf(temps[2]));
        }
        if (text.Contains("bonus2 bAddClass,"))
        {
            var temp = text.Replace("bonus2 bAddClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีกายภาพ {0} +{1}%", ParseClass(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bMagicAddClass,"))
        {
            var temp = text.Replace("bonus2 bMagicAddClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีเวทย์ {0} +{1}%", ParseClass(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bSubClass,"))
        {
            var temp = text.Replace("bonus2 bSubClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กัน {0} +{1}%", ParseClass(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bAddSize,"))
        {
            var temp = text.Replace("bonus2 bAddSize,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีกายภาพ {0} +{1}%", ParseSize(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bMagicAddSize,"))
        {
            var temp = text.Replace("bonus2 bMagicAddSize,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีเวทย์ {0} +{1}%", ParseSize(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bSubSize,"))
        {
            var temp = text.Replace("bonus2 bSubSize,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันกายภาพ {0} +{1}%", ParseSize(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bWeaponSubSize,"))
        {
            var temp = text.Replace("bonus2 bWeaponSubSize,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันกายภาพ {0} +{1}%", ParseSize(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bMagicSubSize,"))
        {
            var temp = text.Replace("bonus2 bMagicSubSize,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันเวทย์ {0} +{1}%", ParseSize(temps[0]), TryParseInt(temps[1]));
        }
        text = text.Replace("bonus bNoSizeFix", "ไม่สนใจขนาดในการคำนวณ");
        if (text.Contains("bonus2 bAddDamageClass,"))
        {
            var temp = text.Replace("bonus2 bAddDamageClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีกายภาพกับ {0} +{1}%", TryParseInt(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bAddMagicDamageClass,"))
        {
            var temp = text.Replace("bonus2 bAddMagicDamageClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีเวทย์กับ {0} +{1}%", TryParseInt(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bAddDefMonster,"))
        {
            var temp = text.Replace("bonus2 bAddDefMonster,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันกายภาพจาก {0} +{1}%", GetMonsterNameFromId(TryParseInt(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bAddMDefMonster,"))
        {
            var temp = text.Replace("bonus2 bAddMDefMonster,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันเวทย์จาก {0} +{1}%", GetMonsterNameFromId(TryParseInt(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bAddRace2,"))
        {
            var temp = text.Replace("bonus2 bAddRace2,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีกายภาพ {0} +{1}%", ParseRace2(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bSubRace2,"))
        {
            var temp = text.Replace("bonus2 bSubRace2,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กัน {0} +{1}%", ParseRace2(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bMagicAddRace2,"))
        {
            var temp = text.Replace("bonus2 bMagicAddRace2,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีเวทย์ {0} +{1}%", ParseRace2(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bSubSkill,"))
        {
            var temp = text.Replace("bonus2 bSubSkill,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กัน {0} +{1}%", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bAbsorbDmgMaxHP,"))
        {
            var temp = text.Replace("bonus bAbsorbDmgMaxHP,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ หากโดนตี แรงกว่าเลือดมากสุด จะโดนแค่ {0}% จาก MaxHP (ทับไม่ได้)", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bAtkEle,"))
        {
            var temp = text.Replace("bonus bAtkEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เคลือบอาวุธธาตุ {0}", ParseElement(temps[0]));
        }
        if (text.Contains("bonus bDefEle,"))
        {
            var temp = text.Replace("bonus bDefEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เคลือบชุดเกราะธาตุ {0}", ParseElement(temps[0]));
        }
        if (text.Contains("bonus2 bMagicAtkEle,"))
        {
            var temp = text.Replace("bonus2 bMagicAtkEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีเวทย์ {0} +{1}%", ParseElement(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bDefRatioAtkRace,"))
        {
            var temp = text.Replace("bonus bDefRatioAtkRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีแรงขึ้นตาม DEF {0}", ParseRace(temps[0]));
        }
        if (text.Contains("bonus bDefRatioAtkEle,"))
        {
            var temp = text.Replace("bonus bDefRatioAtkEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีแรงขึ้นตาม DEF {0}", ParseElement(temps[0]));
        }
        if (text.Contains("bonus bDefRatioAtkClass,"))
        {
            var temp = text.Replace("bonus bDefRatioAtkClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ตีแรงขึ้นตาม DEF {0}", ParseClass(temps[0]));
        }
        if (text.Contains("bonus4 bSetDefRace,"))
        {
            var temp = text.Replace("bonus4 bSetDefRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาส {1}% ลด DEF เหลือ {3} กับ {0} {2} วินาที", ParseRace(temps[0]), TryParseInt(temps[1]), TryParseInt(temps[2], 1000), TryParseInt(temps[3]));
        }
        if (text.Contains("bonus4 bSetMDefRace,"))
        {
            var temp = text.Replace("bonus4 bSetMDefRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาส {1}% ลด MDEF เหลือ {3} กับ {0} {2} วินาที", ParseRace(temps[0]), TryParseInt(temps[1]), TryParseInt(temps[2], 1000), TryParseInt(temps[3]));
        }
        if (text.Contains("bonus bIgnoreDefEle,"))
        {
            var temp = text.Replace("bonus bIgnoreDefEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ไม่สนใจ DEF {0}", ParseElement(temps[0]));
        }
        if (text.Contains("bonus bIgnoreDefRace,"))
        {
            var temp = text.Replace("bonus bIgnoreDefRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ไม่สนใจ DEF {0}", ParseRace(temps[0]));
        }
        if (text.Contains("bonus bIgnoreDefClass,"))
        {
            var temp = text.Replace("bonus bIgnoreDefClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ไม่สนใจ DEF {0}", ParseClass(temps[0]));
        }
        if (text.Contains("bonus bIgnoreMDefRace,"))
        {
            var temp = text.Replace("bonus bIgnoreMDefRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ไม่สนใจ MDEF {0}", ParseRace(temps[0]));
        }
        if (text.Contains("bonus2 bIgnoreDefRaceRate,"))
        {
            var temp = text.Replace("bonus2 bIgnoreDefRaceRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ไม่สนใจ DEF {1}% {0}", ParseRace(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bIgnoreMdefRaceRate,"))
        {
            var temp = text.Replace("bonus2 bIgnoreMdefRaceRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ไม่สนใจ MDEF {1}% {0}", ParseRace(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bIgnoreMdefRace2Rate,"))
        {
            var temp = text.Replace("bonus2 bIgnoreMdefRace2Rate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ไม่สนใจ MDEF {1}% {0}", ParseRace2(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bIgnoreMDefEle,"))
        {
            var temp = text.Replace("bonus bIgnoreMDefEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ไม่สนใจ MDEF {0}", ParseElement(temps[0]));
        }
        if (text.Contains("bonus2 bIgnoreDefClassRate,"))
        {
            var temp = text.Replace("bonus2 bIgnoreDefClassRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ไม่สนใจ DEF {1}% {0}", ParseClass(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bIgnoreMdefClassRate,"))
        {
            var temp = text.Replace("bonus2 bIgnoreMdefClassRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ไม่สนใจ MDEF {1}% {0}", ParseClass(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bExpAddRace,"))
        {
            var temp = text.Replace("bonus2 bExpAddRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ EXP +{1}% {0}", ParseRace(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bExpAddClass,"))
        {
            var temp = text.Replace("bonus2 bExpAddClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ EXP +{1}% {0}", ParseClass(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bAddEff,"))
        {
            var temp = text.Replace("bonus2 bAddEff,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพมีโอกาส {1}% ที่จะเกิด {0} กับเป้าหมาย", ParseEffect(temps[0]), TryParseInt(temps[1], 100));
        }
        if (text.Contains("bonus2 bAddEff2,"))
        {
            var temp = text.Replace("bonus2 bAddEff2,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพมีโอกาส {1}% ที่จะเกิด {0} กับตนเอง", ParseEffect(temps[0]), TryParseInt(temps[1], 100));
        }
        if (text.Contains("bonus2 bAddEffWhenHit,"))
        {
            var temp = text.Replace("bonus2 bAddEffWhenHit,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อโดนตีกายภาพมีโอกาส {1}% ที่จะเกิด {0} กับเป้าหมาย", ParseEffect(temps[0]), TryParseInt(temps[1], 100));
        }
        if (text.Contains("bonus2 bResEff,"))
        {
            var temp = text.Replace("bonus2 bResEff,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาส {1}% ที่จะกัน {0}", ParseEffect(temps[0]), TryParseInt(temps[1], 100));
        }
        if (text.Contains("bonus3 bAddEff,"))
        {
            var temp = text.Replace("bonus3 bAddEff,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีโดย {2} มีโอกาส {1}% ที่จะเกิด {0} กับเป้าหมาย", ParseEffect(temps[0]), TryParseInt(temps[1], 100), ParseAtf(temps[2]));
        }
        if (text.Contains("bonus4 bAddEff,"))
        {
            var temp = text.Replace("bonus4 bAddEff,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีโดย {2} มีโอกาส {1}% ที่จะเกิด {0} กับเป้าหมาย {3} วินาที", ParseEffect(temps[0]), TryParseInt(temps[1], 100), ParseAtf(temps[2]), TryParseInt(temps[3], 1000));
        }
        if (text.Contains("bonus3 bAddEffWhenHit,"))
        {
            var temp = text.Replace("bonus3 bAddEffWhenHit,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อโดนตีโดย {2} มีโอกาส {1}% ที่จะเกิด {0} กับเป้าหมาย", ParseEffect(temps[0]), TryParseInt(temps[1], 100), ParseAtf(temps[2]));
        }
        if (text.Contains("bonus4 bAddEffWhenHit,"))
        {
            var temp = text.Replace("bonus4 bAddEff,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อโดนตีโดย {2} มีโอกาส {1}% ที่จะเกิด {0} กับเป้าหมาย {3} วินาที", ParseEffect(temps[0]), TryParseInt(temps[1], 100), ParseAtf(temps[2]), TryParseInt(temps[3], 1000));
        }
        if (text.Contains("bonus3 bAddEffOnSkill,"))
        {
            var temp = text.Replace("bonus3 bAddEffOnSkill,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อร่าย {0} มีโอกาส {2}% ที่จะเกิด {1} กับเป้าหมาย", GetSkillName(RemoveQuote(temps[0])), ParseEffect(temps[1]), TryParseInt(temps[2], 100));
        }
        if (text.Contains("bonus4 bAddEffOnSkill,"))
        {
            var temp = text.Replace("bonus4 bAddEffOnSkill,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อร่าย {0} กับ {3} มีโอกาส {2}% ที่จะเกิด {1} กับเป้าหมาย", GetSkillName(RemoveQuote(temps[0])), ParseEffect(temps[1]), TryParseInt(temps[2], 100), ParseAtf(temps[3]));
        }
        if (text.Contains("bonus5 bAddEffOnSkill,"))
        {
            var temp = text.Replace("bonus5 bAddEffOnSkill,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อร่าย {0} กับ {3} มีโอกาส {2}% ที่จะเกิด {1} กับเป้าหมาย {4} วินาที", GetSkillName(RemoveQuote(temps[0])), ParseEffect(temps[1]), TryParseInt(temps[2], 100), ParseAtf(temps[3]), TryParseInt(temps[4], 1000));
        }
        if (text.Contains("bonus2 bComaClass,"))
        {
            var temp = text.Replace("bonus2 bComaClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตี {0} มีโอกาส {1}% ที่จะเกิด Coma", ParseClass(temps[0]), TryParseInt(temps[1], 100));
        }
        if (text.Contains("bonus2 bComaRace,"))
        {
            var temp = text.Replace("bonus2 bComaRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตี {0} มีโอกาส {1}% ที่จะเกิด Coma", ParseRace(temps[0]), TryParseInt(temps[1], 100));
        }
        if (text.Contains("bonus2 bWeaponComaEle,"))
        {
            var temp = text.Replace("bonus2 bWeaponComaEle,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพ {0} มีโอกาส {1}% ที่จะเกิด Coma", ParseElement(temps[0]), TryParseInt(temps[1], 100));
        }
        if (text.Contains("bonus2 bWeaponComaClass,"))
        {
            var temp = text.Replace("bonus2 bWeaponComaClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพ {0} มีโอกาส {1}% ที่จะเกิด Coma", ParseClass(temps[0]), TryParseInt(temps[1], 100));
        }
        if (text.Contains("bonus2 bWeaponComaRace,"))
        {
            var temp = text.Replace("bonus2 bWeaponComaRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพ {0} มีโอกาส {1}% ที่จะเกิด Coma", ParseRace(temps[0]), TryParseInt(temps[1], 100));
        }
        if (text.Contains("bonus3 bAutoSpell,"))
        {
            var temp = text.Replace("bonus3 bAutoSpell,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพมีโอกาส {2}% ร่าย Lv.{1} {0}", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]), TryParseInt(temps[2], 10));
        }
        if (text.Contains("bonus3 bAutoSpellWhenHit,"))
        {
            var temp = text.Replace("bonus3 bAutoSpellWhenHit,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อโดนตีกายภาพมีโอกาส {2}% ร่าย Lv.{1} {0}", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]), TryParseInt(temps[2], 10));
        }
        if (text.Contains("bonus4 bAutoSpell,"))
        {
            var temp = text.Replace("bonus4 bAutoSpell,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพมีโอกาส {2}% ร่าย Lv.{1} {0} ใส่ {3}", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]), TryParseInt(temps[2], 10), ParseI(temps[3]));
        }
        if (text.Contains("bonus5 bAutoSpell,"))
        {
            var temp = text.Replace("bonus5 bAutoSpell,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพโดย {3} มีโอกาส {2}% ร่าย Lv.{1} {0} ใส่ {4}", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]), TryParseInt(temps[2], 10), ParseAtf(temps[3]), ParseI(temps[4]));
        }
        if (text.Contains("bonus4 bAutoSpellWhenHit,"))
        {
            var temp = text.Replace("bonus4 bAutoSpellWhenHit,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อโดนตีกายภาพมีโอกาส {2}% ร่าย Lv.{1} {0} ใส่ {3}", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]), TryParseInt(temps[2], 10), ParseI(temps[3]));
        }
        if (text.Contains("bonus5 bAutoSpellWhenHit,"))
        {
            var temp = text.Replace("bonus5 bAutoSpellWhenHit,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อโดนตีกายภาพโดย {3} มีโอกาส {2}% ร่าย Lv.{1} {0} ใส่ {4}", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]), TryParseInt(temps[2], 10), ParseAtf(temps[3]), ParseI(temps[4]));
        }
        if (text.Contains("bonus4 bAutoSpellOnSkill,"))
        {
            var temp = text.Replace("bonus4 bAutoSpellOnSkill,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อร่าย {0} มีโอกาส {3}% ร่าย Lv.{2} {1}", GetSkillName(RemoveQuote(temps[0])), GetSkillName(RemoveQuote(temps[1])), TryParseInt(temps[2]), TryParseInt(temps[3], 10));
        }
        if (text.Contains("bonus5 bAutoSpellOnSkill,"))
        {
            var temp = text.Replace("bonus5 bAutoSpellOnSkill,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อร่าย {0} มีโอกาส {3}% ร่าย Lv.{2} {1} ใส่ {4}", GetSkillName(RemoveQuote(temps[0])), GetSkillName(RemoveQuote(temps[1])), TryParseInt(temps[2]), TryParseInt(temps[3], 10), ParseI(temps[4]));
        }
        text = text.Replace("bonus bHPDrainValue,", "๐ เมื่อตีกายภาพ HP +");
        if (text.Contains("bonus2 bHPDrainValueRace,"))
        {
            var temp = text.Replace("bonus2 bHPDrainValueRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพ {0} HP +{1}", ParseRace(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bHpDrainValueClass,"))
        {
            var temp = text.Replace("bonus2 bHpDrainValueClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพ {0} HP +{1}", ParseClass(temps[0]), TryParseInt(temps[1]));
        }
        text = text.Replace("bonus bSPDrainValue,", "๐ เมื่อตีกายภาพ SP +");
        if (text.Contains("bonus2 bSPDrainValueRace,"))
        {
            var temp = text.Replace("bonus2 bSPDrainValueRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพ {0} SP +{1}", ParseRace(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bSpDrainValueClass,"))
        {
            var temp = text.Replace("bonus2 bSpDrainValueClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพ {0} SP +{1}", ParseClass(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bHPDrainRate,"))
        {
            var temp = text.Replace("bonus2 bHPDrainRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกาพภาพมีโอกาส {0}% HP +{1}%", TryParseInt(temps[0], 10), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bSPDrainRate,"))
        {
            var temp = text.Replace("bonus2 bSPDrainRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกาพภาพมีโอกาส {0}% SP +{1}%", TryParseInt(temps[0], 10), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bHPVanishRate,"))
        {
            var temp = text.Replace("bonus2 bHPVanishRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพมีโอกาส {0}% ลด HP ศัตรู {1}%", TryParseInt(temps[0], 10), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus3 bHPVanishRaceRate,"))
        {
            var temp = text.Replace("bonus3 bHPVanishRaceRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพเผ่า {0} มีโอกาส {1}% ลด HP ศัตรู {2}%", ParseRace(temps[0]), TryParseInt(temps[1], 10), TryParseInt(temps[2]));
        }
        if (text.Contains("bonus3 bHPVanishRate,"))
        {
            var temp = text.Replace("bonus3 bHPVanishRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีโดย {2} มีโอกาส {0}% ลด HP ศัตรู {1}%", TryParseInt(temps[0], 10), TryParseInt(temps[1]), ParseAtf(temps[2]));
        }
        if (text.Contains("bonus2 bSPVanishRate,"))
        {
            var temp = text.Replace("bonus2 bSPVanishRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพมีโอกาส {0}% ลด SP ศัตรู {1}%", TryParseInt(temps[0], 10), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus3 bSPVanishRaceRate,"))
        {
            var temp = text.Replace("bonus3 bSPVanishRaceRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพ {0} มีโอกาส {1}% ลด SP ศัตรู {2}%", ParseRace(temps[0]), TryParseInt(temps[1], 10), TryParseInt(temps[2]));
        }
        if (text.Contains("bonus3 bSPVanishRate,"))
        {
            var temp = text.Replace("bonus3 bSPVanishRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีโดย {2} มีโอกาส {0}% ลด SP ศัตรู {1}%", TryParseInt(temps[0], 10), TryParseInt(temps[1]), ParseAtf(temps[2]));
        }
        if (text.Contains("bonus3 bStateNoRecoverRace,"))
        {
            var temp = text.Replace("bonus3 bStateNoRecoverRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีกายภาพ {0} มีโอกาส {1}% ที่จะหยุดการฟื้นฟูทุกอย่าง กับเป้าหมาย {2} วินาที", ParseRace(temps[0]), TryParseInt(temps[1], 100), TryParseInt(temps[2], 1000));
        }
        text = text.Replace("bonus bHPGainValue,", "๐ ฆ่าด้วยตีกายภาพใกล้ HP +");
        text = text.Replace("bonus bSPGainValue,", "๐ ฆ่าด้วยตีกายภาพใกล้ SP +");
        if (text.Contains("bonus2 bSPGainRace,"))
        {
            var temp = text.Replace("bonus2 bSPGainRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ฆ่า {0} ด้วยตีกายภาพใกล้ HP +{1}", ParseRace(temps[0]), TryParseInt(temps[1]));
        }
        text = text.Replace("bonus bLongHPGainValue,", "๐ ฆ่าด้วยตีกายภาพไกล HP +");
        text = text.Replace("bonus bLongSPGainValue,", "๐ ฆ่าด้วยตีกายภาพไกล SP +");
        text = text.Replace("bonus bMagicHPGainValue,", "๐ ฆ่าด้วยตีเวทย์ HP +");
        text = text.Replace("bonus bMagicSPGainValue,", "๐ ฆ่าด้วยตีเวทย์ SP +");
        if (text.Contains("bonus bShortWeaponDamageReturn,"))
        {
            var temp = text.Replace("bonus bShortWeaponDamageReturn,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ สะท้อนกายภาพใกล้ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bLongWeaponDamageReturn,"))
        {
            var temp = text.Replace("bonus bLongWeaponDamageReturn,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ สะท้อนกายภาพไกล +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bMagicDamageReturn,"))
        {
            var temp = text.Replace("bonus bMagicDamageReturn,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ สะท้อนเวทย์ +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bReduceDamageReturn,"))
        {
            var temp = text.Replace("bonus bReduceDamageReturn,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันการสะท้อน +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bUnstripableWeapon", "๐ กันปลดอาวุธ");
        text = text.Replace("bonus bUnstripableArmor", "๐ กันปลดชุดเกราะ");
        text = text.Replace("bonus bUnstripableHelm", "๐ กันปลดหมวก");
        text = text.Replace("bonus bUnstripableShield", "๐ กันปลดโล่");
        text = text.Replace("bonus bUnstripable", "๐ กันปลดทุกอย่าง");
        text = text.Replace("bonus bUnbreakableGarment", "๐ ผ้าคลุมจะไม่พัง");
        text = text.Replace("bonus bUnbreakableWeapon", "๐ อาวุธจะไม่พัง");
        text = text.Replace("bonus bUnbreakableArmor", "๐ ชุดเกราะจะไม่พัง");
        text = text.Replace("bonus bUnbreakableHelm", "๐ หมวกจะไม่พัง");
        text = text.Replace("bonus bUnbreakableShield", "๐ โล่จะไม่พัง");
        text = text.Replace("bonus bUnbreakableShoes", "๐ รองเท้าจะไม่พัง");
        if (text.Contains("bonus bUnbreakable,"))
        {
            var temp = text.Replace("bonus bUnbreakable,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ กันอุปกรณ์สวมใส่ ทุกชนิดพัง +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bBreakWeaponRate,"))
        {
            var temp = text.Replace("bonus bBreakWeaponRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีมีโอกาส {0}% ที่จะพังอาวุธเป้าหมาย", TryParseInt(temps[0], 100));
        }
        if (text.Contains("bonus bBreakArmorRate,"))
        {
            var temp = text.Replace("bonus bBreakArmorRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อตีมีโอกาส {0}% ที่จะพังชุดเกราะเป้าหมาย", TryParseInt(temps[0], 100));
        }
        if (text.Contains("bonus2 bDropAddRace,"))
        {
            var temp = text.Replace("bonus2 bDropAddRace,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Drop +{1}% {0}", ParseRace(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bDropAddClass,"))
        {
            var temp = text.Replace("bonus2 bDropAddClass,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ Drop +{1}% {0}", ParseClass(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus3 bAddMonsterIdDropItem,"))
        {
            var temp = text.Replace("bonus3 bAddMonsterIdDropItem,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อกำจัด {1} มีโอกาส {2}% ที่จะ Drop {0}", GetItemName(RemoveQuote(temps[0])), TryParseInt(temps[1]), TryParseInt(temps[2], 100));
        }
        if (text.Contains("bonus2 bAddMonsterDropItem,"))
        {
            var temp = text.Replace("bonus2 bAddMonsterDropItem,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาส {1}% ที่จะ Drop {0}", GetItemName(RemoveQuote(temps[0])), TryParseInt(temps[1], 100));
        }
        if (text.Contains("bonus3 bAddMonsterDropItem,"))
        {
            var temp = text.Replace("bonus3 bAddMonsterDropItem,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาส {2}% ที่จะ Drop {0} {1}", GetItemName(RemoveQuote(temps[0])), ParseRace(temps[1]), TryParseInt(temps[2], 100));
        }
        if (text.Contains("bonus3 bAddClassDropItem,"))
        {
            var temp = text.Replace("bonus3 bAddClassDropItem,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาส {2}% ที่จะ Drop {0} {1}", GetItemName(RemoveQuote(temps[0])), ParseClass(temps[1]), TryParseInt(temps[2], 100));
        }
        if (text.Contains("bonus2 bAddMonsterDropItemGroup,"))
        {
            var temp = text.Replace("bonus2 bAddMonsterDropItemGroup,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาส {1}% ที่จะ Drop Item กลุ่ม {0}", RemoveQuote(temps[0]), TryParseInt(temps[1], 100));
        }
        if (text.Contains("bonus3 bAddMonsterDropItemGroup,"))
        {
            var temp = text.Replace("bonus3 bAddMonsterDropItemGroup,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาส {2}% ที่จะ Drop Item กลุ่ม {0} {1}", RemoveQuote(temps[0]), ParseRace(temps[1]), TryParseInt(temps[2], 100));
        }
        if (text.Contains("bonus3 bAddClassDropItemGroup,"))
        {
            var temp = text.Replace("bonus3 bAddClassDropItemGroup,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาส {2}% ที่จะ Drop Item กลุ่ม {0} {1}", RemoveQuote(temps[0]), ParseClass(temps[1]), TryParseInt(temps[2], 100));
        }
        if (text.Contains("bonus2 bGetZenyNum,"))
        {
            var temp = text.Replace("bonus2 bGetZenyNum,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อกำจัด Monster มีโอกาส {1}% ที่จะได้รับ 1~{0} Zeny (ทับไม่ได้)", TryParseInt(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus2 bAddGetZenyNum,"))
        {
            var temp = text.Replace("bonus2 bAddGetZenyNum,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อกำจัด Monster มีโอกาส {1}% ที่จะได้รับ 1~{0} Zeny", TryParseInt(temps[0]), TryParseInt(temps[1]));
        }
        if (text.Contains("bonus bDoubleRate,"))
        {
            var temp = text.Replace("bonus bDoubleRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาส ตีกายภาพสองครั้ง +{0}% (ทับไม่ได้)", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bDoubleAddRate,"))
        {
            var temp = text.Replace("bonus bDoubleAddRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาส ตีกายภาพสองครั้ง +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bSplashRange,", "๐ ตีกระจาย (ทับไม่ได้) +");
        text = text.Replace("bonus bSplashAddRange,", "๐ ตีกระจาย +");
        if (text.Contains("bonus2 bAddSkillBlow,"))
        {
            var temp = text.Replace("bonus2 bAddSkillBlow,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เมื่อใช้ {0} จะพลักเป้าหมาย {1} ช่อง", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        text = text.Replace("bonus bNoKnockback", "๐ กันการพลัก");
        text = text.Replace("bonus bNoGemStone", "๐ ไม่ต้องใช้ Gemstone ในการร่าย");
        text = text.Replace("bonus bIntravision", "๐ มองเห็นการหายตัว");
        text = text.Replace("bonus bPerfectHide", "๐ หายตัวโดยที่ MvP มองไม่เห็น");
        text = text.Replace("bonus bRestartFullRecover", "๐ HP และ SP เต็ม เมื่อพ้นจากการหมดสติ");
        if (text.Contains("bonus bClassChange,"))
        {
            var temp = text.Replace("bonus bClassChange,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ มีโอกาสเปลี่ยนแปลง รูปแบบเป้าหมาย +{0}%", TryParseInt(temps[0]));
        }
        if (text.Contains("bonus bAddStealRate,"))
        {
            var temp = text.Replace("bonus bAddStealRate,", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ เพิ่มโอกาสขโมยของ +{0}%", TryParseInt(temps[0]));
        }
        text = text.Replace("bonus bNoMadoFuel", "๐ ไม่ต้องใช้ Mado Fuel ในการร่าย");
        text = text.Replace("bonus bNoWalkDelay", "๐ เมื่อโดนตีจะไม่ชะงัก");
        text = text.Replace("specialeffect2", "๐ แสดง Effect");
        text = text.Replace("specialeffect", "๐ แสดง Effect");
        // Unit Skill Use Id
        if (text.Contains("unitskilluseid "))
        {
            var temp = text.Replace("unitskilluseid ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ สามารถใช้ Lv.{2} {1}", RemoveQuote(temps[0]), GetSkillName(RemoveQuote(temps[1])), TryParseInt(temps[2]));
        }
        // Item Skill
        if (text.Contains("itemskill "))
        {
            var temp = text.Replace("itemskill ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ สามารถใช้ Lv.{1} {0}", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        // Skill
        if (text.Contains("skill "))
        {
            var temp = text.Replace("skill ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ สามารถใช้ Lv.{1} {0}", GetSkillName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        // itemheal
        if (text.Contains("percentheal "))
        {
            var temp = text.Replace("percentheal ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ฟื้นฟู HP {0}% และ SP {1}%", TryParseInt(temps[0]), TryParseInt(temps[1]));
        }
        // itemheal
        if (text.Contains("itemheal "))
        {
            var temp = text.Replace("itemheal ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ฟื้นฟู {0} HP และ {1} SP", TryParseInt(temps[0]), TryParseInt(temps[1]));
        }
        // heal
        if (((!string.IsNullOrEmpty(text) && (text[0] == 'h')) || text.Contains(" h")) && text.Contains("heal "))
        {
            var temp = text.Replace("heal ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ฟื้นฟู {0} HP และ {1} SP", TryParseInt(temps[0]), TryParseInt(temps[1]));
        }
        // sc_start4
        if (text.Contains("sc_start4 "))
        {
            var temp = text.Replace("sc_start4 ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ รับผล {0} เป็นเวลา {1} วินาที", RemoveQuote(temps[0]), TryParseInt(temps[1], 1000));
        }
        // sc_start2
        if (text.Contains("sc_start2 "))
        {
            var temp = text.Replace("sc_start2 ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ รับผล {0} เป็นเวลา {1} วินาที", RemoveQuote(temps[0]), TryParseInt(temps[1], 1000));
        }
        // sc_start
        if (text.Contains("sc_start "))
        {
            var temp = text.Replace("sc_start ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ รับผล {0} เป็นเวลา {1} วินาที", RemoveQuote(temps[0]), TryParseInt(temps[1], 1000));
        }
        // sc_end
        if (text.Contains("sc_end "))
        {
            var temp = text.Replace("sc_end ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ ลบผล {0}", RemoveQuote(temps[0]));
        }
        // active_transform
        if (text.Contains("active_transform "))
        {
            var temp = text.Replace("active_transform ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ แปลงร่างเป็น {0} เป็นเวลา {1} วินาที", RemoveQuote(temps[0]), TryParseInt(temps[1], 1000));
        }
        // getitem
        if (text.Contains("getitem "))
        {
            var temp = text.Replace("getitem ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ รับ {0} {1} ชิ้น", GetItemName(RemoveQuote(temps[0])), TryParseInt(temps[1]));
        }
        // pet
        if (text.Contains("pet "))
        {
            var temp = text.Replace("pet ", string.Empty);
            var temps = temp.Split(',');
            text = string.Format("๐ สำหรับจับ {0}", GetMonsterNameFromId(RemoveQuote(temps[0])));
        }
        text = text.Replace("sc_end_class", "๐ ลบ Buff ทุกอย่าง");
        text = text.Replace("setmounting()", "๐ ขึ้น/ลง พาหนะ");

        // All in one parse...
        text = AllInOneParse(text);

        // Negative value
        text = text.Replace("+-", "-");

        // Whitespace
        text = text.Replace("    ", " ");
        text = text.Replace("   ", " ");
        text = text.Replace("  ", " ");
        text = text.Replace("\\", string.Empty);

        return text;
    }

    string RemoveSpace(string text)
    {
        while (text.Contains(" "))
            text = text.Replace(" ", string.Empty);
        return text;
    }

    string RemoveQuote(string text)
    {
        while (text.Contains("\""))
            text = text.Replace("\"", string.Empty);
        return text;
    }

    string ParseRace(string text)
    {
        text = text.Replace("RC_Angel", "^AC6523(Angel)^000000");
        text = text.Replace("RC_Brute", "^AC6523(Brute)^000000");
        text = text.Replace("RC_DemiHuman", "^AC6523(Demi-Human)^000000");
        text = text.Replace("RC_Demon", "^AC6523(Demon)^000000");
        text = text.Replace("RC_Dragon", "^AC6523(Dragon)^000000");
        text = text.Replace("RC_Fish", "^AC6523(Fish)^000000");
        text = text.Replace("RC_Formless", "^AC6523(Formless)^000000");
        text = text.Replace("RC_Insect", "^AC6523(Insect)^000000");
        text = text.Replace("RC_Plant", "^AC6523(Plant)^000000");
        text = text.Replace("RC_Player_Human", "^AC6523(Human)^000000");
        text = text.Replace("RC_Player_Doram", "^AC6523(Doram)^000000");
        text = text.Replace("RC_Undead", "^AC6523(Undead)^000000");
        text = text.Replace("RC_All", "^AC6523(ทุกเผ่า)^000000");
        return text;
    }

    string ParseRace2(string text)
    {
        text = text.Replace("RC2_Goblin", "^AC6523(Goblin)^000000");
        text = text.Replace("RC2_Kobold", "^AC6523(Kobold)^000000");
        text = text.Replace("RC2_Orc", "^AC6523(Orc)^000000");
        text = text.Replace("RC2_Golem", "^AC6523(Golem)^000000");
        text = text.Replace("RC2_Guardian", "^AC6523(Guardian)^000000");
        text = text.Replace("RC2_Ninja", "^AC6523(Ninja)^000000");
        text = text.Replace("RC2_BioLab", "^AC6523(Biolab)^000000");
        text = text.Replace("RC2_SCARABA", "^AC6523(Scaraba)^000000");
        text = text.Replace("RC2_FACEWORM", "^AC6523(Faceworm)^000000");
        text = text.Replace("RC2_THANATOS", "^AC6523(Thanatos)^000000");
        text = text.Replace("RC2_CLOCKTOWER", "^AC6523(Clocktower)^000000");
        text = text.Replace("RC2_ROCKRIDGE", "^AC6523(Rockridge)^000000");
        return text;
    }

    string ParseClass(string text)
    {
        text = text.Replace("Class_Normal", "^0040B6(Normal)^000000");
        text = text.Replace("Class_Boss", "^0040B6(Boss)^000000");
        text = text.Replace("Class_Guardian", "^0040B6(Guardian)^000000");
        text = text.Replace("Class_All", "^0040B6(ทุกประเภท)^000000");
        return text;
    }

    string ParseSize(string text)
    {
        text = text.Replace("Size_Small", "^FF26F5(ขนาดเล็ก)^000000");
        text = text.Replace("Size_Medium", "^FF26F5(ขนาดกลาง)^000000");
        text = text.Replace("Size_Large", "^FF26F5(ขนาดใหญ่)^000000");
        text = text.Replace("Size_All", "^FF26F5(ทุกขนาด)^000000");
        return text;
    }

    string ParseElement(string text)
    {
        text = text.Replace("Ele_Dark", "^C426FF(Dark)^000000");
        text = text.Replace("Ele_Earth", "^C426FF(Earth)^000000");
        text = text.Replace("Ele_Fire", "^C426FF(Fire)^000000");
        text = text.Replace("Ele_Ghost", "^C426FF(Ghost)^000000");
        text = text.Replace("Ele_Holy", "^C426FF(Holy)^000000");
        text = text.Replace("Ele_Neutral", "^C426FF(Neutral)^000000");
        text = text.Replace("Ele_Poison", "^C426FF(Poison)^000000");
        text = text.Replace("Ele_Undead", "^C426FF(Undead)^000000");
        text = text.Replace("Ele_Water", "^C426FF(Water)^000000");
        text = text.Replace("Ele_Wind", "^C426FF(Wind)^000000");
        text = text.Replace("Ele_All", "^C426FF(ทุกธาตุ)^000000");
        return text;
    }

    string ParseEffect(string text)
    {
        text = text.Replace("Eff_Bleeding", "^EC1B3ABleeding^000000");
        text = text.Replace("Eff_Blind", "^EC1B3ABlind^000000");
        text = text.Replace("Eff_Burning", "^EC1B3ABurning^000000");
        text = text.Replace("Eff_Confusion", "^EC1B3AConfusion^000000");
        text = text.Replace("Eff_Crystalize", "^EC1B3ACrystalize^000000");
        text = text.Replace("Eff_Curse", "^EC1B3ACurse^000000");
        text = text.Replace("Eff_DPoison", "^EC1B3ADeadly Poison^000000");
        text = text.Replace("Eff_Fear", "^EC1B3AFear^000000");
        text = text.Replace("Eff_Freeze", "^EC1B3AFreeze^000000");
        text = text.Replace("Eff_Freezing", "^EC1B3AFreezing^000000");
        text = text.Replace("Eff_Poison", "^EC1B3APoison^000000");
        text = text.Replace("Eff_Silence", "^EC1B3ASilence^000000");
        text = text.Replace("Eff_Sleep", "^EC1B3ASleep^000000");
        text = text.Replace("Eff_Stone", "^EC1B3AStone^000000");
        text = text.Replace("Eff_Stun", "^EC1B3AStun^000000");
        return text;
    }

    string ParseAtf(string text)
    {
        text = text.Replace("ATF_SELF", "ตนเอง");
        text = text.Replace("ATF_TARGET", "เป้าหมาย");
        text = text.Replace("ATF_SHORT", "ตีกายภาพ ใกล้");
        text = text.Replace("BF_SHORT", "ตีกายภาพ ใกล้");
        text = text.Replace("ATF_LONG", "ตีกายภาพ ไกล");
        text = text.Replace("BF_LONG", "ตีกายภาพ ไกล");
        text = text.Replace("ATF_SKILL", "ใช้ Skill");
        text = text.Replace("ATF_WEAPON", "ตี");
        text = text.Replace("BF_WEAPON", "ตี");
        text = text.Replace("ATF_MAGIC", "ใช้ Skill");
        text = text.Replace("BF_MAGIC", "ใช้ Skill");
        text = text.Replace("BF_SKILL", "ใช้ Skill");
        text = text.Replace("ATF_MISC", "ใช้ Skill อื่น ๆ");
        text = text.Replace("BF_MISC", "ใช้ Skill อื่น ๆ");
        text = text.Replace("BF_NORMAL", "ตีกายภาพ");
        return text;
    }

    string ParseI(string text)
    {
        text = text.Replace("0", "ตนเอง");
        text = text.Replace("1", "เป้าหมาย");
        text = text.Replace("2", "สุ่ม Lv. Skill");
        text = text.Replace("3", "สุ่ม Lv. Skill ใส่เป้าหมาย");
        return text;
    }

    string ParseEQI(string text)
    {
        text = text.Replace("EQI_COMPOUND_ON", "อุปกรณ์ที่สวมใส่อยู่");
        text = text.Replace("EQI_ACC_L", "ประดับข้างซ้าย");
        text = text.Replace("EQI_ACC_R", "ประดับข้างขวา");
        text = text.Replace("EQI_SHOES", "รองเท้า");
        text = text.Replace("EQI_GARMENT", "ผ้าคลุม");
        text = text.Replace("EQI_HEAD_LOW", "หมวกส่วนล่าง");
        text = text.Replace("EQI_HEAD_MID", "หมวกส่วนกลาง");
        text = text.Replace("EQI_HEAD_TOP", "หมวกส่วนบน");
        text = text.Replace("EQI_ARMOR", "ชุดเกราะ");
        text = text.Replace("EQI_HAND_L", "มือซ้าย");
        text = text.Replace("EQI_HAND_R", "มือขวา");
        text = text.Replace("EQI_COSTUME_HEAD_TOP", "หมวกส่วนบน Costume");
        text = text.Replace("EQI_COSTUME_HEAD_MID", "หมวกส่วนกลาง Costume");
        text = text.Replace("EQI_COSTUME_HEAD_LOW", "หมวกส่วนล่าง Costume");
        text = text.Replace("EQI_COSTUME_GARMENT", "ผ้าคลุม Costume");
        text = text.Replace("EQI_AMMO", "กระสุน");
        text = text.Replace("EQI_SHADOW_ARMOR", "ชุดเกราะ Shadow");
        text = text.Replace("EQI_SHADOW_WEAPON", "อาวุธ Shadow");
        text = text.Replace("EQI_SHADOW_SHIELD", "โล่ Shadow");
        text = text.Replace("EQI_SHADOW_SHOES", "รองเท้า Shadow");
        text = text.Replace("EQI_SHADOW_ACC_R", "ประดับ Shadow ข้างขวา");
        text = text.Replace("EQI_SHADOW_ACC_L", "ประดับ Shadow ข้างซ้าย");
        return text;
    }

    string ParseWeaponType(string text)
    {
        text = text.Replace("W_FIST", "มือเปล่า");
        text = text.Replace("W_DAGGER", "Dagger");
        text = text.Replace("W_1HSWORD", "One-handed Sword");
        text = text.Replace("W_2HSWORD", "Two-handed Sword");
        text = text.Replace("W_1HSPEAR", "One-handed Spear");
        text = text.Replace("W_2HSPEAR", "Two-handed Spear");
        text = text.Replace("W_1HAXE", "One-handed Axe");
        text = text.Replace("W_2HAXE", "Two-handed Axe");
        text = text.Replace("W_MACE", "Mace");
        text = text.Replace("W_2HMACE", "Two-handed Mace");
        text = text.Replace("W_STAFF", "Staff");
        text = text.Replace("W_BOW", "Bow");
        text = text.Replace("W_KNUCKLE", "Knuckle");
        text = text.Replace("W_MUSICAL", "Musical");
        text = text.Replace("W_WHIP", "Whip");
        text = text.Replace("W_BOOK", "Book");
        text = text.Replace("W_KATAR", "Katar");
        text = text.Replace("W_REVOLVER", "Revolver");
        text = text.Replace("W_RIFLE", "Rifle");
        text = text.Replace("W_GATLING", "Gatling");
        text = text.Replace("W_SHOTGUN", "Shotgun");
        text = text.Replace("W_GRENADE", "Grenade Launcher");
        text = text.Replace("W_HUUMA", "Huuma");
        text = text.Replace("W_2HSTAFF", "Two-handed Staff");
        text = text.Replace("W_DOUBLE_DD,", "2 Daggers");
        text = text.Replace("W_DOUBLE_SS,", "2 Swords");
        text = text.Replace("W_DOUBLE_AA,", "2 Axes");
        text = text.Replace("W_DOUBLE_DS,", "Dagger + Sword");
        text = text.Replace("W_DOUBLE_DA,", "Dagger + Axe");
        text = text.Replace("W_DOUBLE_SA,", "Sword + Axe");
        text = text.Replace("W_SHIELD", "โล่");
        return text;
    }

    string AllInOneParse(string text)
    {
        text = text.Replace("else if (", "^FF2525หากไม่ผ่านเงื่อนไข^000000(");
        text = text.Replace("else if(", "^FF2525หากไม่ผ่านเงื่อนไข^000000(");
        text = text.Replace("if (", "^FF2525ถ้า^000000(");
        text = text.Replace("else (", "^FF2525หากไม่ผ่านเงื่อนไข^000000(");
        text = text.Replace("if(", "^FF2525ถ้า^000000(");
        text = text.Replace("else(", "^FF2525หากไม่ผ่านเงื่อนไข^000000(");
        text = text.Replace("||", "หรือ");
        text = text.Replace("&&", "และ");
        text = text.Replace("&", " คือ ");
        text = text.Replace("BaseLevel", "Level");
        text = text.Replace("BaseJob", "ฐานอาชีพ");
        text = text.Replace("BaseClass", "ฐานอาชีพ");
        text = text.Replace("JobLevel", "Job Level");
        text = text.Replace("readparam", "ค่า");
        text = text.Replace("bSTR", "STR");
        text = text.Replace("bStr", "STR");
        text = text.Replace("bAGI", "AGI");
        text = text.Replace("bAgi", "AGI");
        text = text.Replace("bVIT", "VIT");
        text = text.Replace("bVit", "VIT");
        text = text.Replace("bINT", "INT");
        text = text.Replace("bInt", "INT");
        text = text.Replace("bDEX", "DEX");
        text = text.Replace("bDex", "DEX");
        text = text.Replace("bLUK", "LUK");
        text = text.Replace("bLuk", "LUK");
        text = text.Replace("eaclass()", "Class");
        text = text.Replace("EAJL_2_1", "คลาส 2-1");
        text = text.Replace("EAJL_2_2", "คลาส 2-2");
        text = text.Replace("EAJL_2", "คลาส 2");
        text = text.Replace("EAJL_UPPER", "ไฮคลาส");
        text = text.Replace("EAJL_BABY", "คลาส Baby");
        text = text.Replace("EAJL_THIRD", "คลาส 3");
        text = text.Replace("EAJ_BASEMASK", "คลาส 1");
        text = text.Replace("EAJ_UPPERMASK", "ไฮคลาส");
        text = text.Replace("EAJ_THIRDMASK", "คลาส 3");
        text = text.Replace("PETINFO_ID", "Pet");
        text = text.Replace("PETINFO_INTIMATE", "ความสนิท Pet");
        text = text.Replace("PET_INTIMATE_LOYAL", "สนิทสนม");
        text = text.Replace("getpetinfo(", "(");
        text = text.Replace("getiteminfo(", "(");
        text = text.Replace("getequipid(", " Item ");
        text = text.Replace("getequiprefinerycnt", "ตีบวก");
        text = text.Replace("getenchantgrade()", "เกรด");
        text = text.Replace("getenchantgrade", "เกรด");
        text = text.Replace("getrefine()", "ตีบวก");
        text = text.Replace("getequipweaponlv()", "Lv. อาวุธ");
        text = text.Replace("ismounting()", "หากขี่หาหนะ");
        text = text.Replace("getskilllv", "Lv. Skill");
        text = text.Replace("pow (", "ยกกำลัง(");
        text = text.Replace("pow(", "ยกกำลัง(");
        text = text.Replace("min (", "ใช้ค่าน้อยสุด(");
        text = text.Replace("min(", "ใช้ค่าน้อยสุด(");
        text = text.Replace("max (", "ใช้ค่ามากสุด(");
        text = text.Replace("max(", "ใช้ค่ามากสุด(");
        text = text.Replace("rand (", "สุ่ม(");
        text = text.Replace("rand(", "สุ่ม(");
        text = text.Replace("==", "คือ");
        text = text.Replace("!=", " ไม่ใช่ ");
        text = text.Replace("JOB_", string.Empty);
        text = text.Replace("Job_", string.Empty);
        text = text.Replace("job_", string.Empty);
        text = text.Replace("EAJ_", string.Empty);
        text = text.Replace("eaj_", string.Empty);
        text = text.Replace("SC_", string.Empty);
        text = text.Replace("sc_", string.Empty);
        text = text.Replace("else", "^FF2525หากไม่ผ่านเงื่อนไข^000000");
        text = text.Replace(" ? ", " เป็น ");
        text = text.Replace("?", " เป็น ");
        text = text.Replace(" : ", " ถ้าไม่ใช่ ");
        text = text.Replace(":", " ถ้าไม่ใช่ ");

        text = ParseWeaponType(text);
        text = ParseEQI(text);
        bool isHadQuote = text.Contains("\"");
        text = RemoveQuote(text);
        if (isHadQuote)
            text = ParseSkillName(text);

        return text;
    }

    string TryParseInt(string text, float divider = 1)
    {
        if (text == "INFINITE_TICK")
            return "ถาวร";

        float sum = -1;
        if (float.TryParse(text, out sum))
            sum = float.Parse(text);
        else if (divider == 1)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
                return "0";
            else
                return text;
        }
        else
            return text + " หาร " + divider.ToString("f0");
        if ((sum / divider) == 0)
            return (sum / divider).ToString("f0");
        else if ((sum / divider) > -0.1f && (sum / divider) < 0.1f)
            return (sum / divider).ToString("f2");
        else if (((sum / divider) % 1) != 0)
            return (sum / divider).ToString("f1");
        else
            return (sum / divider).ToString("f0");
    }

    string GetCombo(string aegis_name)
    {
        StringBuilder builder = new StringBuilder();

        // Loop all combo data
        for (int i = 0; i < comboDatas.Count; i++)
        {
            var currentComboData = comboDatas[i];

            // Found
            if (currentComboData.IsAegisNameContain(aegis_name))
            {
                StringBuilder sum = new StringBuilder();

                bool isFoundNow = false;

                for (int j = 0; j < currentComboData.sameComboDatas.Count; j++)
                {
                    var currentSameComboData = currentComboData.sameComboDatas[j];

                    // Add item name
                    for (int k = 0; k < currentSameComboData.aegis_names.Count; k++)
                    {
                        var currentAegisName = currentSameComboData.aegis_names[k];

                        if (currentAegisName == aegis_name)
                            isFoundNow = true;
                    }

                    if (isFoundNow)
                    {
                        // Declare header
                        var same_set_name_list = "			\"^666478[ถ้าใส่คู่ ";

                        // Add item name
                        for (int k = 0; k < currentSameComboData.aegis_names.Count; k++)
                        {
                            var currentAegisName = currentSameComboData.aegis_names[k];

                            // Should not add base item name
                            if (currentAegisName == aegis_name)
                                continue;
                            else
                                same_set_name_list += GetItemName(currentAegisName, true) + ", ";
                        }

                        // Remove leftover ,
                        same_set_name_list = same_set_name_list.Substring(0, same_set_name_list.Length - 2);

                        // End
                        same_set_name_list += "]^000000\",\n";

                        sum.Append(same_set_name_list);

                        break;
                    }
                }

                // Add combo bonus description
                for (int j = 0; j < currentComboData.descriptions.Count; j++)
                {
                    if (j >= currentComboData.descriptions.Count - 1)
                        sum.Append(currentComboData.descriptions[j]);
                    else
                        sum.Append(currentComboData.descriptions[j] + "\n");
                }

                // End
                sum.Append("			\"^58990F[สิ้นสุด Combo]^000000\",\n");

                // Finalize this combo data
                builder.Append(sum);
            }
        }

        return builder.ToString();
    }

    string GetItemName(string text, bool isForceAegisName = false)
    {
        int _int = 0;
        if (!isForceAegisName && int.TryParse(text, out _int))
        {
            _int = int.Parse(text);
            foreach (var item in idNameDatas)
            {
                if (item.id == _int)
                    return item._name;
            }
        }
        else
        {
            text = RemoveSpace(text);
            foreach (var item in idNameDatas)
            {
                if (item.aegisName.ToLower() == text.ToLower())
                    return item._name;
            }
        }

        return text;
    }

    string GetResourceNameFromId(int id, string type, string subType, string location)
    {
        if (isRandomizeResourceNameCustomItemOnly)
        {
            if (isRandomizeResourceName && id >= ItemGenerator.startId)
            {
                if (subType.ToLower().Contains("dagger"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameDagger[UnityEngine.Random.Range(0, resNameDagger.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameDagger[UnityEngine.Random.Range(0, resNameDagger.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("1hsword"))
                {
                    var s = GetResourceNameFromId(int.Parse(resName1hSword[UnityEngine.Random.Range(0, resName1hSword.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resName1hSword[UnityEngine.Random.Range(0, resName1hSword.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("2hsword"))
                {
                    var s = GetResourceNameFromId(int.Parse(resName2hSword[UnityEngine.Random.Range(0, resName2hSword.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resName2hSword[UnityEngine.Random.Range(0, resName2hSword.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("1hspear"))
                {
                    var s = GetResourceNameFromId(int.Parse(resName1hSpear[UnityEngine.Random.Range(0, resName1hSpear.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resName1hSpear[UnityEngine.Random.Range(0, resName1hSpear.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("2hspear"))
                {
                    var s = GetResourceNameFromId(int.Parse(resName2hSpear[UnityEngine.Random.Range(0, resName2hSpear.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resName2hSpear[UnityEngine.Random.Range(0, resName2hSpear.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("1haxe"))
                {
                    var s = GetResourceNameFromId(int.Parse(resName1hAxe[UnityEngine.Random.Range(0, resName1hAxe.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resName1hAxe[UnityEngine.Random.Range(0, resName1hAxe.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("2haxe"))
                {
                    var s = GetResourceNameFromId(int.Parse(resName2hAxe[UnityEngine.Random.Range(0, resName2hAxe.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resName2hAxe[UnityEngine.Random.Range(0, resName2hAxe.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("mace"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameMace[UnityEngine.Random.Range(0, resNameMace.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameMace[UnityEngine.Random.Range(0, resNameMace.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("staff"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameStaff[UnityEngine.Random.Range(0, resNameStaff.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameStaff[UnityEngine.Random.Range(0, resNameStaff.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("bow"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameBow[UnityEngine.Random.Range(0, resNameBow.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameBow[UnityEngine.Random.Range(0, resNameBow.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("knuckle"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameKnuckle[UnityEngine.Random.Range(0, resNameKnuckle.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameKnuckle[UnityEngine.Random.Range(0, resNameKnuckle.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("musical"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameMusical[UnityEngine.Random.Range(0, resNameMusical.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameMusical[UnityEngine.Random.Range(0, resNameMusical.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("whip"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameWhip[UnityEngine.Random.Range(0, resNameWhip.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameWhip[UnityEngine.Random.Range(0, resNameWhip.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("book"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameBook[UnityEngine.Random.Range(0, resNameBook.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameBook[UnityEngine.Random.Range(0, resNameBook.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("katar"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameKatar[UnityEngine.Random.Range(0, resNameKatar.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameKatar[UnityEngine.Random.Range(0, resNameKatar.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("revolver"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameRevolver[UnityEngine.Random.Range(0, resNameRevolver.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameRevolver[UnityEngine.Random.Range(0, resNameRevolver.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("rifle"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameRifle[UnityEngine.Random.Range(0, resNameRifle.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameRifle[UnityEngine.Random.Range(0, resNameRifle.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("gatling"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameGatling[UnityEngine.Random.Range(0, resNameGatling.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameGatling[UnityEngine.Random.Range(0, resNameGatling.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("shotgun"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameShotgun[UnityEngine.Random.Range(0, resNameShotgun.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameShotgun[UnityEngine.Random.Range(0, resNameShotgun.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("grenade"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameGrenade[UnityEngine.Random.Range(0, resNameGrenade.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameGrenade[UnityEngine.Random.Range(0, resNameGrenade.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("huuma"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameHuuma[UnityEngine.Random.Range(0, resNameHuuma.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameHuuma[UnityEngine.Random.Range(0, resNameHuuma.Count)]), null, null, null);
                    return s;
                }
                else if (subType.ToLower().Contains("2hstaff"))
                {
                    var s = GetResourceNameFromId(int.Parse(resNameStaff[UnityEngine.Random.Range(0, resNameStaff.Count)]), null, null, null);
                    while (s == "\"Bio_Reseearch_Docu\"")
                        s = GetResourceNameFromId(int.Parse(resNameStaff[UnityEngine.Random.Range(0, resNameStaff.Count)]), null, null, null);
                    return s;
                }
                if (type.ToLower() == "armor")
                {
                    if (location == "หมวกส่วนบน")
                    {
                        var s = GetResourceNameFromId(int.Parse(resNameHead_Top[UnityEngine.Random.Range(0, resNameHead_Top.Count)]), null, null, null);
                        while (s == "\"Bio_Reseearch_Docu\"")
                            s = GetResourceNameFromId(int.Parse(resNameHead_Top[UnityEngine.Random.Range(0, resNameHead_Top.Count)]), null, null, null);
                        return s;
                    }
                    else if (location == "หมวกส่วนกลาง")
                    {
                        var s = GetResourceNameFromId(int.Parse(resNameHead_Mid[UnityEngine.Random.Range(0, resNameHead_Mid.Count)]), null, null, null);
                        while (s == "\"Bio_Reseearch_Docu\"")
                            s = GetResourceNameFromId(int.Parse(resNameHead_Mid[UnityEngine.Random.Range(0, resNameHead_Mid.Count)]), null, null, null);
                        return s;
                    }
                    else if (location == "หมวกส่วนล่าง")
                    {
                        var s = GetResourceNameFromId(int.Parse(resNameHead_Low[UnityEngine.Random.Range(0, resNameHead_Low.Count)]), null, null, null);
                        while (s == "\"Bio_Reseearch_Docu\"")
                            s = GetResourceNameFromId(int.Parse(resNameHead_Low[UnityEngine.Random.Range(0, resNameHead_Low.Count)]), null, null, null);
                        return s;
                    }
                    else if (location == "ชุดเกราะ")
                    {
                        var s = GetResourceNameFromId(int.Parse(resNameArmor[UnityEngine.Random.Range(0, resNameArmor.Count)]), null, null, null);
                        while (s == "\"Bio_Reseearch_Docu\"")
                            s = GetResourceNameFromId(int.Parse(resNameArmor[UnityEngine.Random.Range(0, resNameArmor.Count)]), null, null, null);
                        return s;
                    }
                    else if (location == "ผ้าคลุม")
                    {
                        var s = GetResourceNameFromId(int.Parse(resNameGarment[UnityEngine.Random.Range(0, resNameGarment.Count)]), null, null, null);
                        while (s == "\"Bio_Reseearch_Docu\"")
                            s = GetResourceNameFromId(int.Parse(resNameGarment[UnityEngine.Random.Range(0, resNameGarment.Count)]), null, null, null);
                        return s;
                    }
                    else if (location == "รองเท้า")
                    {
                        var s = GetResourceNameFromId(int.Parse(resNameShoes[UnityEngine.Random.Range(0, resNameShoes.Count)]), null, null, null);
                        while (s == "\"Bio_Reseearch_Docu\"")
                            s = GetResourceNameFromId(int.Parse(resNameShoes[UnityEngine.Random.Range(0, resNameShoes.Count)]), null, null, null);
                        return s;
                    }
                    else if (location == "ประดับข้างซ้าย" || location == "ประดับข้างขวา" || location == "ประดับสองข้าง")
                    {
                        var s = GetResourceNameFromId(int.Parse(resNameAccessory[UnityEngine.Random.Range(0, resNameAccessory.Count)]), null, null, null);
                        while (s == "\"Bio_Reseearch_Docu\"")
                            s = GetResourceNameFromId(int.Parse(resNameAccessory[UnityEngine.Random.Range(0, resNameAccessory.Count)]), null, null, null);
                        return s;
                    }
                    else if (location == "มือซ้าย")
                    {
                        var s = GetResourceNameFromId(int.Parse(resNameShield[UnityEngine.Random.Range(0, resNameShield.Count)]), null, null, null);
                        while (s == "\"Bio_Reseearch_Docu\"")
                            s = GetResourceNameFromId(int.Parse(resNameShield[UnityEngine.Random.Range(0, resNameShield.Count)]), null, null, null);
                        return s;
                    }
                }
                return resourceNameDatas[UnityEngine.Random.Range(0, resourceNameDatas.Count)].resourceName;
            }
        }
        else
        {
            if (isRandomizeResourceName)
                return resourceNameDatas[UnityEngine.Random.Range(0, resourceNameDatas.Count)].resourceName;
        }

        foreach (var item in resourceNameDatas)
        {
            if (item.id == id)
                return item.resourceName;
        }

        return "\"Bio_Reseearch_Docu\"";
    }

    string GetSkillName(string text)
    {
        int _int = 0;
        if (int.TryParse(text, out _int))
        {
            _int = int.Parse(text);
            foreach (var item in skillDatas)
            {
                if (item.id == _int)
                    return "^990B0B" + item.description + "^000000";
            }
        }
        else
        {
            text = RemoveSpace(text);
            foreach (var item in skillDatas)
            {
                if (item.name.ToLower() == text.ToLower())
                    return "^990B0B" + item.description + "^000000";
            }
        }
        return text;
    }

    string ParseSkillName(string text)
    {
        foreach (var item in skillDatas)
        {
            if (text.Contains(item.name))
            {
                text = text.Replace(item.name, item.description);
                break;
            }
        }
        return text;
    }

    string GetClassNumFromId(int id)
    {
        foreach (var item in classNumDatas)
        {
            if (item.id == id)
                return item.classNum;
        }
        return "0";
    }

    string GetMonsterNameFromId(string text)
    {
        int _int = 0;
        if (int.TryParse(text, out _int))
        {
            _int = int.Parse(text);
            foreach (var item in monsterNameDatas)
            {
                if (item.id == _int)
                    return "^FF0000" + item.monsterName + "^000000";
            }
        }
        return "^FF0000" + text + "^000000";
    }

    public static string RemoveCommentAndUnwantedWord(string s)
    {
        if (string.IsNullOrWhiteSpace(s) || string.IsNullOrEmpty(s))
            return string.Empty;
        else
        {
            s = s.Replace("    # !todo check english name", string.Empty);
            s = s.Replace("   # unknown view", string.Empty);
            if (s.Contains("#"))
                s = s.Substring(0, s.IndexOf("#"));
            s = s.Replace("Header:", string.Empty);
            s = s.Replace("  Type: ITEM_DB", string.Empty);
            if (s.Contains("  Version: "))
                s = string.Empty;
            return s;
        }
    }

    public IdNameData GetIdNameData(int id)
    {
        for (int i = 0; i < idNameDatas.Count; i++)
        {
            if (idNameDatas[i].id == id)
                return idNameDatas[i];
        }
        return null;
    }
}
