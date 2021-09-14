using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using EasyButtons;
using System;
using System.Text;

public class KnuckleFix : MonoBehaviour
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
            if (text.Contains("SubType: Knuckle"))
            {
                bool isFound = false;
                var _i = i;
                while (!isFound && _i < lines.Length)
                {
                    if (lines[_i].Contains("- Id:"))
                        break;

                    if (lines[_i].Contains("Both_Hand: true"))
                    {
                        lines[_i] = lines[_i].Replace("Both_Hand: true", "Right_Hand: true");
                        isFound = true;
                    }
                    _i++;
                }
            }
            builder.Append(text + "\n");
        }
        File.WriteAllText("knuckleFix.txt", builder.ToString(), Encoding.UTF8);
    }
}
