using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Localization : MonoBehaviour
{
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

    string _currentLanguage;
    LocalizationDatabase _currentLocalizationDatabase = new LocalizationDatabase();

    void Start()
    {
        ParseJson();

        SetupLanguage(THAI);
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
}
