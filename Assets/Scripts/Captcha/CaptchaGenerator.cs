using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class CaptchaGenerator : MonoBehaviour
{
    public class Data
    {
        public int id;
        public string answer;
    }

    [SerializeField] int _width = 525;
    [SerializeField] int _height = 175;

    [SerializeField] GameObject _captchaObject;
    [SerializeField] Text _captchaText;

    List<Data> _datas = new List<Data>();

    bool _isWorking;

    public void Generate()
    {
        if (_isWorking)
            return;


        FetchCaptcha();
        Print();
    }

    void FetchCaptcha()
    {
        var path = Application.dataPath + "/Assets/captcha.txt";

        // Is file exists?
        if (!File.Exists(path))
        {
            Debug.Log(path + " not found.");

            return;
        }

        var captchaFile = File.ReadAllText(path);

        var captchaDatabases = captchaFile.Split('\n');

        _datas = new List<Data>();

        for (int i = 0; i < captchaDatabases.Length; i++)
        {
            var text = captchaDatabases[i];

            text = CommentRemover.Fix(text);

            text = LineEndingsRemover.Fix(text);

            if (string.IsNullOrEmpty(text)
                || string.IsNullOrWhiteSpace(text))
                continue;

            var texts = text.Split('=');

            var captchaId = int.Parse(texts[0]);
            var answer = texts[1];

            Data data = new Data();
            data.id = captchaId;
            data.answer = answer;
            _datas.Add(data);
        }

        Debug.Log("There are " + _datas.Count + " captcha database.");
    }

    void Print()
    {
        StartCoroutine(_captchaGenerate());
    }

    // Area Screenshot
    // Credit: https://forum.unity.com/threads/taking-screenshot-of-partial-area.54189/#post-8377539
    IEnumerator _captchaGenerate()
    {
        _isWorking = true;
        _captchaObject.SetActive(_isWorking);

        StringBuilder builder = new StringBuilder();

        builder.Append("# This file is a part of rAthena.\n");
        builder.Append("#   Copyright(C) 2022 rAthena Development Team\n");
        builder.Append("#   https://rathena.org - https://github.com/rathena\n");
        builder.Append("#\n");
        builder.Append("# This program is free software: you can redistribute it and/or modify\n");
        builder.Append("# it under the terms of the GNU General Public License as published by\n");
        builder.Append("# the Free Software Foundation, either version 3 of the License, or\n");
        builder.Append("# (at your option) any later version.\n");
        builder.Append("#\n");
        builder.Append("# This program is distributed in the hope that it will be useful,\n");
        builder.Append("# but WITHOUT ANY WARRANTY; without even the implied warranty of\n");
        builder.Append("# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the\n");
        builder.Append("# GNU General Public License for more details.\n");
        builder.Append("#\n");
        builder.Append("# You should have received a copy of the GNU General Public License\n");
        builder.Append("# along with this program. If not, see <http://www.gnu.org/licenses/>.\n");
        builder.Append("#\n");
        builder.Append("###########################################################################\n");
        builder.Append("# Captcha Database Table\n");
        builder.Append("###########################################################################\n");
        builder.Append("#\n");
        builder.Append("# Captcha Database Settings\n");
        builder.Append("#\n");
        builder.Append("###########################################################################\n");
        builder.Append("# - Id                Index value.\n");
        builder.Append("#   Filename          Name of the BMP image file (with location).\n");
        builder.Append("#   Answer            Correct answer for the captcha (case-sensitive).\n");
        builder.Append("#   Bonus             Bonus Script ran on success. (Default: Level 10 Blessing and Increase Agility)\n");
        builder.Append("###########################################################################\n");
        builder.Append("\n");
        builder.Append("Header:\n");
        builder.Append("  Type: CAPTCHA_DB\n");
        builder.Append("  Version: 1\n");
        builder.Append("\n");
        builder.Append("Body:\n");

        var texture2D = new Texture2D(_width, _height);
        Rect rect = new Rect(0, 0, _width, _height);

        for (int i = 0; i < _datas.Count; i++)
        {
            _captchaText.text = _datas[i].answer;

            yield return new WaitForEndOfFrame();

            texture2D.ReadPixels(rect, 0, 0);
            texture2D.Apply();

            var bytes = texture2D.EncodeToPNG();

            File.WriteAllBytes(Application.dataPath + "/" + (i + 1) + ".png", bytes);

            Debug.Log("'" + _datas[i].answer + "' has been successfully created.");

            builder.Append(" - Id: " + _datas[i].id + "\n");
            builder.Append("   Filename: captcha/" + _datas[i].id + ".bmp\n");
            builder.Append("   Answer: " + _datas[i].answer + "\n");
        }

        File.WriteAllText("captcha_db.yml", builder.ToString(), Encoding.UTF8);

        Debug.Log("'captcha_db.yml' has been successfully created.");

        Destroy(texture2D);

        _isWorking = false;
        _captchaObject.SetActive(_isWorking);
    }
}
