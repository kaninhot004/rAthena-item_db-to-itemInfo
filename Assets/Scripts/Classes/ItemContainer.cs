using System;

[Serializable]
public class ItemContainer
{
    public string id = string.Empty;
    public string aegisName = string.Empty;
    public string name = string.Empty;
    public string type = string.Empty;
    public string subType = string.Empty;
    public string buy = string.Empty;
    public string weight = string.Empty;
    public string attack = string.Empty;
    public string magicAttack = string.Empty;
    public string defense = string.Empty;
    public string range = string.Empty;
    public string slots = string.Empty;
    public int delay = 0;
    public bool isJob = false;
    public string jobs = string.Empty;
    public bool isClass = false;
    public string classes = string.Empty;
    public string gender = string.Empty;
    public string locations = string.Empty;
    public string debugLocations = string.Empty;
    public string weaponLevel = string.Empty;
    public string armorLevel = string.Empty;
    public string equipLevelMinimum = string.Empty;
    public string equipLevelMaximum = string.Empty;
    public string refinable = string.Empty;
    public string grable = string.Empty;
    public string view = string.Empty;
    public bool isScript = false;
    public string script = string.Empty;
    public bool isEquipScript = false;
    public string equipScript = string.Empty;
    public bool isUnequipScript = false;
    public string unequipScript = string.Empty;
}
