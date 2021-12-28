using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardcodeItemScripts : MonoBehaviour
{
    [Serializable]
    public class Data
    {
        public int itemId;
        public string itemScriptDescription;
    }

    public List<Data> datas = new List<Data>();

    public string GetHardcodeItemScript(int itemId)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].itemId == itemId)
                return "			\"" + datas[i].itemScriptDescription + "\",\n";
        }
        return string.Empty;
    }
}
