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
    public const string CONVERT_PROGRESSION_FETCHING_PLEASE_WAIT = "CONVERT_PROGRESSION_FETCHING_PLEASE_WAIT";

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

    public const string AUTO_BONUS_3 = "AUTO_BONUS_3";

    [Serializable]
    public class JsonData
    {
        public List<Data> datas = new List<Data>();

        [Serializable]
        public class Data
        {
            public string language;

            public List<KeyData> keyDatas = new List<KeyData>();

            [Serializable]
            public class KeyData
            {
                public string key;
                public string texts;
            }
        }

    }

    List<LocalizationDatabase> _localizationDatabases = new List<LocalizationDatabase>();

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
                    _localizationDatabases = new List<LocalizationDatabase>();

                    for (int i = 0; i < jsonData.datas.Count; i++)
                    {
                        LocalizationDatabase localizationDatabase = new LocalizationDatabase();

                        localizationDatabase.language = jsonData.datas[i].language;

                        localizationDatabase.datas = new Dictionary<string, string>();

                        for (int j = 0; j < jsonData.datas[i].keyDatas.Count; j++)
                            localizationDatabase.datas.Add(jsonData.datas[i].keyDatas[j].key, jsonData.datas[i].keyDatas[j].texts);

                        _localizationDatabases.Add(localizationDatabase);
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
            return _currentLocalizationDatabase.datas[key];
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

        _currentLocalizationDatabase = new LocalizationDatabase();

        for (int i = 0; i < _localizationDatabases.Count; i++)
        {
            if (_localizationDatabases[i].language == _currentLanguage)
            {
                _currentLocalizationDatabase = _localizationDatabases[i];

                break;
            }
        }
    }
}
