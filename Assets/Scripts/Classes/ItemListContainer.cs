using System;
using System.Collections.Generic;

[Serializable]
public class ItemListContainer
{
    public List<string> allItemIds = new List<string>();

    public List<string> weaponIds = new List<string>();
    public List<string> equipmentIds = new List<string>();
    public List<string> costumeIds = new List<string>();
    public List<string> cardIds = new List<string>();
    public List<string> enchantIds = new List<string>();

    public List<string> petEggIds = new List<string>();
    public List<string> petArmorIds = new List<string>();
    public List<string> fashionCostumeIds = new List<string>();
    public List<string> buffItemIds = new List<string>();

    public List<string> statsItemIds = new List<string>();
    public List<string> hpSpApItemIds = new List<string>();
    public List<string> offensiveItemIds = new List<string>();
    public List<string> defensiveItemIds = new List<string>();
    public List<string> specialItemIds = new List<string>();
    public List<string> castItemIds = new List<string>();
    public List<string> effectItemIds = new List<string>();
    public List<string> autoSpellItemIds = new List<string>();

    public List<string> comboItemIds = new List<string>();
}
