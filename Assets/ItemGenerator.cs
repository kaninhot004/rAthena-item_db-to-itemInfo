using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using System.IO;
using System.Text;

public class ItemGenerator : MonoBehaviour
{
    List<string> allWeaponSubType = new List<string>() { "Dagger", "1hSword", "2hSword", "1hSpear", "2hSpear", "1hAxe", "2hAxe", "Mace", "Staff", "Bow", "Knuckle", "Musical", "Whip", "Book", "Katar", "Revolver", "Rifle", "Gatling", "Shotgun", "Grenade", "Huuma", "2hStaff" };
    List<string> allAmmoSubType = new List<string>() { "Arrow", "Dagger", "Bullet", "Shell", "Grenade", "Shuriken", "Kunai", "CannonBall", "ThrowWeapon" };

    List<string> allWeaponLocation = new List<string>() { "Right_Hand", "Left_Hand", "Both_Hand" };
    List<string> allShieldLocation = new List<string>() { "Left_Hand" };
    List<string> allArmorLocation = new List<string>() { "Head_Top", "Head_Mid", "Head_Low", "Armor", "Garment", "Shoes", "Right_Accessory", "Left_Accessory", "Both_Accessory" };
    List<string> allAmmoLocation = new List<string>() { "Ammo" };
    List<int> allShieldView = new List<int>() { 1, 2, 3 };

    // View
    int GetWeaponView(string subType)
    {
        if (subType == "Dagger") return 1;
        else if (subType == "1hSword") return 2;
        else if (subType == "2hSword") return 3;
        else if (subType == "1hSpear") return 4;
        else if (subType == "2hSpear") return 5;
        else if (subType == "1hAxe") return 6;
        else if (subType == "2hAxe") return 7;
        else if (subType == "Mace") return 8;
        else if (subType == "Staff") return 10;
        else if (subType == "Bow") return 11;
        else if (subType == "Knuckle") return 12;
        else if (subType == "Musical") return 13;
        else if (subType == "Whip") return 14;
        else if (subType == "Book") return 15;
        else if (subType == "Katar") return 16;
        else if (subType == "Revolver") return 17;
        else if (subType == "Rifle") return 18;
        else if (subType == "Gatling") return 19;
        else if (subType == "Shotgun") return 20;
        else if (subType == "Grenade") return 21;
        else if (subType == "Huuma") return 22;
        else if (subType == "2hStaff") return 23;
        return 0;
    }

    enum GenType { Weapon, Shield, Armor, Ammo };

    [Button]
    public void Generate()
    {
        StringBuilder sum = new StringBuilder();

        int itemName = 1;

        int itemPerTier = 10000;

        int id = 100000;

        for (int i = 0; i < itemPerTier; i++)
        {
            int itemType = Random.Range(0, 4);
            GenType genType = GenType.Weapon;
            if (itemType == 1)
                genType = GenType.Shield;
            else if (itemType == 2)
                genType = GenType.Armor;
            else if (itemType == 3)
                genType = GenType.Ammo;

            string subType = genType == GenType.Weapon ? allWeaponSubType[Random.Range(0, allWeaponSubType.Count)] : genType == GenType.Ammo ? allAmmoSubType[Random.Range(0, allAmmoSubType.Count)] : "";
            sum.Append(string.Format(" - Id: {0}\n   AegisName: {1}\n   Name: {2}\n   Type: {3}\n   SubType: {4}\n   Weight: {5}\n   Attack: {6}\n   MagicAttack: {7}\n   Defense: {8}\n   Range: {9}\n   Slots: {10}\n   Locations:\n      {11}\n   WeaponLevel: {12}\n   View: {13}\n   Script: |\n      {14}\n"
            , id.ToString("f0") // ID
                , "aegis_" + id.ToString("f0") // Aegis Name
                , itemName.ToString("f0") // Name
                , genType.ToString() // Type
                , subType // Sub Type
                , Random.Range(10, 10000).ToString("f0") // Weight
                , (genType == GenType.Weapon || genType == GenType.Ammo) ? Random.Range(1, 501).ToString("f0") : "0" // Attack
                , (genType == GenType.Weapon || genType == GenType.Ammo) ? Random.Range(1, 501).ToString("f0") : "0" // Magic Attack
                , (genType == GenType.Armor || genType == GenType.Shield) ? Random.Range(1, 51).ToString("f0") : "0" // Defense
                , genType == GenType.Weapon ? Random.Range(1, 6).ToString("f0") : "0" // Range
                , "0" // Slots
                , genType == GenType.Weapon ? allWeaponLocation[Random.Range(0, allWeaponLocation.Count)] + ": true" : genType == GenType.Shield ? allShieldLocation[Random.Range(0, allShieldLocation.Count)] + ": true" : genType == GenType.Armor ? allArmorLocation[Random.Range(0, allArmorLocation.Count)] + ": true" : genType == GenType.Ammo ? allAmmoLocation[Random.Range(0, allAmmoLocation.Count)] + ": true" : string.Empty  // Locations
                , genType == GenType.Weapon ? Random.Range(1, 5).ToString("f0") : "0" // Weapon Level
                , genType == GenType.Weapon ? GetWeaponView(subType).ToString("f0") : genType == GenType.Shield ? allShieldView[Random.Range(0, allShieldView.Count)].ToString("f0") : 0.ToString("f0") // View
                , "bonus bInt,1;" // Script
                ));
            id++;
            itemName++;
        }

        File.WriteAllText("generatedItemDb.txt", sum.ToString(), Encoding.UTF8);
    }
}
