using System;
using System.Collections.Generic;

[Serializable]
public class LocalizationDatabase
{
    public string language;

    public Dictionary<string, string> datas = new Dictionary<string, string>();
}
