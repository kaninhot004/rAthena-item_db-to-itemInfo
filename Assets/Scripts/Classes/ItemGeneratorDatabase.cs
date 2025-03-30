using System;
using System.IO;
using UnityEngine;

public class ItemGeneratorDatabase
{
    [Serializable]
    public class JsonData
    {
        public Data data = new Data();
    }

    [Serializable]
    public class Data
    {
        public string[] bonuses;
        public string[] bonusesNotStack;
        public string[] notStackableBonuses;
        public string[] effects;
        public string[] elements;
        public string[] races;
        public string[] classes;
        public string[] sizes;
    }

    public static Data MyData = new Data();

    public static void ParseJson()
    {
        MyData = new Data();

        var path = Application.dataPath + "/Assets/item-generator-settings.json";

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
                    MyData = jsonData.data;
            }
        }

        Debug.Log("There are " + MyData.bonuses.Length + " item generator bonuses");
        Debug.Log("There are " + MyData.bonusesNotStack.Length + " item generator bonusesNotStack");
        Debug.Log("There are " + MyData.notStackableBonuses.Length + " item generator notStackableBonuses");
        Debug.Log("There are " + MyData.effects.Length + " item generator effects");
        Debug.Log("There are " + MyData.elements.Length + " item generator elements");
        Debug.Log("There are " + MyData.races.Length + " item generator races");
        Debug.Log("There are " + MyData.classes.Length + " item generator classes");
        Debug.Log("There are " + MyData.sizes.Length + " item generator sizes");
    }
}
