using System;
using System.Collections.Generic;

[Serializable]
public class LocalizationDatabase
{
    public class Data
    {
        public string thai;
        public string english;
        public string japanese;
        public string traditionalChinese;
        public string simpifiedChinese;
        public string korean;
    }

    public Dictionary<string, Data> datas = new Dictionary<string, Data>();
}
