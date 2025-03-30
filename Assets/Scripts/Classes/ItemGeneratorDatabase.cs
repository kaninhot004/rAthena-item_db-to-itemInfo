using System;
using System.IO;
using System.Collections.Generic;
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
        public List<string> bonuses = new List<string>();
        public List<string> bonusesNotStack = new List<string>();
        public List<string> notStackableBonuses = new List<string>();
        public List<string> effects = new List<string>();
        public List<string> elements = new List<string>();
        public List<string> races = new List<string>();
        public List<string> classes = new List<string>();
        public List<string> sizes = new List<string>();
    }

    public static Data MyData = new Data();

    public static void ParseJson()
    {
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

        Debug.Log("There are " + MyData.bonuses.Count + " item generator bonuses");
        Debug.Log("There are " + MyData.bonusesNotStack.Count + " item generator bonusesNotStack");
        Debug.Log("There are " + MyData.notStackableBonuses.Count + " item generator notStackableBonuses");
        Debug.Log("There are " + MyData.effects.Count + " item generator effects");
        Debug.Log("There are " + MyData.elements.Count + " item generator elements");
        Debug.Log("There are " + MyData.races.Count + " item generator races");
        Debug.Log("There are " + MyData.classes.Count + " item generator classes");
        Debug.Log("There are " + MyData.sizes.Count + " item generator sizes");
    }
}
