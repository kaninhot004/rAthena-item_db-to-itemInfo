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
        [Serializable]
        public class Value
        {
            public string itemName;
            public string itemValue;
        }

        public string bonus;
        public List<Value> values = new List<Value>();
    }

    // Enums

    // Variables

    public List<Data> datas = new List<Data>();

    // Methods

    public void Add(string bonus, string itemName, string itemValue)
    {
        bonus = bonus.Replace("^990B0B", string.Empty);
        bonus = bonus.Replace("^000000", string.Empty);

        Value value = new Value();
        value.itemName = itemName;
        value.itemValue = itemValue;

        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].bonus == bonus)
            {
                datas[i].values.Add(value);
                return;
            }
        }

        Data data = new Data();
        data.bonus = bonus;
        data.values.Add(value);
        datas.Add(data);
    }
}
