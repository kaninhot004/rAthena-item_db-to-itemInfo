using System;
using System.Collections.Generic;

[Serializable]
public class EasyItemBuilderDatabase
{
    // Constants

    // Local classes / structs

    [Serializable]
    public class Data
    {
        public string bonus;
        public string item;
    }

    // Enums

    // Variables

    public List<Data> datas = new List<Data>();

    // Methods

    public void Add(string value, string itemName)
    {
        value = value.Replace("^990B0B", string.Empty);
        value = value.Replace("^000000", string.Empty);

        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].bonus == value)
            {
                datas[i].item += "\n" + itemName;
                return;
            }
        }

        Data data = new Data();
        data.bonus = value;
        data.item = itemName;

        if (!datas.Contains(data))
            datas.Add(data);
    }
}
