using System;
using System.Collections.Generic;

[Serializable]
public class ComboDatabase
{
    [Serializable]
    public class SameComboData
    {
        public List<string> aegisNames = new List<string>();
    }

    public List<SameComboData> sameComboDatas = new List<SameComboData>();

    public List<string> descriptions = new List<string>();

    public bool IsAegisNameContain(string aegis_name)
    {
        for (int i = 0; i < sameComboDatas.Count; i++)
        {
            if (sameComboDatas[i].aegisNames.Contains(aegis_name))
                return true;
        }

        return false;
    }
}
