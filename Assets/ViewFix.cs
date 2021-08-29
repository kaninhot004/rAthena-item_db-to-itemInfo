using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using EasyButtons;
using System;
using System.Text;

public class ViewFix : MonoBehaviour
{
    [SerializeField] ItemGenerator itemGenerator;

    [Button]
    public void FixNow()
    {
        StringBuilder builder = new StringBuilder();
        var allTextAsset = File.ReadAllText(Application.dataPath + "/Assets/item_db_custom.txt");
        var lines = allTextAsset.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            var text = lines[i];
            if (text.Contains("Head_Top:") || text.Contains("Head_Mid:") || text.Contains("Head_Low:"))
                lines[i + 1] = "    View: " + itemGenerator.m_AllHeadgearView[UnityEngine.Random.Range(0, itemGenerator.m_AllHeadgearView.Count)].ToString("f0");
            builder.Append(text+"\n");
        }
        File.WriteAllText("viewFix.txt", builder.ToString(), Encoding.UTF8);
    }
}
