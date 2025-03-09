using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class HardcodeItemScripts : MonoBehaviour
{
    [Serializable]
    public class JsonData
    {
        public List<Data> datas = new List<Data>();
    }

    [Serializable]
    public class Data
    {
        public int itemId;
        public string itemScriptDescription;
    }

    Dictionary<int, string> _dictDatas = new Dictionary<int, string>();

    void Start()
    {
        ParseJson();
    }

    /// <summary>
    /// Get hardcode item scripts by item id
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public string GetHardcodeItemScript(int itemId)
    {
        if (_dictDatas.ContainsKey(itemId))
            return "			\"" + _dictDatas[itemId] + "\",\n";
        else
            return string.Empty;
    }

    void ParseJson()
    {
        var path = Application.dataPath + "/Assets/hard_code_item_scripts.json";

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
                    _dictDatas = new Dictionary<int, string>();

                    for (int i = 0; i < jsonData.datas.Count; i++)
                        _dictDatas.Add(jsonData.datas[i].itemId, jsonData.datas[i].itemScriptDescription);
                }
            }
        }
        Debug.Log("There are " + _dictDatas.Count + " hardcode item description");
    }
}
