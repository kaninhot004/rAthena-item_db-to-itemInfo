using System;
using System.Collections.Generic;

[Serializable]
public class ItemListContainer
{
    [Serializable]
    public class SubTypeData
    {
        public string subType;
        public List<string> id = new List<string>();
    }

    [Serializable]
    public class LocationData
    {
        public string location;
        public List<string> id = new List<string>();
    }

    public List<string> allItemIds = new List<string>();

    public List<string> weaponIds = new List<string>();
    public List<string> equipmentIds = new List<string>();
    public List<string> costumeIds = new List<string>();
    public List<string> cardIds = new List<string>();
    /// <summary>
    /// Only Card that contained scripts will be kept
    /// </summary>
    public List<string> card2Ids = new List<string>();
    public List<string> enchantIds = new List<string>();
    /// <summary>
    /// Only Enchantment that contained scripts will be kept
    /// </summary>
    public List<string> enchant2Ids = new List<string>();
    public List<string> itemGroupIds = new List<string>();

    public List<string> petEggIds = new List<string>();
    public List<string> petArmorIds = new List<string>();
    public List<string> fashionCostumeIds = new List<string>();
    public List<string> buffItemIds = new List<string>();

    public List<SubTypeData> subTypeDatas = new List<SubTypeData>();
    public List<LocationData> locationDatas = new List<LocationData>();

    public void AddSubType(string subType, string id)
    {
        if (string.IsNullOrEmpty(subType))
            return;

        for (int i = 0; i < subTypeDatas.Count; i++)
        {
            if (subTypeDatas[i].subType == subType)
            {
                subTypeDatas[i].id.Add(id);
                return;
            }
        }

        SubTypeData subTypeData = new SubTypeData();
        subTypeData.subType = subType;
        subTypeData.id.Add(id);

        subTypeDatas.Add(subTypeData);
    }

    public void AddLocation(string location, string id)
    {
        if (string.IsNullOrEmpty(location))
            return;

        for (int i = 0; i < locationDatas.Count; i++)
        {
            if (locationDatas[i].location == location)
            {
                locationDatas[i].id.Add(id);
                return;
            }
        }

        LocationData locationData = new LocationData();
        locationData.location = location;
        locationData.id.Add(id);

        locationDatas.Add(locationData);
    }
}
